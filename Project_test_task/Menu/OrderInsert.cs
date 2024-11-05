using System;
using System.Collections.Generic;
using System.ComponentModel;

using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.ComponentModel.DataAnnotations;
using Project_test_task.Data;
using Project_test_task.Uility;
using Newtonsoft.Json.Linq;

namespace Project_test_task
{
    public partial class OrderInsert : Form
    {
        private readonly DataBase dataBase;
        private readonly CompositeLogger logger;
        public OrderInsert()
        {
            InitializeComponent();
            dateTimePicker1.Value = DateTime.Now;
            logger = CompositeLogger.Instance;
            dataBase = DataBase.getInstence();
            FillSelector();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!CheckVaild()) return;
            double weight = Convert.ToDouble(textboxWeight.Text);
            int cityDistrictId = comboboxDistrict.SelectedIndex + 1;
            var deliveryTime = dateTimePicker1.Value;

            Order order = new Order
                (weight, cityDistrictId, deliveryTime);
            if (!ValidateOrder(order)) return;
            dataBase.InsertSingleData(order.Weight, order.CityDistrict, order.DeliveryDateTime);
            logger.Log("Заказ был успешно добавлен");
            MessageBox.Show("Данные успешно добавлены.", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.Close();
        
        }

        private bool ValidateOrder(Order order) 
        {
            
            var validationErrors = order.ValidateOrder().ToList();
            if (validationErrors.Any())
            {
                string errors = string.Join(Environment.NewLine, validationErrors);
                MessageBox.Show(errors, "Ошибка валидации", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }
        private bool CheckVaild()
        {
            if (comboboxDistrict.SelectedIndex < 0)
            {
                MessageBox.Show("Пожалуйста, выберите район.", "Ошибка ввода", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (!double.TryParse(textboxWeight.Text, out double weight))
            {
                MessageBox.Show("Введите корректный вес (число).", "Ошибка ввода", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }

        private void FillSelector()
        {
            Dictionary<long,string> district = dataBase.SelectDistricts();
            foreach (var distr in district)
            {
                comboboxDistrict.Items.Add(distr.Value);
            }
        }


    }
    public class Order 
    {
        [Required(ErrorMessage = "Вес является обязательным полем.")]
        [Range(1, 100, ErrorMessage = "Вес должен быть в диапазоне от 1 до 100.")]
        public double Weight { get; set; }

        [Required(ErrorMessage = "Район является обязательным полем.")]
        public int CityDistrict { get; set; }

        [Required(ErrorMessage = "Дата и время доставки являются обязательными.")]
        public DateTime DeliveryDateTime { get; set; }

        public Order(double weight, int cityDistrict, DateTime deliveryDateTime)
        {
            Weight = weight;
            CityDistrict = cityDistrict;
            DeliveryDateTime = deliveryDateTime;
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
