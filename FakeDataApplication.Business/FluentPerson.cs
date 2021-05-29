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

namespace FakeDataApplication.Entity
{
    public class FluentPerson : FluentBase, IFluentBase
    {
        Random randomNumber = new Random();
        int id = 0;
        Person person;
        Person[] persons;
        int _requestedData = 1; // default requested value.
        string removedWhiteSpaces, capitalizedFirstLetter;

        public FluentPerson()
        {
            id = randomNumber.Next(0, 100);
            person = new Person();
        }

        public FluentPerson(int requestedData)
        {
            if(requestedData > 1000)
            {
                Console.WriteLine("Sahte veri üretme miktarı 1000 ile sınırlıdır.");
                return;
            }
            id = randomNumber.Next(0, 100);
            persons = InitializeArray<Person>(requestedData);
            _requestedData = requestedData;
        }
        public FluentPerson FluentName<T>(Gender gender) where T : IName
        {
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
        public FluentPerson FluentName()
        {
            if(_requestedData > 1)
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
        public FluentPerson FluentSurname()
        {
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
        public FluentPerson FluentTelephoneNumber()
        {
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
        public FluentPerson FluentIdentityNumber()
        {
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
        public FluentPerson FluentBirthPlace()
        {
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
        public FluentPerson FluentEMail()
        {
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
        public FluentPerson FluentAddress()
        {
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
                    fakeAddress = CreateFullAdress(
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

            fakeAddress = CreateFullAdress((Neighborhood)addressTypes[0],
                             (Street)addressTypes[1],
                             (District)addressTypes[2],
                             (Province)addressTypes[3]);

            person.Address = fakeAddress;

            return this;
        }
        public FluentPerson FluentDepartment()
        {
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
        public FluentPerson FluentHighSchool()
        {
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
        public FluentPerson FluentUniversity()
        {
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
        public FluentPerson FluentSalary()
        {
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
        public FluentPerson FluentHobbies(params int[] requestedHobbyNums)
        {
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
                            person.Hobbies = hobbyList.ToArray();
                        else
                            persons[i].Hobbies = hobbyList.ToArray();
                    }
                }
            }  
            return this;
        }
        public FluentPerson FluentDrivingLicense()
        {
            int totalData = GetDataLength("DrivingLicence");
            if (_requestedData > 1)
            {
                DrivingLicence[] resultArr = GetDataList<DrivingLicence>(_requestedData);
                for (int i = 0; i < _requestedData; i++)
                {
                    id = randomNumber.Next(1, totalData);
                    persons[i].DrivingLicense = resultArr[id].class_name;
                }
                return this;
            }

            id = randomNumber.Next(1, totalData);
            var result = GetData<DrivingLicence>(id);
            person.DrivingLicense = result.class_name;
            return this;
        }

        public string CreateAsJSON()
        {
            var options = new JsonSerializerOptions
            {
                IgnoreNullValues = true,
                WriteIndented = true
            };
            if (_requestedData > 1)
                return JsonSerializer.Serialize(persons, options);
            return JsonSerializer.Serialize(person, options);
        }
    }
}