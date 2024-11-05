using System;
using System.IO;
using System.Globalization;
using System.Text;

class CSVGenerator
{
    public static void Main()
    {
        
        string filePath = "district_data.csv";
        using (StreamWriter writer = new StreamWriter(filePath, false, new UTF8Encoding(true))) // Используем UTF-8 с BOM
        {
            writer.WriteLine("Weight,District,Value,DeliveryDateTime");
            Random random = new Random();
            double randomFloat = Math.Round(random.NextDouble() * 99 + 1, 2);
            for (int i = 0; i < 30; i++)
            {
                writer.WriteLine($"{Math.Round(random.NextDouble() * 99 + 1, 2).ToString("F2", CultureInfo.InvariantCulture)},Центральный,{DateTime.Now.AddMinutes(-i).ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture)}");
            }

            for (int i = 31; i < 35; i++)
            {
                writer.WriteLine($"{Math.Round(random.NextDouble() * 99 + 1, 2).ToString("F2", CultureInfo.InvariantCulture)},Центральный,{DateTime.Now.AddMinutes(-i).ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture)}");
            }
        }
        Console.WriteLine("CSV файл успешно создан!");
    }
}
