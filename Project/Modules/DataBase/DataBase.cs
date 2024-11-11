using System.Data;
using System.Data.Common;
using System.Data.SQLite;
using Project.Modules.Logger;
using Project_test_task.Uility;


namespace Project.Modules.DataBase
{
    public sealed class DataBase
    {
        private SQLiteConnection sqlconnection;
        private static DataBase? instence;
        private readonly CompositeLogger logger;
        private DataBase()
        {
            logger = CompositeLogger.Instance;
            RefreshDataBase();
            instence = this;
        }

        public static DataBase getInstence()
        {
            if (instence == null)
                instence = new DataBase();
            return instence;
        }

        public void LoadData()
        {
            string sql = Config.ConfigConnectionDataBaseSettings();
            logger.Log("Текущий рабочий каталог: " + Environment.CurrentDirectory);
            sqlconnection = new SQLiteConnection(sql);
        }
        public void ChangeDataBase(string sql)
        {
            sqlconnection = new SQLiteConnection(sql);
        }
        /// <summary>
        /// Инициализация таблиц
        /// </summary>
        public void CreateTable()
        {
            sqlconnection.Open();
            string createTableQuery = @"
                CREATE TABLE IF NOT EXISTS CityDistricts (
                    DistrictId INTEGER PRIMARY KEY AUTOINCREMENT,
                    DistrictName TEXT UNIQUE NOT NULL
                )";
            using (SQLiteCommand command = new SQLiteCommand(createTableQuery, sqlconnection))
            {
                command.ExecuteNonQuery();
                logger.Log("Таблица CityDistricts создана.");
            }

            createTableQuery = @"
                CREATE TABLE IF NOT EXISTS Orders (
                    OrderId INTEGER PRIMARY KEY AUTOINCREMENT,
                    Weight REAL NOT NULL,
                    CityDistrict INT NOT NULL,
                    DeliveryDateTime DATETIME NOT NULL,
                    FOREIGN KEY (CityDistrict) REFERENCES CityDistricts(DistrictId)
                )";
            using (SQLiteCommand command = new SQLiteCommand(createTableQuery, sqlconnection))
            {
                command.ExecuteNonQuery();
                logger.Log("Таблица Orders создана.");
            }
            sqlconnection.Close();

        }
        public SQLiteConnection OpenConnection()
        {
            if (sqlconnection.State != ConnectionState.Open)
                sqlconnection.Open();
            return sqlconnection;
        }
        public SQLiteConnection CloseConnection()
        {
            if (sqlconnection.State != ConnectionState.Closed)
                sqlconnection.Close();
            return sqlconnection;
        }
        public void BackUpDatabaseFromFile(string backupFilePath)
        {
            sqlconnection.Open();
            if (string.IsNullOrEmpty(backupFilePath))
            { logger.Log("Путь к файлу с базой данных не корректен"); return; }
            try
            {
                var backupConnection = new SQLiteConnection($"Data Source={backupFilePath};Version=3;");
                backupConnection.Open();
                backupConnection.BackupDatabase(sqlconnection, "main", "main", -1, null, 0);
                
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при восстановлении базы данных: {ex.Message}");
                throw; 
            }
            finally
            {
                sqlconnection.Close(); 
            }
        }
        public void SaveDatabaseToFile(string filePath)
        {
            if (string.IsNullOrEmpty(filePath))
            {
                MessageBox.Show("Путь к файлу не указан.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                logger.Log("Путь к файлу не указан.");
                return;
            }

            try
            {
                sqlconnection.Open();
                using (var backupConnection = new SQLiteConnection($"Data Source={filePath};Version=3;"))
                {
                    backupConnection.Open();

                    sqlconnection.BackupDatabase(backupConnection, "main", "main", -1, null, 0);
                }
                logger.Log($"База данных успешно сохранена в '{filePath}'.");
                MessageBox.Show($"База данных успешно сохранена в '{filePath}'.", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                logger.Log($"Ошибка при сохранении базы данных: {ex.Message}");
                MessageBox.Show($"Ошибка при сохранении базы данных: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                sqlconnection.Close();
            }
        }

        /// <summary>
        /// Удаление всех таблиц и последовательностей
        /// </summary>
        public void DeleteTable()
        {
            string deleteOrdersQuery = "DELETE FROM Orders;";
            string deleteDistrictsQuery = "DELETE FROM CityDistricts;";
            string resetOrdersAutoIncrementQuery = "DELETE FROM sqlite_sequence WHERE name='Orders';";
            string resetDistrictsAutoIncrementQuery = "DELETE FROM sqlite_sequence WHERE name='CityDistricts';";

            try
            {
                sqlconnection.Open();
                using (SQLiteCommand deleteOrdersCommand = new SQLiteCommand(deleteOrdersQuery, sqlconnection))
                {
                    deleteOrdersCommand.ExecuteNonQuery();
                }
                using (SQLiteCommand deleteDistrictsCommand = new SQLiteCommand(deleteDistrictsQuery, sqlconnection))
                {
                    deleteDistrictsCommand.ExecuteNonQuery();
                }
                using (SQLiteCommand resetOrdersCommand = new SQLiteCommand(resetOrdersAutoIncrementQuery, sqlconnection))
                {
                    resetOrdersCommand.ExecuteNonQuery();
                }
                using (SQLiteCommand resetDistrictsCommand = new SQLiteCommand(resetDistrictsAutoIncrementQuery, sqlconnection))
                {
                    resetDistrictsCommand.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                sqlconnection.Close();
            }
            logger.Log("Удаление таблиц");
            //RefreshDataBase();

        }
        public void InsertSingleData(double weight, int cityDistrict, DateTime deliveryDateTime)
        {
            // SQL-запрос для проверки существования района
            string checkDistrictQuery = "SELECT COUNT(*) FROM CityDistricts WHERE DistrictId = @DistrictId;";
            string insertQuery = "INSERT INTO Orders (Weight, CityDistrict, DeliveryDateTime) " +
                                 "VALUES (@Weight, @CityDistrict, @DeliveryDateTime);";

            try
            {
                sqlconnection.Open();

                using (SQLiteCommand checkCommand = new SQLiteCommand(checkDistrictQuery, sqlconnection))
                {
                    checkCommand.Parameters.AddWithValue("@DistrictId", cityDistrict);
                    int districtCount = Convert.ToInt32(checkCommand.ExecuteScalar());

                    if (districtCount == 0)
                    {
                        logger.Log("Ошибка: указанный район не существует.");
                        return;
                    }
                }

                using (SQLiteCommand command = new SQLiteCommand(insertQuery, sqlconnection))
                {
                    command.Parameters.AddWithValue("@Weight", weight);
                    command.Parameters.AddWithValue("@CityDistrict", cityDistrict);
                    command.Parameters.AddWithValue("@DeliveryDateTime", deliveryDateTime);
                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        logger.Log("Заказ успешно добавлен.");
                        logger.Log($"Заказ с полями:\n" +
                            $"Вес: {weight},\n" +
                            $"Район: {cityDistrict},\n" +
                            $"Время доставки: {deliveryDateTime}");

                    }
                    else
                    {
                        logger.Log("Не удалось добавить заказ.");
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Log("Ошибка при добавлении заказа: " + ex.Message);
            }
            finally
            {
                sqlconnection.Close();
            }
        }

        public void FillCityDistrict()
        {
            if (SelectDistricts().Count() > 0) return;
            string[] districts = new string[]
            {
                "Центральный",
                "Северный",
                "Южный",
                "Западный",
                "Восточный",
                "Московский",
                "Киевский",
                "Львовский",
                "Одесский",
                "Харьковский"
            };

            try
            {
                sqlconnection.Open();

                foreach (var district in districts)
                {
                    string insertQuery = "INSERT INTO CityDistricts (DistrictName) VALUES (@DistrictName)";
                    using (SQLiteCommand command = new SQLiteCommand(insertQuery, sqlconnection))
                    {
                        command.Parameters.AddWithValue("@DistrictName", district);
                        command.ExecuteNonQuery();
                    }
                }

                logger.Log("Таблица CityDistricts успешно заполнена начальными значениями.");
            }
            catch (Exception ex)
            {
                logger.Log("Ошибка при заполнении таблицы: " + ex.Message);
            }
            finally
            {
                sqlconnection.Close();
            }
        }

        public void InsertData(List<InputData> generatedData)
        {
            if (generatedData == null || generatedData.Count == 0)
            {
                return;
            }
            sqlconnection.Open();
            using (SQLiteTransaction transaction = sqlconnection.BeginTransaction())
            {
                try
                {
                    foreach (var data in generatedData)
                    {
                        string insertCommand = @"
                        INSERT INTO Orders(Weight, CityDistrict, DeliveryDateTime)
                        VALUES (@Weight, @CityDistrict, @DeliveryDateTime);";
                        using (SQLiteCommand command = new SQLiteCommand(insertCommand, sqlconnection))
                        {
                            command.Parameters.AddWithValue("@Weight", Math.Round(data.weight, 2));
                            command.Parameters.AddWithValue("@CityDistrict", data.district);
                            command.Parameters.AddWithValue("@DeliveryDateTime", data.deliveryTime);
                            command.ExecuteNonQuery();
                        }
                    }

                    transaction.Commit();
                    logger.Log("Транзакция прошла успешно, данные были добавленны");
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    logger.Log($"Ошибка: {ex.Message}");
                }
                finally
                {
                    sqlconnection.Close();
                }
            }

        }
        public int GetOrderCount()
        {
            sqlconnection.Open();
            using (var command = new SQLiteCommand("SELECT COUNT(*) FROM Orders;", sqlconnection))
            {
                int result = Convert.ToInt32(command.ExecuteScalar());
                sqlconnection.Close();
                return result;
            }
        }

        public int GetDistrictCount()
        {
            sqlconnection.Open();
            using (var command = new SQLiteCommand("SELECT COUNT(*) FROM CityDistricts;", sqlconnection))
            {

                int result = Convert.ToInt32(command.ExecuteScalar());
                sqlconnection.Close();
                return result;
            }
        }

        public bool CheckTablesExist()
        {
            var query = "SELECT name FROM sqlite_master WHERE type='table' AND (name='Orders' OR name='CityDistricts');";
            sqlconnection.Open();
            using (var command = new SQLiteCommand(query, sqlconnection))
            using (var reader = command.ExecuteReader())
            {
                int tableCount = 0;
                while (reader.Read())
                {
                    tableCount++;
                }
                sqlconnection.Close();
                return tableCount == 2;
            }

        }

        public int GetDistrictIdByName(string districtName)
        {
            int districtId = -1;
            string query = "SELECT DistrictId FROM CityDistricts WHERE DistrictName = @DistrictName";
            sqlconnection.Open();
            using (var command = new SQLiteCommand(query, sqlconnection))
            {
                command.Parameters.AddWithValue("@DistrictName", districtName);
                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        districtId = reader.GetInt32(0);
                    }
                }
            }
            sqlconnection.Close();
            return districtId;
        }

        public int InsertDistrict(string districtName)
        {
            string insertQuery = "INSERT INTO CityDistricts (DistrictName) VALUES (@DistrictName)";
            sqlconnection.Open();
            using (var command = new SQLiteCommand(insertQuery, sqlconnection))
            {
                command.Parameters.AddWithValue("@DistrictName", districtName);
                command.ExecuteNonQuery();
            }
            int resultId = (int)sqlconnection.LastInsertRowId;
            sqlconnection.Close();
            return resultId;
        }
        /// <summary>
        /// Формирование таблицы для вывода
        /// </summary>
        /// <returns></returns>
        public DataTable MakeDataTable()
        {
            try
            {
                sqlconnection.Open();
                string selectQuery = @"
                            SELECT 
                                Orders.OrderId, 
                                Orders.Weight, 
                                CityDistricts.DistrictName AS CityDistrict, 
                                Orders.DeliveryDateTime 
                            FROM Orders
                            INNER JOIN CityDistricts ON Orders.CityDistrict = CityDistricts.DistrictId;
                        ";
                SQLiteDataAdapter dataAdapter = new SQLiteDataAdapter(selectQuery, sqlconnection);
                DataTable dataTable = new DataTable();
                dataAdapter.Fill(dataTable);

                return dataTable;
            }
            catch (Exception ex)
            {
                logger.Log("Ошибка при загрузке данных: " + ex.Message);
                return new DataTable();
            }
            finally
            {
                sqlconnection.Close();
            }
        }
        public Dictionary<long, string> SelectDistricts()
        {
            Dictionary<long, string> cityDistricts = new Dictionary<long, string>();
            try
            {
                sqlconnection.Open();

                string selectQuery = "SELECT DistrictId, DistrictName FROM CityDistricts;";
                using (SQLiteCommand command = new SQLiteCommand(selectQuery, sqlconnection))
                using (SQLiteDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        cityDistricts.Add(reader.GetInt64(0), reader.GetString(1));
                    }
                }
                return cityDistricts;
            }
            catch (Exception ex)
            {
                logger.Log("Ошибка при загрузке данных: " + ex.Message);
                return cityDistricts;
            }
            finally
            {
                sqlconnection.Close();
            }
        }
        public void RefreshDataBase()
        {
            LoadData();
            CreateTable();
            FillCityDistrict();
        }
    }



}
