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
            string fileFolder = $@"D:\Bitirme-Çalışması\UYGULAMA ÖRNEK JSON DOSYALARI";
            //FluentPerson person = new FluentPerson(3330);
            //person
            //.FluentIdentityNumber()
            //.FluentName()
            //.FluentSurname()
            //.FluentEMail()
            //.FluentAddress()
            //.FluentUniversity()
            //.FluentHighSchool()
            //.FluentDepartment()
            //.FluentDrivingLicense()
            //.CreateAsXML(fileFolder);

            FluentTechnologicalDevice techDevice = new FluentTechnologicalDevice(1000);
            techDevice
                .FluentLaptop()
                .FluentTablet()
                .FluentTelevision()
                .FluentTelephone()
                .CreateAsXML(fileFolder);
        }
    }
}