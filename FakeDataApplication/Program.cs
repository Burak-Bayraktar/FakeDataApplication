using FakeDataApplication.Business;
using FakeDataApplication.Entity;
using System;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace FakeDataApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            FluentPerson person = new FluentPerson(1000);
            string s = person
                        .FluentIdentityNumber()
                        .FluentName()
                        .FluentSurname()
                        .FluentTelephoneNumber()
                        .FluentEMail()
                        .FluentBirthPlace()
                        .FluentAddress()
                        .FluentDepartment()
                        .FluentHighSchool()
                        .FluentUniversity()
                        .FluentSalary()
                        //.FluentHobbies(1,4,2,3,5)
                        .FluentDrivingLicense()
                        .CreateAsJSON();

            var date = DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString();
            string fileName = $@"D:\Bitirme-Çalışması\UYGULAMA ÖRNEK JSON DOSYALARI\ornek{date}.json";
            File.WriteAllText(fileName, s);

            Console.WriteLine(s);
        }
    }
}

/*
        *** CREATE JSON FILE SAMPLE

         static async Task Main(string[] args)
        {
            FluentPerson person = new FluentPerson(200);

            Person s = (Person)person
                        .FluentName<ManName>(Gender.Man)
                        .FluentSurname()
                        .FluentDepartment()
                        .FluentUniversity()
                        .CreateAsJSON();
            Person[] p = new Person[] { s, s };

            using FileStream fileStream = File.Create(@"D:\Bitirme-Çalışması\UYGULAMA ÖRNEK JSON DOSYALARI\ornek.json");

            await JsonSerializer.SerializeAsync(fileStream, p);

        }
 */