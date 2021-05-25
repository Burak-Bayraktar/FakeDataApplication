using FakeDataApplication.Business;
using FakeDataApplication.Entity;
using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace FakeDataApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            FluentPerson person = new FluentPerson(200);

                string s = person
                .FluentName<ManName>(Gender.Man)
                .FluentSurname()
                .FluentEMail()
                .FluentAddress()
                .FluentDepartment()
                .FluentUniversity()
                .CreateAsJSON();

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