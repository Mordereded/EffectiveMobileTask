using System.Data;
using Project.Modules.DataBase;

namespace Project_test_task.InternalTests;

public class InternalTests
{
    private List<string> testResults = new List<string>();
    public void RunAllTests()
    {
        Test_CreateTable_ShouldCreateTables();
        Test_InsertSingleData_ShouldInsertOrderSuccessfully();
        Test_FillCityDistrict_ShouldFillInitialDistricts();
        Test_DeleteTable_ShouldDeleteData();
        Test_InsertData_ShouldInsertMultipleOrders();

        Console.WriteLine("\nРезультаты тестов:");
        foreach (var result in testResults)
        {
            if (result.StartsWith("Тест не пройден"))
            {
                Console.ForegroundColor = ConsoleColor.Red;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Green;
            }

            Console.WriteLine(result);
        }
        Console.ResetColor(); 
    }
    

    private void Test_CreateTable_ShouldCreateTables()
    {
        Console.WriteLine("Запуск теста: Test_CreateTable_ShouldCreateTables...");
        var database = DataBase.getInstence();
        database.CreateTable();

        var tablesCreated = database.CheckTablesExist();
        if (tablesCreated)
        {
            testResults.Add("Тест Test_CreateTable_ShouldCreateTables пройден.");
        }
        else
        {
            testResults.Add("Тест Test_CreateTable_ShouldCreateTables не пройден.");
        }
    }
    private void Test_InsertSingleData_ShouldInsertOrderSuccessfully()
    {
        Console.WriteLine("Запуск теста: Test_InsertSingleData_ShouldInsertOrderSuccessfully...");
        var database = DataBase.getInstence();
        database.CreateTable();
        double weight = 25.5;
        int district = 1;
        DateTime deliveryTime = DateTime.Now;
        database.InsertSingleData(weight, district, deliveryTime);

        DataTable ordersTable = database.MakeDataTable();
        bool orderExists = false;

        // Проверяем, существует ли заказ в полученной таблице
        foreach (DataRow row in ordersTable.Rows)
        {
            if ((double)row["Weight"] == weight &&
                (DateTime)row["DeliveryDateTime"] == deliveryTime)
            {
                orderExists = true;
                break;
            }
        }
        if (orderExists)
        {
            testResults.Add("Тест Test_InsertSingleData_ShouldInsertOrderSuccessfully пройден.");
        }
        else
        {
            testResults.Add("Тест Test_InsertSingleData_ShouldInsertOrderSuccessfully не пройден.");
        }
    }
    private void Test_FillCityDistrict_ShouldFillInitialDistricts()
    {
        Console.WriteLine("Запуск теста: Test_FillCityDistrict_ShouldFillInitialDistricts...");
        var database = DataBase.getInstence();
        database.FillCityDistrict();

        int districtCount = database.GetDistrictCount();
        if (districtCount == 10)
        {
            testResults.Add("Тест Test_FillCityDistrict_ShouldFillInitialDistricts пройден.");
        }
        else
        {
            testResults.Add("Тест Test_FillCityDistrict_ShouldFillInitialDistricts не пройден.");
        }
    }
    private void Test_DeleteTable_ShouldDeleteData()
    {
        Console.WriteLine("Запуск теста: Test_DeleteTable_ShouldDeleteData...");
        var database = DataBase.getInstence();
        database.CreateTable();
        database.DeleteTable();

        int orderCount = database.GetOrderCount();
        int districtCount = database.GetDistrictCount();
        if (orderCount == 0 && districtCount == 0)
        {
            testResults.Add("Тест Test_DeleteTable_ShouldDeleteData пройден.");
        }
        else
        {
            testResults.Add("Тест Test_DeleteTable_ShouldDeleteData не пройден.");
        }
    }
    private void Test_InsertData_ShouldInsertMultipleOrders()
    {
        Console.WriteLine("Запуск теста: Test_InsertData_ShouldInsertMultipleOrders...");
        var database = DataBase.getInstence();
        List<InputData> data = [
            new InputData { weight = 30.5, district = 1, deliveryTime = DateTime.Now },
            new InputData { weight = 45.3, district = 2, deliveryTime = DateTime.Now }
        ];

        database.InsertData(data);

        int orderCount = database.GetOrderCount();
        if (orderCount == data.Count)
        {
            testResults.Add("Тест Test_InsertData_ShouldInsertMultipleOrders пройден.");
        }
        else
        {
            testResults.Add("Тест Test_InsertData_ShouldInsertMultipleOrders не пройден.");
        }
    }
}
