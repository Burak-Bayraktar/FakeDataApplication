using FakeDataApplication.Entity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Xml.Serialization;

namespace FakeDataApplication.Business
{
    public class FluentTechnologicalDevice : FluentBase, IFluentBase
    {
        Random randomNumber = new Random();
        int id = 0;
        TechnologicalDevice techDevice;
        TechnologicalDevice[] techDevices;
        int _requestedData = 1; // default requested value.
        string removedWhiteSpaces, capitalizedFirstLetter;

        public FluentTechnologicalDevice()
        {
            id = randomNumber.Next(0, 100);
            techDevice = new TechnologicalDevice();
        }

        public FluentTechnologicalDevice(int requestedData)
        {
            _requestedData = requestedData;
            if (_requestedData == 0 || _requestedData > 1000)
            {
                Console.WriteLine("İzin verilmeyen sahte veri oluşum isteği. \nTalep edilen veri miktarı 0'a eşit veya 1000'den büyük olamaz.");
                System.Environment.Exit(0);
            }
            id = randomNumber.Next(0, 100);
            techDevices = InitializeArray<TechnologicalDevice>(requestedData);
        }

        public FluentTechnologicalDevice FluentLaptop()
        {
            if (_requestedData > 1)
            {
                var resultArr = GetDataList<Laptop>(_requestedData);
                for (int i = 0; i < resultArr.Length; i++)
                {
                    techDevices[i].Laptop = resultArr[i].laptop;
                }

                return this;
            }

            int totalData = GetDataLength("Laptop");
            id = randomNumber.Next(1, totalData);
            var result = GetData<Laptop>(id);
            techDevice.Laptop = result.laptop;
            return this;
        }
        public FluentTechnologicalDevice FluentTelevision()
        {
            if(_requestedData > 1)
            {
                var resultArr = GetDataList<Television>(_requestedData);
                for (int i = 0; i < resultArr.Length; i++)
                {
                    techDevices[i].Television = resultArr[i].television;
                }
                return this;
            }

            int totalData = GetDataLength("Television");
            id = randomNumber.Next(1, totalData);
            var result = GetData<Television>(id);
            techDevice.Television = result.television;
            return this;
        }
        public FluentTechnologicalDevice FluentTablet()
        {
            if (_requestedData > 1)
            {
                var resultArr = GetDataList<Tablet>(_requestedData);
                for (int i = 0; i < resultArr.Length; i++)
                {
                    techDevices[i].Tablet = resultArr[i].tablet;
                }
                return this;
            }
            int totalData = GetDataLength("Tablet");
            id = randomNumber.Next(1, totalData);
            var result = GetData<Tablet>(id);
            techDevice.Tablet = result.tablet;
            return this;
        }
        public FluentTechnologicalDevice FluentTelephone()
        {
            if (_requestedData > 1)
            {
                var resultArr = GetDataList<TelephoneBrand>(_requestedData);
                for (int i = 0; i < resultArr.Length; i++)
                {
                    techDevices[i].Telephone = resultArr[i].telephone_brand;
                }
                return this;
            }
            int totalData = GetDataLength("TelephoneBrand");
            id = randomNumber.Next(1, totalData);
            var result = GetData<TelephoneBrand>(id);
            techDevice.Telephone = result.telephone_brand;
            return this;
        }
        public string CreateAsJSON(string folderName)
        {
            var options = new JsonSerializerOptions
            {
                IgnoreNullValues = true,
                WriteIndented = true
            };


            var fileName = $"{folderName}\\FakeData{DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString()}.json";
            var s = "";
            if (_requestedData > 1)
                s = JsonSerializer.Serialize(techDevices, options);
            else
                s = JsonSerializer.Serialize(techDevice, options);


            File.WriteAllText(fileName, s);
            return s;

        }

        public void CreateAsXML(string folderName)
        {
            var fileName = $"{folderName}\\FakeData{DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString()}.xml";
            using (var stream = new FileStream(fileName, FileMode.Create))
            {
                if (_requestedData > 1)
                {
                    XmlSerializer XML = new XmlSerializer(typeof(Person[]));
                    XML.Serialize(stream, techDevices);

                }
                else
                {
                    XmlSerializer XML = new XmlSerializer(typeof(Person));
                    XML.Serialize(stream, techDevice);
                }
            }
        }
    }
}
