using System;
using System.Collections.Generic;
using System.Text;

namespace FakeDataApplication.Entity
{
   public class Car : IEntity
    {
        public int id { get; set; }
        public string brand_model { get; set; }
        public int year{ get; set; }

    }
}
