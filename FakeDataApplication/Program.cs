using FakeDataApplication.Business;
using FakeDataApplication.Entity;

namespace FakeDataApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            string fileFolder = $@"D:\Bitirme-Çalışması\UYGULAMA ÖRNEK JSON DOSYALARI";

            FakePerson person = new FakePerson(100);
            person
                .FakeIdentityNumber()
                .FakeName<ManName>(Gender.Man)
                .FakeSurname()
                .FakeEMail()
                .FakeBirthPlace()
                .FakeAddress()
                .FakeDepartment()
                .FakeDrivingLicense()
                .FakePersonalCar()
                .FakeHobbies(3, 2, 4)
                .FakeHighSchool()
                .FakeUniversity()
                .CreateAsJSON(fileFolder);

            FakeBookStore book = new FakeBookStore(1000);
            book.FakeBookAndAuthor().CreateAsJSON(fileFolder);

            FakeTechnologicalDevice techDevice = new FakeTechnologicalDevice(1000);
            techDevice
                .FluentLaptop()
                .FluentTablet()
                .FluentTelevision()
                .FluentTelephone()
                .CreateAsXML(fileFolder);

            FakeNature nature = new FakeNature(1000);
            nature.FluentAnimal().FluentPlant().CreateAsJSON(fileFolder);
        }
    }
}