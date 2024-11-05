using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project_test_task.Data;

namespace Project_test_task.Generator
{
    class GenerateDataBuilder
    {
        private InputData InputData;
        private Random random;
        public GenerateDataBuilder()
        {
            this.InputData = new InputData();
            this.random = new Random();
        }
        public GenerateDataBuilder GenerateOrderId()
        {
            InputData.orderId = Guid.NewGuid().ToString();
            return this;
        }
        public GenerateDataBuilder GenerateOrderWeight()
        {
            InputData.weight = Math.Round((random.NextDouble() * 99 + 1), 2);
            return this;
        }
        public GenerateDataBuilder GenerateOrderDistrict()
        {

            DataBase dataBase = DataBase.getInstence();
            var dict = dataBase.SelectDistricts();
            InputData.district = random.Next(dict.Count);
            return this;
        }
        public GenerateDataBuilder GenerateOrderDeliveryTime()
        {
            InputData.deliveryTime = DateTime.Now.AddMinutes(random.Next(1, 1440));
            return this;
        }
        public InputData GetFinalObject()
        {
            return InputData;
        }
        public void New()
        {
            InputData = new InputData();
        }
    }

    class GenerateDataDirector
    {
        private GenerateDataBuilder generateDataBuilder;
        public GenerateDataDirector(GenerateDataBuilder generateDataBuilder)
        {
            this.generateDataBuilder = generateDataBuilder;
        }
        public InputData GenerateFullData()
        {
            RenewGenerateDataBuilder();
            return generateDataBuilder.GenerateOrderId()
                .GenerateOrderWeight()
                .GenerateOrderDeliveryTime()
                .GenerateOrderDistrict()
                .GetFinalObject();
        }
        public InputData GenerateWeightDistrictDeliveryTime()
        {
            RenewGenerateDataBuilder();
            return generateDataBuilder.GenerateOrderWeight()
                .GenerateOrderDistrict()
                .GenerateOrderDeliveryTime()
                .GetFinalObject();
        }
        public InputData GenerateIdDeliverytimeWeight()
        {
            RenewGenerateDataBuilder();
            return generateDataBuilder.GenerateOrderId()
                .GenerateOrderWeight()
                .GenerateOrderDeliveryTime()
                .GetFinalObject();
        }
        public List<InputData> GenerateFullDataList(int itemsCount)
        {
            List<InputData> result = new List<InputData>();
            for(int i = 0; i < itemsCount; ++i)
            {
                RenewGenerateDataBuilder();
                result.Add(GenerateWeightDistrictDeliveryTime());
            }
            return result;
        }
        private void RenewGenerateDataBuilder()
        {
            generateDataBuilder.New();
        }
    }


    public struct InputData
    {
        public string? orderId;
        public double weight;
        public int? district;
        public DateTime deliveryTime; 
    }

}
