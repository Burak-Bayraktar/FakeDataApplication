using System;
using System.Collections.Generic;
using System.Text;

namespace FakeDataApplication.Entity
{
    public class Laptop : IEntity
    {
        public int id { get; set; }
        public string laptop { get; set; }
    }
}
