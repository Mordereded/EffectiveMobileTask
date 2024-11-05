using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;


namespace Project_test_task.Uility
{
    internal static class Config
    {
        static DatabaseSettings databaseSettings;
        static ConfigLoader configLoader;
        static Config()
        {
            configLoader = new ConfigLoader();

            databaseSettings = configLoader.LoadDataBaseConfig();
        }
        public static string ConfigConnectionDataBaseSettings()
        {
            return databaseSettings.ConnectionString;
        }
        public static string ConfigFileLogger()
        {
            return databaseSettings.Path;
        }
        public static string ConfigResultFilePath()
        {
            return databaseSettings.ResultFilePath;
        }

    }
    public class ConfigLoader
    {
        public DatabaseSettings LoadDataBaseConfig(string filePath = "Config\\Config.json")
        {
            try
            {
                if (!File.Exists(filePath))
                    throw new FileNotFoundException($"Ошибка конфигурации: {filePath}");

                var json = File.ReadAllText(filePath);
                var jsonObject = JObject.Parse(json);
                string connectionString = jsonObject["Database"]?["ConnectionString"]?.ToString() ?? "Data Source=dilivery.db;Version=3;";
                string loggerFilePath = jsonObject["FilePath"]?["LoggerFilePath"]?.ToString() ?? "..\\..\\..\\LoggerTextFile\\log.txt";
                string resultFilePath = jsonObject["FilePath"]?["ResultFilePath"]?.ToString() ?? "..\\..\\..\\ResultingFile\\result.txt";
                return new DatabaseSettings(connectionString, loggerFilePath, resultFilePath);

            }
            catch (Exception ex) 
            {
                //MessageBox.Show($"При загрузке конфигурации произошла ошбика -> {ex.Message}");
                return new DatabaseSettings("Data Source=dilivery.db;Version=3;", "..\\..\\..\\LoggerTextFile\\log.txt", "..\\..\\..\\ResultingFile\\result.txt");
            }
            }
    }
    public class DatabaseSettings
    {
        public DatabaseSettings(string connectionString, string path,string resultpath)
        {
            ConnectionString = connectionString;
            Path = path;
            ResultFilePath = resultpath;
        }

        public string ConnectionString { get; set; }
        public string Path { get; set; }
        public string ResultFilePath { get; set; }
    }
}