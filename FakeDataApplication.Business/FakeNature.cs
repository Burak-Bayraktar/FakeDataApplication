using FakeDataApplication.Entity;
using FakeDataApplication.Entity.Concrete;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Xml.Serialization;

namespace FakeDataApplication.Business
{
    public class FakeNature : FakeNatureBase, IFluentBase
    {
        Random randomNumber = new Random();
        int id = 0;
        Nature nature;
        Nature[] natures;
        int _requestedData = 1; // default requested value.
        public FakeNature()
        {
            id = randomNumber.Next(0, 100);
            nature = new Nature();

        }
        public FakeNature(int requestedData)
        {
            _requestedData = requestedData;
            CheckRequestedData(_requestedData);
            id = randomNumber.Next(0, 100);
            natures = InitializeArray<Nature>(requestedData);
        }

        public FakeNature FluentAnimal()
        {
            MessageToTheUser(MethodBase.GetCurrentMethod().Name);
            if (_requestedData > 1)
            {
                var resultArr = GetDataList<Animal>(_requestedData);
                for (int i = 0; i < resultArr.Length; i++)
                {
                    natures[i].Animal = resultArr[i].animal;
                }

                return this;
            }

            int totalData = GetDataLength("Animal");
            id = randomNumber.Next(1, totalData);
            var result = GetData<Animal>(id);
            nature.Animal= result.animal;
            return this;
        }

        public FakeNature FluentPlant()
        {
            MessageToTheUser(MethodBase.GetCurrentMethod().Name);
            if (_requestedData > 1)
            {
                var resultArr = GetDataList<Plant>(_requestedData);
                for (int i = 0; i < resultArr.Length; i++)
                {
                    natures[i].Plant = resultArr[i].plant;
                }

                return this;
            }

            int totalData = GetDataLength("Plant");
            id = randomNumber.Next(1, totalData);
            var result = GetData<Plant>(id);
            nature.Plant = result.plant;
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
            try
            {
                if (!Directory.Exists(folderName))
                {
                    Directory.CreateDirectory(folderName);
                }


                if (_requestedData > 1)
                    s = JsonSerializer.Serialize(natures, options);
                else
                    s = JsonSerializer.Serialize(nature, options);

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
                if (Directory.Exists(folderName))
                {
                    using (var stream = new FileStream(fileName, FileMode.Create))
                    {
                        if (_requestedData > 1)
                        {
                            XmlSerializer XML = new XmlSerializer(typeof(TechnologicalDevice[]));
                            XML.Serialize(stream, natures);

                        }
                        else
                        {
                            XmlSerializer XML = new XmlSerializer(typeof(TechnologicalDevice));
                            XML.Serialize(stream, nature);
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
