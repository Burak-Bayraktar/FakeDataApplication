using FakeDataApplication.Business;
using FakeDataApplication.Entity;

namespace FakeDataApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            string fileFolder = $@"D:\Bitirme-Çalışması\UYGULAMA ÖRNEK JSON DOSYALARI";
            FakePerson person = new FakePerson(1000);
            person
                .FakeIdentityNumber()
                .FakeName()
                .FakeSurname()
                .FakeEMail()
                .FakeBirthPlace()
                .FakeAddress()
                .FakeDepartment()
                .FakeDrivingLicense()
                .FakePersonalCar()
                .FakeHobbies(3, 2, 4)
                .FakeHighSchool()
                .FakeUniversity().CreateAsJSON(fileFolder);

            //FluentTechnologicalDevice techDevice = new FluentTechnologicalDevice(1000);
            //techDevice
            //    .FluentLaptop()
            //    .FluentTablet()
            //    .FluentTelevision()
            //    .FluentTelephone()
            //    .CreateAsXML(fileFolder);
        }
    }
}