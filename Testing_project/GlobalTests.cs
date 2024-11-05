using Project_test_task.Generator;
using Project_test_task.Data;
using System.Data;
using System.Text;
using Project_test_task.ConsoleManager;
using Project_test_task;

namespace Tests
{
    public struct TestResult
    {
        public bool isPassed;
        public string Name;
        public TestResult(bool isPassed, string name)
        { this.isPassed = isPassed; this.Name = name; }
    }

    public class UnitTests
    {
        public StringBuilder RunTests()
        {
            var results = new List<TestResult>
            {
                Test_CreateTable_ShouldCreateTables(),
                Test_InsertSingleData_ShouldInsertOrderSuccessfully(),
                Test_FillCityDistrict_ShouldFillInitialDistricts(),
                Test_DeleteTable_ShouldDeleteData(),
                Test_InsertData_ShouldInsertMultipleOrders()
            };

            var answer = ShowResults(results);

            return answer;
        }

        [Fact]
        public TestResult Test_CreateTable_ShouldCreateTables()
        {
            try
            {
                var database = DataBase.getInstence();
                database.CreateTable();
                var tablesCreated = database.CheckTablesExist();
                Assert.True(tablesCreated);
                return new TestResult(true, "Test_CreateTable_ShouldCreateTables");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка в тесте Test_CreateTable_ShouldCreateTables: {ex.Message}");
                return new TestResult(false, "Test_CreateTable_ShouldCreateTables");
            }
        }

        [Fact]
        public TestResult Test_InsertSingleData_ShouldInsertOrderSuccessfully()
        {
            try
            {
                var database = DataBase.getInstence();
                database.CreateTable();
                double weight = 25.5;
                int district = 1;
                DateTime deliveryTime = DateTime.Now;
                database.InsertSingleData(weight, district, deliveryTime);
                DataTable ordersTable = database.MakeDataTable();
                bool orderExists = false;

                foreach (DataRow row in ordersTable.Rows)
                {
                    if ((double)row["Weight"] == weight &&
                        (DateTime)row["DeliveryDateTime"] == deliveryTime)
                    {
                        orderExists = true;
                        break;
                    }
                }

                Assert.True(orderExists);
                return new TestResult(orderExists, "Test_InsertSingleData_ShouldInsertOrderSuccessfully");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка в тесте Test_InsertSingleData_ShouldInsertOrderSuccessfully: {ex.Message}");
                return new TestResult(false, "Test_InsertSingleData_ShouldInsertOrderSuccessfully");
            }
        }

        [Fact]
        public TestResult Test_FillCityDistrict_ShouldFillInitialDistricts()
        {
            try
            {
                var database = DataBase.getInstence();
                database.FillCityDistrict();
                int districtCount = database.GetDistrictCount();
                Assert.Equal(10, districtCount);
                return new TestResult(districtCount == 10, "Test_FillCityDistrict_ShouldFillInitialDistricts");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка в тесте Test_FillCityDistrict_ShouldFillInitialDistricts: {ex.Message}");
                return new TestResult(false, "Test_FillCityDistrict_ShouldFillInitialDistricts");
            }
        }

        [Fact]
        public TestResult Test_DeleteTable_ShouldDeleteData()
        {
            try
            {
                var database = DataBase.getInstence();
                database.CreateTable();
                database.DeleteTable();
                int orderCount = database.GetOrderCount();
                int districtCount = database.GetDistrictCount();
                Assert.Equal(0, orderCount);
                Assert.Equal(0, districtCount);
                return new TestResult(orderCount == 0 && districtCount == 0, "Test_DeleteTable_ShouldDeleteData");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка в тесте Test_DeleteTable_ShouldDeleteData: {ex.Message}");
                return new TestResult(false, "Test_DeleteTable_ShouldDeleteData");
            }
        }

        [Fact]
        public TestResult Test_InsertData_ShouldInsertMultipleOrders()
        {
            try
            {
                var database = DataBase.getInstence();
                List<InputData> data = new List<InputData> {
                    new InputData { weight = 30.5, district = 1, deliveryTime = DateTime.Now },
                    new InputData { weight = 45.3, district = 2, deliveryTime = DateTime.Now }
                };

                database.FillCityDistrict();
                database.InsertData(data);
                DataTable ordersTable = database.MakeDataTable();
                var districtNames = database.SelectDistricts();
                bool allOrdersExist = true;

                foreach (var inputData in data)
                {
                    bool orderExists = false;

                    foreach (DataRow row in ordersTable.Rows)
                    {
                        if ((double)row["Weight"] == inputData.weight &&
                            (string)row["CityDistrict"] == districtNames[Convert.ToInt64(inputData.district)] && // Используем словарь для получения названия района
                            ((DateTime)row["DeliveryDateTime"]).ToString("yyyy-MM-dd HH:mm:ss") == inputData.deliveryTime.ToString("yyyy-MM-dd HH:mm:ss"))
                        {
                            orderExists = true;
                            break;
                        }
                    }

                    if (!orderExists)
                    {
                        allOrdersExist = false;
                        break;
                    }
                }

                Assert.True(allOrdersExist);
                return new TestResult(allOrdersExist, "Test_InsertData_ShouldInsertMultipleOrders");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка в тесте Test_InsertData_ShouldInsertMultipleOrders: {ex.Message}");
                return new TestResult(false, "Test_InsertData_ShouldInsertMultipleOrders");
            }
        }

        private StringBuilder ShowResults(List<TestResult> results)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine("Результаты тестов:");
            foreach (var result in results)
            {
                string status = result.isPassed ? "пройден." : "не пройден.";
                stringBuilder.AppendLine($"{result.Name}: Тест {status}");
            }

            return stringBuilder;
        }
    }
}
