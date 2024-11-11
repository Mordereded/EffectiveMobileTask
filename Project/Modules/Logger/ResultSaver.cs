using Project_test_task.Uility;
using System.Data;

namespace Project.Modules.Logger
{
    public interface IResultSaver
    {
        void SaveResult(DataTable result);
    }

    public class FileResultSaver : IResultSaver
    {
        private string filePath;
        public FileResultSaver()
        {
            filePath = Config.ConfigResultFilePath();
        }
        public FileResultSaver(string filePath)
        {
            this.filePath = filePath;
        }
        public void SetPath(string path)
        {
            filePath = path;
        }
        public void SaveResult(DataTable result)
        {
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                foreach (DataRow row in result.Rows)
                {
                    writer.WriteLine($"OrderId: {row["OrderId"]}, Weight: {row["Weight"]}, DeliveryDateTime: {row["DeliveryDateTime"]}");
                }
            }
        }
    }


}
