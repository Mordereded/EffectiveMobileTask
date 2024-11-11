using System.Data;
using Project.Modules.DataBase;


namespace Tests
{
    

    public class DataBaseTests
    {
        [Fact]
        public void Test_CreateTable_ShouldCreateTables()
        {
            try
            {
                DataBase conn = DataBase.getInstence();
                conn.ChangeDataBase("Data Source=tests.db;Version=3;");
                conn.CreateTable();
                var tablesCreated = conn.CheckTablesExist();
                Assert.True(tablesCreated, "Таблицы должны создаться.");
            }
            catch (Exception ex)
            {
                Assert.True(false, $"Ошибка в Test_CreateTable_ShouldCreateTables: {ex.Message}");
            }
        }

        [Fact]
        public void Test_InsertSingleData_ShouldInsertOrderSuccessfully()
        {
            try
            {
                DataBase database = DataBase.getInstence();
                database.ChangeDataBase("Data Source=tests.db;Version=3;");
                database.CreateTable();
                database.FillCityDistrict(); 
                
                double weight = 25.5;
                int district = 1;
                DateTime deliveryTime = DateTime.Now;
                
                database.InsertSingleData(weight, district, deliveryTime);
                DataTable ordersTable = database.MakeDataTable();
                
                bool orderExists = false;
                foreach (DataRow row in ordersTable.Rows)
                {
                    if ((double)row["Weight"] == weight && (DateTime)row["DeliveryDateTime"] == deliveryTime)
                    {
                        orderExists = true;
                        break;
                    }
                }
                
                Assert.True(orderExists, "Заказ должен быть в таблице.");
            }
            catch (Exception ex)
            {
                Assert.True(false, $"Ошибка в Test_InsertSingleData_ShouldInsertOrderSuccessfully: {ex.Message}");
            }
        }

        
    }
}
