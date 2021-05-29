using FakeDataApplication.Entity.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace FakeDataApplication.Entity
{
    public class DrivingLicence : IEntity
    {
        public int id { get; set; }

        public string class_name { get; set; }
    }

}
