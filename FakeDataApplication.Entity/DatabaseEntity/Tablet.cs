using System;
using System.Collections.Generic;
using System.Text;

namespace FakeDataApplication.Entity
{
    public class Tablet : IEntity
    {
        public int id { get; set; }
        public string tablet { get; set; }
    }
}
