using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using Dapper;
using System.Reflection;
using FakeDataApplication.Business;
using System.Globalization;
using System.Text.Json;
using FakeDataApplication.Entity.Abstract;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.IO;
using System.Xml.Serialization;
using System.Security.Principal;
using System.Text.Encodings.Web;

namespace FakeDataApplication.Entity
{
    public class FakePerson : FakePersonBase, IFluentBase
    {
        Random randomNumber = new Random();
        int id = 0;
        Person person;
        Person[] persons;
        int _requestedData = 1; // default requested value.
        string removedWhiteSpaces, capitalizedFirstLetter;

        public FakePerson()
        {
            id = randomNumber.Next(0, 100);
            person = new Person();
        }

        public FakePerson(int requestedData)
        {
            _requestedData = requestedData;
            CheckRequestedData(_requestedData);
            id = randomNumber.Next(0, 100);
            persons = InitializeArray<Person>(requestedData);
        }
        /// <summary>
        /// Create Fake Name for specified gender.
        /// </summary>
        /// <typeparam name="T">Requested Gender name.</typeparam>
        /// <param name="gender"></param>
        /// <returns></returns>
        public FakePerson FakeName<T>(Gender gender) where T : IName
        {
            MessageToTheUser(MethodBase.GetCurrentMethod().Name);
            if (_requestedData > 1)
            {
                var methodInfo = typeof(FluentBase).GetMethod(nameof(FluentBase.GetDataList));
                var genericMethodInfo = methodInfo.MakeGenericMethod(typeof(T));
                T[] result = (T[])genericMethodInfo.Invoke(null, new object[] { _requestedData });

                for (int i = 0; i < result.Length; i++)
                {
                    var removedWhiteSpaces = RemoveFromWhiteSpaces(result[i].name);
                    var capitalizedFirstLetter = CapitalizeFirstLetterOfString(removedWhiteSpaces);
                    persons[i].Name = capitalizedFirstLetter;
                }
                return this;
            }

            int totalData = GetDataLength(typeof(T).Name);
            id = randomNumber.Next(1, totalData);
            if (gender == Gender.Man)
            {
                ManName result =  GetData<ManName>(id);
                var removedWhiteSpaces = RemoveFromWhiteSpaces(result.name);
                var capitalizedFirstLetter = CapitalizeFirstLetterOfString(removedWhiteSpaces);
                person.Name = capitalizedFirstLetter;
            }
            else
            {
                WomanName result =  GetData<WomanName>(id);
                var removedWhiteSpaces = RemoveFromWhiteSpaces(result.name);
                var capitalizedFirstLetter = CapitalizeFirstLetterOfString(removedWhiteSpaces);
                person.Name = capitalizedFirstLetter;
            }
            return this;
        }
        /// <summary>
        /// Rastgele karışık isim döndürür.
        /// </summary>
        /// <returns></returns>
        public FakePerson FakeName()
        {
            MessageToTheUser(MethodBase.GetCurrentMethod().Name);
            if (_requestedData > 1)
            {
                var woman = randomNumber.Next(1, _requestedData);
                var man = _requestedData - woman;
                WomanName[] womanNameList = GetDataList<WomanName>(woman);
                ManName[] manNameList = GetDataList<ManName>(man);
                IName[][] names = new IName[][] { womanNameList, manNameList };

                var allNames = MergeAndShuffleNames(names);

                for (int i = 0; i < allNames.Length; i++)
                {
                    removedWhiteSpaces = RemoveFromWhiteSpaces(allNames[i].name);
                    capitalizedFirstLetter = CapitalizeFirstLetterOfString(removedWhiteSpaces);
                    persons[i].Name = capitalizedFirstLetter;
                }
                return this;
            }

            var creatingNamesGender = randomNumber.Next(0, 2) == 0 ? typeof(ManName) : typeof(WomanName);
            int totalData = GetDataLength(creatingNamesGender.Name);
            id = randomNumber.Next(1, totalData);

            var methodInfo = typeof(FluentBase).GetMethod(nameof(FluentBase.GetData));
            var genericMethodInfo = methodInfo.MakeGenericMethod(creatingNamesGender);
            var result = (IName)genericMethodInfo.Invoke(null, new object[] { id });   
            removedWhiteSpaces = RemoveFromWhiteSpaces(result.name);
            capitalizedFirstLetter = CapitalizeFirstLetterOfString(removedWhiteSpaces);
            person.Name = capitalizedFirstLetter;

            return this;
        }
        public FakePerson FakeSurname()
        {
            MessageToTheUser(MethodBase.GetCurrentMethod().Name);
            string removedWhiteSpaces, capitalizedFirstLetter;
            if (_requestedData > 1)
            {
                Surname[] resultArr = GetDataList<Surname>(_requestedData);
                for (int i = 0; i < resultArr.Length; i++)
                {
                    removedWhiteSpaces = RemoveFromWhiteSpaces(resultArr[i].surname);
                    capitalizedFirstLetter = CapitalizeFirstLetterOfString(removedWhiteSpaces);
                    persons[i].Surname = capitalizedFirstLetter;
                }
                return this;
            }

            int totalData = GetDataLength("Surname");
            id = randomNumber.Next(1, totalData);
            var result =  GetData<Surname>(id);
            removedWhiteSpaces = RemoveFromWhiteSpaces(result.surname);
            capitalizedFirstLetter = CapitalizeFirstLetterOfString(removedWhiteSpaces);
            person.Surname = capitalizedFirstLetter;
            return this;
        }
        public FakePerson FakeTelephoneNumber()
        {
            MessageToTheUser(MethodBase.GetCurrentMethod().Name);
            if (_requestedData > 1)
            {
                TelephoneNumber[] resultArr = GetDataList<TelephoneNumber>(_requestedData);
                for (int i = 0; i < resultArr.Length; i++)
                {
                    persons[i].TelephoneNumber = resultArr[i].telephone_number;
                }
                return this;
            }
            int totalData = GetDataLength("TelephoneNumber");
            id = randomNumber.Next(1, totalData);
            var result =  GetData<TelephoneNumber>(id);
            person.TelephoneNumber = result.telephone_number;
            return this;
        }

