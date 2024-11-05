

using System.ComponentModel.DataAnnotations;
using Project_test_task.Uility;
using Project_test_task.Data;

namespace Project_test_task
{
    public partial class InsertDistrict : Form
    {
        private readonly DataBase dataBase;
        private readonly CompositeLogger logger;
        public InsertDistrict()
        {
            InitializeComponent();
            logger = CompositeLogger.Instance;
            dataBase = DataBase.getInstence();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string districtName = textBox1.Text;

                // Проверяем валидацию и добавляем район
                AddDistrict(districtName);
            }
            catch (Exception ex)
            {
                HandleError(ex);
            }
        }

        private void AddDistrict(string districtName)
        {
            CityDistrict district = new CityDistrict(districtName);
            IEnumerable<string> validationErrors = district.ValidateOrder();

            if (validationErrors != null && validationErrors.Any())
            {
                ShowValidationErrors(validationErrors);
                return; 
            }
            dataBase.InsertDistrict(district.District);
            logger.Log("Район был успешно добавлен");
            MessageBox.Show("Данные успешно добавлены.", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.Close();
        }

        private void ShowValidationErrors(IEnumerable<string> validationErrors)
        {
            string errorMessage = string.Join(Environment.NewLine, validationErrors);
            MessageBox.Show(errorMessage, "Ошибка валидации", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void HandleError(Exception ex)
        {
            logger.Log($"Ошибка при добавлении района -> {ex.Message}");
            MessageBox.Show("Произошла ошибка при добавлении района.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            textBox1.Text = string.Empty; 
        }


    }
    public class CityDistrict
    {
        private const string DistrictPattern = @"^[А-Яа-яЁёA-Za-z\s\-]+$"; // Регулярное выражение для проверки только кириллицы, пробелов и дефисов

        [Required(ErrorMessage = "Название района является обязательным полем.")]
        [RegularExpression(DistrictPattern, ErrorMessage = "Название района может содержать только буквы, пробелы и дефисы.")]
        public string District { get; set; }

        public CityDistrict(string district)
        {
            this.District = district;
        }
        public IEnumerable<string> ValidateOrder()
        {
            var validationResults = new List<ValidationResult>();
            var validationContext = new ValidationContext(this);
            bool isValid = Validator.TryValidateObject(this, validationContext, validationResults, true);

            if (!isValid)
            {
                foreach (var validationResult in validationResults)
                {
                    yield return validationResult.ErrorMessage;
                }
            }
        }
    }
}
