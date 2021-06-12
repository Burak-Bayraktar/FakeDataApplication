using FakeDataApplication.Entity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Xml.Serialization;

namespace FakeDataApplication.Business
{
    public class FakeTechnologicalDevice : FakeTechnologicalDeviceBase, IFluentBase
    {
        Random randomNumber = new Random();
        int id = 0;
        TechnologicalDevice techDevice;
        TechnologicalDevice[] techDevices;
        int _requestedData = 1; // default requested value.

        public FakeTechnologicalDevice()
        {
            id = randomNumber.Next(0, 100);
            techDevice = new TechnologicalDevice();
        }

        public FakeTechnologicalDevice(int requestedData)
        {
            _requestedData = requestedData;
            CheckRequestedData(_requestedData);
            id = randomNumber.Next(0, 100);
            techDevices = InitializeArray<TechnologicalDevice>(requestedData);
        }

        public FakeTechnologicalDevice FluentLaptop()
        {
            MessageToTheUser(MethodBase.GetCurrentMethod().Name);
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
        public FakeTechnologicalDevice FluentTelevision()
        {
            MessageToTheUser(MethodBase.GetCurrentMethod().Name);
            if (_requestedData > 1)
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
        public FakeTechnologicalDevice FluentTablet()
        {
            MessageToTheUser(MethodBase.GetCurrentMethod().Name);
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
        public FakeTechnologicalDevice FluentTelephone()
        {
            MessageToTheUser(MethodBase.GetCurrentMethod().Name);
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


            var s = "";
            try
            {
                if (!Directory.Exists(folderName))
                {
                    Directory.CreateDirectory(folderName);
                }

                var fileName = $"{folderName}\\FakeData{DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString()}.json";
                if (_requestedData > 1)
                    s = JsonSerializer.Serialize(techDevices, options);
                else
                    s = JsonSerializer.Serialize(techDevice, options);

                File.WriteAllText(fileName, s);
                Console.WriteLine($"***************************\nJSON file includes {_requestedData} {this.GetType().Name} created at {folderName}\n****************************\n");
            }
            catch (DirectoryNotFoundException)
            {
                Console.WriteLine("***************************\nError!\nThe specified file folder couldn't found. \nMake sure that you passed valid folderName parameter.");
            }
            return s;
        }

        public void CreateAsXML(string folderName)
        {
            var fileName = $"{folderName}\\FakeData{DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString()}.xml";

            try
            {
                if(Directory.Exists(folderName))
                {
                    using (var stream = new FileStream(fileName, FileMode.Create))
                    {
                        if (_requestedData > 1)
                        {
                            XmlSerializer XML = new XmlSerializer(typeof(TechnologicalDevice[]));
                            XML.Serialize(stream, techDevices);

                        }
                        else
                        {
                            XmlSerializer XML = new XmlSerializer(typeof(TechnologicalDevice));
                            XML.Serialize(stream, techDevice);
                        }
                        Console.WriteLine($"***************************\nXML file includes {_requestedData} {this.GetType().Name} created at {folderName}\n****************************\n");
                    }
                }
            }
            catch (DirectoryNotFoundException)
            {
                Console.WriteLine("***************************\nError!\nThe specified file folder couldn't found. \nMake sure that you passed valid folderName parameter.\n");
            }
        }
    }
}