        public FakePerson FakeIdentityNumber()
        {
            MessageToTheUser(MethodBase.GetCurrentMethod().Name);
            if (_requestedData > 1)
            {
                IdentityNumber[] resultArr = GetDataList<IdentityNumber>(_requestedData);
                for (int i = 0; i < resultArr.Length; i++)
                {
                    removedWhiteSpaces = RemoveFromWhiteSpaces(resultArr[i].tc);
                    capitalizedFirstLetter = CapitalizeFirstLetterOfString(removedWhiteSpaces);
                    persons[i].Id = capitalizedFirstLetter;
                }
                return this;
            }

            int totalData = GetDataLength("IdentityNumber");
            id = randomNumber.Next(1, totalData);
            var result =  GetData<IdentityNumber>(id);
            removedWhiteSpaces = RemoveFromWhiteSpaces(result.tc);
            capitalizedFirstLetter = CapitalizeFirstLetterOfString(removedWhiteSpaces);
            person.Id = capitalizedFirstLetter;
            return this;
        }

        public FakePerson FakeBirthPlace()
        {
            MessageToTheUser(MethodBase.GetCurrentMethod().Name);
            if (_requestedData > 1)
            {
                Province[] resultArr = GetDataList<Province>(_requestedData);
                for (int i = 0; i < resultArr.Length; i++)
                {
                    removedWhiteSpaces = RemoveFromWhiteSpaces(resultArr[i].province);
                    capitalizedFirstLetter = CapitalizeFirstLetterOfString(removedWhiteSpaces);
                    persons[i].BirthPlace = capitalizedFirstLetter;
                }
                return this;
            }

            int totalData = GetDataLength("Province");
            id = randomNumber.Next(1, totalData);
            var result =  GetData<Province>(id);
            removedWhiteSpaces = RemoveFromWhiteSpaces(result.province);
            capitalizedFirstLetter = CapitalizeFirstLetterOfString(removedWhiteSpaces);
            person.BirthPlace = capitalizedFirstLetter;
            return this;
        }
        public FakePerson FakeEMail()
        {
            MessageToTheUser(MethodBase.GetCurrentMethod().Name);
            string[] emailDomains = new string[] { "@gmail.com", "@hotmail.com", "@yahoo.com", "@yandex.com", "@icloud.com", "@outlook.com" };
            int randomNum = 0;
            string fakeEmail = string.Empty;
            if (_requestedData > 1)
            {
                for (int i = 0; i < _requestedData; i++)
                {
                    randomNum = randomNumber.Next(1, emailDomains.Length);
                    fakeEmail = persons[i].Name.ToLower() + persons[i].Surname.ToLower() + emailDomains[randomNum];
                    fakeEmail = ReplaceTurkishCharacter(fakeEmail);
                    persons[i].EMail = fakeEmail;
                }
                return this;
            }

            randomNum = randomNumber.Next(1, emailDomains.Length);
            if (!string.IsNullOrEmpty(person.Name) && !string.IsNullOrEmpty(person.Surname))
            {
                fakeEmail = person.Name.ToLower() + person.Surname.ToLower() + emailDomains[randomNum];
                fakeEmail = ReplaceTurkishCharacter(fakeEmail);
                person.EMail = fakeEmail;
                return this;
            }

            return this;
        }
        public FakePerson FakeAddress()
        {
            MessageToTheUser(MethodBase.GetCurrentMethod().Name);
            Type[] tableNames = new Type[] { typeof(Neighborhood), typeof(Street), typeof(District), typeof(Province) };
            List<IEntity> addressTypes = new List<IEntity>();
            string fakeAddress;
            if (_requestedData > 1)
            {
                List<IEntity[]> addressTypesArr = new List<IEntity[]>();
                addressTypes.Clear();
                for (int j = 0; j < tableNames.Length; j++)
                {
                    var methodInfo = typeof(FluentBase).GetMethod(nameof(FluentBase.GetDataList));
                    var genericMethodInfo = methodInfo.MakeGenericMethod(tableNames[j]);
                    var result = (IEntity[])genericMethodInfo.Invoke(null, new object[] { _requestedData });
                    addressTypesArr.Add(result);
                }

                int neighborhoodLength = addressTypesArr[0].Length;
                int streetLength = addressTypesArr[1].Length;
                int districtLength = addressTypesArr[2].Length;
                int provinceLength = addressTypesArr[3].Length;
                for (int i = 0; i < _requestedData; i++)
                {
                    var t = addressTypesArr;
                    fakeAddress = CreateFullAddress(
                                        (Neighborhood)addressTypesArr[0][randomNumber.Next(1, neighborhoodLength)],
                                        (Street)addressTypesArr[1][randomNumber.Next(1, streetLength)],
                                        (District)addressTypesArr[2][randomNumber.Next(1, districtLength)],
                                        (Province)addressTypesArr[3][randomNumber.Next(1, provinceLength)]
                                    );

                    persons[i].Address = fakeAddress;
                }
                return this;
            }

            for (int i = 0; i < tableNames.Length; i++)
            {
                int totalData = GetDataLength(tableNames[i].Name);
                id = randomNumber.Next(1, totalData);
                var methodInfo = typeof(FluentBase).GetMethod(nameof(FluentBase.GetData));
                var genericMethodInfo = methodInfo.MakeGenericMethod(tableNames[i]);
                var result = (IEntity)genericMethodInfo.Invoke(null, new object[] { id });
                addressTypes.Add(result);
            }

            fakeAddress = CreateFullAddress((Neighborhood)addressTypes[0],
                             (Street)addressTypes[1],
                             (District)addressTypes[2],
                             (Province)addressTypes[3]);

            person.Address = fakeAddress;

            return this;
        }
        public FakePerson FakeDepartment()
        {
            MessageToTheUser(MethodBase.GetCurrentMethod().Name);
            if (_requestedData > 1)
            {
                Department[] resultArr = GetDataList<Department>(_requestedData);
                for (int i = 0; i < resultArr.Length; i++)
                {
                    persons[i].Department = resultArr[i].department;
                }
                return this;
            }

            int totalData = GetDataLength("Department");
            id = randomNumber.Next(1, totalData);
            var result =  GetData<Department>(id);
            person.Department = result.department;
            return this;
        }
        public FakePerson FakeHighSchool()
        {
            MessageToTheUser(MethodBase.GetCurrentMethod().Name);
            string fixedResult = string.Empty;
            if (_requestedData > 1)
            {
                HighSchool[] resultArr = GetDataList<HighSchool>(_requestedData);
                for (int i = 0; i < resultArr.Length; i++)
                {
                    fixedResult = resultArr[i].high_school.Substring(7, resultArr[i].high_school.Length - 7);
                    persons[i].HighSchool = fixedResult;
                }
                return this;
            }
            int totalData = GetDataLength("HighSchool");
            id = randomNumber.Next(1, totalData);
            var result =  GetData<HighSchool>(id);
            fixedResult = result.high_school.Substring(7, result.high_school.Length - 7);
            person.HighSchool = fixedResult;
            return this;
        }
        public FakePerson FakeUniversity()
        {
            MessageToTheUser(MethodBase.GetCurrentMethod().Name);
            if (_requestedData > 1)
            {
                University[] resultArr = GetDataList<University>(_requestedData);
                for (int i = 0; i < resultArr.Length; i++)
                {
                    persons[i].University = resultArr[i].university;
                }
                return this;
            }

            int totalData = GetDataLength("University");
            id = randomNumber.Next(1, totalData);
            var result =  GetData<University>(id);
            person.University = result.university;
            return this;
        }
        public FakePerson FakeSalary()
        {
            MessageToTheUser(MethodBase.GetCurrentMethod().Name);
            int num;
            if(_requestedData > 1)
            {
                for (int i = 0; i < _requestedData; i++)
                {
                    num = randomNumber.Next(2000, 20000);
                    if (num % 5 != 0)
                    {
                        var kalan = num % 5;
                        num = num - kalan;
                    }
                    persons[i].Salary = num;

                }
                return this;
            }

            num = randomNumber.Next(2000, 20000);
            if (num % 5 != 0)
            {
                var kalan = num % 5;
                num = num - kalan;
            }

            person.Salary = num;
            return this;
        }

