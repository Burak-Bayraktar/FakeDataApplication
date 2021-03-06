using FakeDataApplication.Business;
using FakeDataApplication.Entity;

namespace FakeDataApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            // Lütfen aşağıdaki string alanına geçerli bir dosya konumu giriniz.
            // ÖRNEK: C:\Users\FAKE DATA APP ÖRNEK DOSYALAR
            string fileFolder = $@"C:\Users\FAKE DATA APP ÖRNEK DOSYALAR";

            //* FAKEPERSON OLUŞTURMA ÖRNEĞİ
            FakePerson person = new FakePerson(5);
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
                .FakeSalary()
                .FakeUniversity()
                .CreateAsJSON(fileFolder);

            //* FAKEBOOKSTORE OLUŞTURMA ÖRNEĞİ
            FakeBookStore book = new FakeBookStore(1000);
            book.FakeBookAndAuthor().CreateAsJSON(fileFolder);

            //* FAKETECHNOLOGICALDEVICE OLUŞTURMA ÖRNEĞİ
            FakeTechnologicalDevice techDevice = new FakeTechnologicalDevice(1000);
            techDevice
                .FluentLaptop()
                .FluentTablet()
                .FluentTelevision()
                .FluentTelephone()
                .CreateAsXML(fileFolder);

            //* FAKENATURE OLUŞTURMA ÖRNEĞİ
            FakeNature nature = new FakeNature(1000);
            nature.FluentAnimal().FluentPlant().CreateAsJSON(fileFolder);

        }
    }
}