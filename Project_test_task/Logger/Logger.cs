

namespace Project_test_task.Uility
{
    public interface ILogger
    {
        void Log(string message);
    }


    public class ConsoleLogger : ILogger
    {
        public void Log(string message)
        {
            Console.WriteLine($"{DateTime.Now:yyyy-MM-dd HH:mm:ss} - {message}\n");
        }
    }
    public class FileLogger : ILogger
    {
        private string logFilePath;

        public FileLogger()
        {
            logFilePath = Config.ConfigFileLogger();
        }
        public void ChangeFilePath(string filePath)
        {
            logFilePath = filePath;
        }

        public void Log(string message)
        {
            using (StreamWriter writer = new StreamWriter(logFilePath, true))
            {
                writer.WriteLine($"{DateTime.Now:yyyy-MM-dd HH:mm:ss} - {message}\n");
            }
        }
    }
    public class FormLogger : ILogger
    {
        private RichTextBox? richTextBox;

        public FormLogger()
        {
            richTextBox = null;
        }
        public void SetRichTextBox(RichTextBox richTextBox)
        {
            this.richTextBox = richTextBox;
        }


        public void Log(string message)
        {
            if (richTextBox == null) return;
            if (richTextBox.InvokeRequired)
            {
                richTextBox.Invoke(new Action(() => AppendText(message)));
            }
            else
            {
                AppendText(message);
            }
        }

        private void AppendText(string message)
        {
            richTextBox.AppendText($"{DateTime.Now:yyyy-MM-dd HH:mm:ss} - {message}\n");
        }
    }
    public class CompositeLogger : ILogger
    {
        private static CompositeLogger? instance;
        private readonly List<ILogger> loggers;
        private CompositeLogger()
        {
            loggers = [];
        }
        public static CompositeLogger Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new CompositeLogger();
                }
                return instance; 
            }
        }

        public void AddLogger(ILogger logger)
    {
        if (!loggers.Any(l => l.GetType() == logger.GetType()) && loggers.Count < 3)
        {
            loggers.Add(logger);
        }
    }
    public void RemoveLogger(Type loggerType)
    {
        loggers.RemoveAll(logger => logger.GetType() == loggerType);
    }


    public void Log(string message)
    {
        foreach (var logger in loggers)
        {
            logger.Log(message);
        }
    }
    public bool Exists<T>() where T : ILogger
    {
        return loggers.Any(logger => logger is T);
    }

    public T GetLogger<T>() where T : ILogger
    {
        return loggers.OfType<T>().FirstOrDefault();
    }
    }

    public interface ILoggerFactory
    {
        ILogger CreateLogger(string type);
    }

    public class LoggerFactory : ILoggerFactory
    {
        public ILogger CreateLogger(string type)
        {
            return type switch
            {
                "Консоль" => new ConsoleLogger(),
                "Файл" => new FileLogger(),
                "Форма" => new FormLogger(),
                _ => throw new ArgumentException("Неизвестный тип логгера", nameof(type))
            };
        }
    }

}