        /// <summary>
        /// Creates Hobby array for each Person.
        /// </summary>
        /// <param name="requestedHobbyNums">
        /// Requested hobby numbers for each Person.
        /// If it is empty, 3 hobbies will created for each Person.
        /// </param>
        /// <returns></returns>
        public FakePerson FakeHobbies(params int[] requestedHobbyNums)
        {
            MessageToTheUser(MethodBase.GetCurrentMethod().Name);
            for (int i = 0; i < _requestedData; i++)
            {
                int s, requested;
                if (requestedHobbyNums.Length == 0)
                    requested = 3;
                else
                {
                    s = randomNumber.Next(0, requestedHobbyNums.Length);
                    requested = requestedHobbyNums[s];
                }
                var result = GetDataList<Hobby>(requested);
                List<string> hobbyList = new List<string>();
                for (int j = 0; j < requested; j++)
                {
                    hobbyList.Add(result[j].hobby);
                    if (j == requested - 1)
                    {
                        if (_requestedData == 1)
                        {
                            person.Hobbies = hobbyList.ToArray();
                        }
                        else
                        {
                            persons[i].Hobbies = hobbyList.ToArray();
                            Console.WriteLine($"Hobby for {i+1}.{this.GetType().Name} created.");
                            ClearLine();
                        }

                    }
                }
            }  
            return this;
        }
        public FakePerson FakeDrivingLicense()
        {
            MessageToTheUser(MethodBase.GetCurrentMethod().Name);
            int totalData = GetDataLength("DrivingLicence");
            if (_requestedData > 1)
            {    
                DrivingLicence[] resultArr = GetDataList<DrivingLicence>(_requestedData);
                for (int i = 0; i < _requestedData; i++)
                {
                    id = randomNumber.Next(1, resultArr.Length);
                    persons[i].DrivingLicense = resultArr[id].class_name;
                }
                return this;
            }

            id = randomNumber.Next(1, totalData);
            var result = GetData<DrivingLicence>(id);
            person.DrivingLicense = result.class_name;
            return this;
        }

