using System;
using System.Collections.Generic;
using System.Text;

namespace FakeDataApplication.Entity
{
    public class ManName : IEntity
    {
        public int id { get; set; }
        public string name { get; set; }
    }
}
