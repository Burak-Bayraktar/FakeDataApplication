using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using FakeDataApplication.Entity;
using Dapper;
using System.Reflection;
using FakeDataApplication.Business;
using System.Globalization;
using System.Text.Json;

namespace FakeDataApplication.Entity
{
    public class FluentPerson : FluentBase, IFluentBase
    {
        Random randomNumber = new Random();
        int id = 0;
        Person person;
        int _requestedData = 1; // default requested value.
        public FluentPerson()
        {
            id = randomNumber.Next(0, 100);
            person = new Person();
        }

        public FluentPerson(int requestedData)
        {
            id = randomNumber.Next(0, 100);
            person = new Person();
            _requestedData = requestedData;
        }

        public FluentPerson FluentName<T>(Gender gender)
        {
            int totalData = GetDataLength(typeof(T).Name);
            id = randomNumber.Next(1, totalData);
            if (gender == Gender.Man)
            {
                ManName result = GetData<ManName>(id);
                var removedWhiteSpaces = RemoveFromWhiteSpaces(result.name);
                var capitalizedFirstLetter = CapitalizeFirstLetterOfString(removedWhiteSpaces);
                person.Name = capitalizedFirstLetter;
            }
            else
            {
                WomanName result = GetData<WomanName>(id);
                var removedWhiteSpaces = RemoveFromWhiteSpaces(result.name);
                var capitalizedFirstLetter = CapitalizeFirstLetterOfString(removedWhiteSpaces);
                person.Name = capitalizedFirstLetter;
            }
            return this;
        }

        public FluentPerson FluentSurname()
        {
            int totalData = GetDataLength("Surname");
            id = randomNumber.Next(1, totalData);
            var result = GetData<Surname>(id);
            var removedWhiteSpaces = RemoveFromWhiteSpaces(result.surname);
            var capitalizedFirstLetter = CapitalizeFirstLetterOfString(removedWhiteSpaces);
            person.Surname = capitalizedFirstLetter;
            return this;
        }
        public FluentPerson FluentTelephoneNumber()
        {
            int totalData = GetDataLength("TelephoneNumber");
            id = randomNumber.Next(1, totalData);
            var result = GetData<TelephoneNumber>(id);
            person.TelephoneNumber = result.telephone_number;
            return this;
        }

        public FluentPerson FluentIdentityNumber()
        {
            int totalData = GetDataLength("IdentityNumber");
            id = randomNumber.Next(1, totalData);
            var result = GetData<IdentityNumber>(id);
            string removedWhiteSpaces = RemoveFromWhiteSpaces(result.identitynumber);
            var capitalizedFirstLetter = CapitalizeFirstLetterOfString(removedWhiteSpaces);
            person.Id = capitalizedFirstLetter;
            return this;
        }
        public FluentPerson FluentProvince()
        {
            int totalData = GetDataLength("Province");
            id = randomNumber.Next(1, totalData);
            var result = GetData<Province>(id);
            var removedWhiteSpaces = RemoveFromWhiteSpaces(result.province);
            var capitalizedFirstLetter = CapitalizeFirstLetterOfString(removedWhiteSpaces);
            person.BirthPlace = capitalizedFirstLetter;
            return this;
        }
            public FluentPerson FluentEMail()
        {
            string[] emailDomains = new string[] { "@gmail.com", "@hotmail.com", "@yahoo.com", "@yandex.com", "@icloud.com", "@outlook.com"};
            var num = randomNumber.Next(1, emailDomains.Length);

            if(!string.IsNullOrEmpty(person.Name) && !string.IsNullOrEmpty(person.Surname))
            {
                var fakeEmail = person.Name.ToLower() + person.Surname.ToLower() + emailDomains[num];
                fakeEmail = ReplaceTurkishCharacter(fakeEmail);
                person.EMail = fakeEmail;
                return this;
            }
            return this;
        }

        public FluentPerson FluentAddress()
        {
            // neighborhood: semt/mahalle,
            // district: ilçe
            // province: il
            // street: sokak
            // ${neighborhood} Mah. ${street} Cad. No: {doorNumber} Dr: {daire} {district} / {province}

            Type[] tableNames = new Type[] { typeof(Neighborhood), typeof(Street), typeof(District), typeof(Province) };

            List<IEntity> addressTypes = new List<IEntity>();

            for (int i = 0; i < tableNames.Length; i++)
            {
                int totalData = GetDataLength(tableNames[i].Name);
                id = randomNumber.Next(1, totalData);
                var methodInfo = typeof(FluentBase).GetMethod(nameof(FluentBase.GetData));
                var genericMethodInfo = methodInfo.MakeGenericMethod(tableNames[i]);
                var result = (IEntity)genericMethodInfo.Invoke(null, new object[] { id });
                addressTypes.Add(result);
            }

            string address = CreateFullAdress((Neighborhood)addressTypes[0], 
                             (Street)addressTypes[1], 
                             (District)addressTypes[2], 
                             (Province)addressTypes[3]);

            person.Address = address;

            return this;
        }

        public FluentPerson FluentDepartment()
        {
            int totalData = GetDataLength("Department");
            id = randomNumber.Next(1, totalData);
            var result = GetData<Department>(id);
            person.Department = result.department;
            return this;
        }

        public FluentPerson FluentUniversity()
        {
            int totalData = GetDataLength("University");
            id = randomNumber.Next(1, totalData);
            var result = GetData<University>(id);
            person.University = result.university;
            return this;
        }

        public string CreateAsJSON()
        {
            var options = new JsonSerializerOptions
            {
                IgnoreNullValues = true,
                WriteIndented = true
            };

            return JsonSerializer.Serialize(person, options);
        }
    }
    }