        public FakePerson FakePersonalCar()
        {
            MessageToTheUser(MethodBase.GetCurrentMethod().Name);
            int totalData = GetDataLength("Car");
            if (_requestedData > 1)
            {
                Car[] resultArr = GetDataList<Car>(_requestedData);
                for (int i = 0; i < _requestedData; i++)
                {
                    id = randomNumber.Next(0, resultArr.Length);
                    persons[i].PersonalCar = resultArr[id].brand_model;
                }
                return this;
            }

            id = randomNumber.Next(1, totalData);
            var result = GetData<Car>(id);
            person.PersonalCar = result.brand_model;
            return this;
        }

        /// <summary>
        /// Creates a JSON file and saves the file to the specified folder
        /// </summary>
        /// <param name="folderName">folder format: D:\FolderFoo\FolderBar</param>
        /// <returns></returns>
        public string CreateAsJSON(string folderName)
        {
            var options = new JsonSerializerOptions
            {
                Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
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

                var fileName = $"{folderName}\\FakeData_{this.GetType().Name}_{DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString()}.json";
                if (_requestedData > 1)
                    s = JsonSerializer.Serialize(persons, options);
                else
                    s = JsonSerializer.Serialize(person, options);


                File.WriteAllText(fileName, s);
                var k = File.ReadAllText(fileName, Encoding.GetEncoding("windows-1254"));

                Console.WriteLine($"***************************\nJSON file includes {_requestedData} {this.GetType().Name} created at {folderName}\n****************************\n");
            }
            catch (DirectoryNotFoundException ex)
            {
                Console.WriteLine("***************************\nError!\nThe specified file folder couldn't found. \nMake sure that you passed valid folderName parameter.\n");
            }
            catch (ArgumentException)
            {
                Console.WriteLine("***************************\nError!\n\"folderName\" parameter cannot be empty. \nMake sure that you passed valid folderName parameter.\n");
            }

            return s;
        }
        /// <summary>
        /// Creates an XML file and saves the file to the specified folder
        /// </summary>
        /// <param name="folderName">folder format: D:\FolderFoo\FolderBar</param>
        public void CreateAsXML(string folderName)
        {
            var fileName = $"{folderName}\\FakeData_{this.GetType().Name}_{DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString()}.xml";
            try
            {
                if (Directory.Exists(folderName))
                {
                    using (var stream = new FileStream(fileName, FileMode.Create))
                    {
                        if (_requestedData > 1)
                        {
                            XmlSerializer XML = new XmlSerializer(typeof(Person[]));
                            XML.Serialize(stream, persons);

                        }
                        else
                        {
                            XmlSerializer XML = new XmlSerializer(typeof(Person));
                            XML.Serialize(stream, person);
                        }
                        Console.WriteLine($"***************************\nXML file includes {_requestedData} {this.GetType().Name} created at {folderName}\n****************************\n");
                    }
                }
            }
            catch (DirectoryNotFoundException)
            {
                Console.WriteLine("***************************\nError!\nThe specified file folder couldn't found. \nMake sure that you passed valid folderName parameter.\n");
            }
            catch (ArgumentException)
            {
                Console.WriteLine("***************************\nError!\n\"folderName\" parameter cannot be empty. \nMake sure that you passed valid folderName parameter.\n");
            }

        }
    }
}