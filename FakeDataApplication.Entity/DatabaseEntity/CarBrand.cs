using System;
using System.Collections.Generic;
using System.Text;

namespace FakeDataApplication.Entity
{
    public class CarBrand : IEntity
    {
        public int id { get; set; }
        public string brand { get; set; }
    }
}
