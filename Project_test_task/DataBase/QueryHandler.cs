using Project_test_task.ResultSaver;
using Project_test_task.Uility;
using System.Data;
using System.Data.SQLite;


namespace Project_test_task.Data
{
    public interface IFilter
    {
        DataTable Filter();
    }

    public class OrderFilterByDate : IFilter
    {
        private readonly DataBase dataBase;
        private readonly string district;
        private readonly int additionalMinutes = 30;
        private readonly IResultSaver resultSaver;
        private readonly CompositeLogger compositeLogger;

        public OrderFilterByDate(string district, IResultSaver resultSaver)
        {
            dataBase = DataBase.getInstence();
            this.district = district;
            this.resultSaver = resultSaver;
            compositeLogger = CompositeLogger.Instance;
        }

        private DateTime GetFirstDeliveryDateTime()
        {
            var connection = dataBase.OpenConnection();

            string query = @"
                SELECT MIN(DeliveryDateTime) 
                FROM Orders 
                INNER JOIN CityDistricts ON Orders.CityDistrict = CityDistricts.DistrictId
                WHERE CityDistricts.DistrictName = @CityDistrict";

            using (SQLiteCommand command = new SQLiteCommand(query, connection))
            {
                command.Parameters.AddWithValue("@CityDistrict", district);
                var result = command.ExecuteScalar();
                return result != DBNull.Value ? Convert.ToDateTime(result) : DateTime.MinValue;
            }
        }

        public DataTable Filter()
        {
            var connection = dataBase.OpenConnection();
            DateTime firstDeliveryDateTime = GetFirstDeliveryDateTime();
            DataTable result = new DataTable();

            if (firstDeliveryDateTime == DateTime.MinValue)
            {
                compositeLogger.Log("Нет заказов для заданного района.");
                return result;
            }

            string query = @"
                    SELECT Orders.OrderId, Orders.Weight, Orders.DeliveryDateTime 
                    FROM Orders 
                    INNER JOIN CityDistricts ON Orders.CityDistrict = CityDistricts.DistrictId
                    WHERE CityDistricts.DistrictName = @CityDistrict
                    AND Orders.DeliveryDateTime BETWEEN @FirstDeliveryDateTime 
                    AND datetime(@FirstDeliveryDateTime, '+' || @AdditionalMinutes || ' minutes')";

            using (SQLiteCommand command = new SQLiteCommand(query, connection))
            {
                command.Parameters.AddWithValue("@CityDistrict", district);
                command.Parameters.AddWithValue("@FirstDeliveryDateTime", firstDeliveryDateTime);
                command.Parameters.AddWithValue("@AdditionalMinutes", additionalMinutes);

                using (SQLiteDataReader reader = command.ExecuteReader())
                {
                    result.Load(reader);
                    resultSaver.SaveResult(result);
                    compositeLogger.Log("Результаты фильтрации успешно сохранены.");
                }
            }
            dataBase.CloseConnection();
            return result;
        }
    }
}
