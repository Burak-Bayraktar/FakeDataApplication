using FakeDataApplication.Entity.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace FakeDataApplication.Entity
{
    public class Person : IEntity
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string EMail { get; set; }
        public long? TelephoneNumber { get; set; }
        public string Address { get; set; }
        public string HighSchool { get; set; }
        public string University { get; set; }
        public string Department { get; set; }
        public int? Salary { get; set; }
        public string[] Hobbies { get; set; }
        public string BirthPlace { get; set; }
        public string DrivingLicense { get; set; }
        public string PersonalCar { get; set; }
    }
}
