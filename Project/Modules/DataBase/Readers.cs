using System.Text.Json;
using Project.Modules.Logger;

namespace Project.Modules.DataBase
{
    interface DataBaseFillAll
    {
        void FillAll();
    }
    interface DataBaseFillItems
    {
        void Fill(int items);
    }

    interface IDataFileReader
    {
        List<InputData> ReadData();
    }
    /// <summary>
    /// Чтение из CSV
    /// </summary>
    class CsvDataFileReader : IDataFileReader
    {
        private string filePath;
        readonly DataBase dataBase;
        public CsvDataFileReader(string filePath)
        {
            this.filePath = filePath;
            dataBase = DataBase.getInstence();
        }
        public List<InputData> ReadData()
        {
            var logger = CompositeLogger.Instance;
            var dataList = new List<InputData>();

            try
            {
                using (var reader = new StreamReader(filePath))
                {
                    if (!reader.EndOfStream)
                    {
                        reader.ReadLine();
                    }

                    while (!reader.EndOfStream)
                    {
                        var line = reader.ReadLine();
                        if (!string.IsNullOrWhiteSpace(line))
                        {
                            var inputData = ParseLine(line); // Вызов нужного метода
                            dataList.Add(inputData);
                        }
                    }
                }
                return dataList;
            }
            catch (Exception ex)
            {
                logger.Log($"Ошибка при чтении CSV: {ex.Message}");
                return new List<InputData>();
            }
        }

        // Метод для парсинга строки CSV в объект InputData
        private InputData ParseLine(string line) //Создание/Определение функции
        {
            //Тело функции - то что делает функция
            var values = line.Split(',');
            var weight = float.Parse(values[0]);
            var districtName = values[1];
            var deliveryTime = DateTime.Parse(values[2]);

            int districtId = GetId(districtName); // Используем GetId для поиска или добавления района

            return new InputData
            {
                weight = weight,
                district = districtId,
                deliveryTime = deliveryTime
            };
        }

        private int GetId(string districtName)
        {
            int districtId = dataBase.GetDistrictIdByName(districtName);
            if (districtId == -1)
            {
                districtId = dataBase.InsertDistrict(districtName);
            }
            return districtId;
        }


    }
    /// <summary>
    /// Чтение из Json
    /// </summary>
    class JsonDataFileReader : IDataFileReader
    {
        private string filePath;
        public JsonDataFileReader(string filePath)
        {
            this.filePath = filePath;
        }
        public List<InputData> ReadData()
        {
            var logger = CompositeLogger.Instance;
            var dataList = new List<InputData>();
            try
            {
                var jsonData = File.ReadAllText(filePath);
                dataList = JsonSerializer.Deserialize<List<InputData>>(jsonData);
                return dataList;
            }
            catch (Exception ex)
            {
                logger.Log($"Ошибка при чтении JSON: {ex.Message}");
                return new List<InputData>();
            }
        }
    }
    /// <summary>
    /// Фабрика для считывания 
    /// </summary>
    class DataFileReaderFactory
    {
        public static IDataFileReader? GetFileReader(string filePath)
        {
            var logger = CompositeLogger.Instance;
            if (string.IsNullOrWhiteSpace(filePath))
            {
                logger.Log("Ошбика при чтении файла, путь к файлу не корректне");
                return default;
            }
            try
            {
                string? extension = Path.GetExtension(filePath).ToLower().TrimStart('.');
                return extension switch
                {
                    "csv" => new CsvDataFileReader(filePath),
                    "json" => new JsonDataFileReader(filePath),
                    _ => throw new NotSupportedException($"Формат {extension} не поддерживается")
                };
            }
            catch (Exception ex)
            {
                logger.Log(ex.Message);
                return default;
            }

        }
    }

    /// <summary>
    /// Заполнение базы данных из файла
    /// </summary>
    class DataBaseFillWithFile : DataBaseFillAll
    {
        readonly private DataBase dataBase;
        readonly private string fileName;
        public DataBaseFillWithFile(DataBase dataBase, string fileName)
        {
            this.dataBase = dataBase;
            this.fileName = fileName;
        }
        public void FillAll()
        {
            List<InputData> dataFromFile = ReadDataFromFile(fileName);
            dataBase.InsertData(dataFromFile);
        }
        private List<InputData> ReadDataFromFile(string fileName)
        {
            //Создание фабрики на основании формата файла
            var fileReader = DataFileReaderFactory.GetFileReader(fileName);
            if (fileReader == null)
            {
                return new List<InputData>();
            }
            return fileReader.ReadData();
        }
    }

    /// <summary>
    /// Заполнение базы данных с помощью генератора
    /// </summary>
    class DataBaseFillWithGenerator : DataBaseFillItems
    {
        readonly private DataBase dataBase;
        public DataBaseFillWithGenerator(DataBase dataBase)
        {
            this.dataBase = dataBase;
        }

        public void Fill(int items)
        {
            List<InputData> data = new List<InputData>();
            GenerateDataDirector generateDataDirector = new GenerateDataDirector(new GenerateDataBuilder());
            dataBase.InsertData(generateDataDirector.GenerateFullDataList(items));
        }
    }
    class DataBaseFillItemsCountExecuter
    {
        private DataBaseFillItems dataBaseFiller;
        public DataBaseFillItemsCountExecuter(DataBaseFillItems dataBaseFiller)
        {
            this.dataBaseFiller = dataBaseFiller;
        }
        public void Execute(int itemsCount)
        {
            dataBaseFiller.Fill(itemsCount);
        }

    }
    class DataBaseFillAllItemsExecuter
    {
        private DataBaseFillAll dataBaseFiller;
        public DataBaseFillAllItemsExecuter(DataBaseFillAll dataBaseFiller)
        {
            this.dataBaseFiller = dataBaseFiller;
        }
        public void Execute()
        {
            dataBaseFiller.FillAll();
        }

    }
}
