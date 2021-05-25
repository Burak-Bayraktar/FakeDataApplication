using System;
using System.Collections.Generic;
using System.Text;

namespace FakeDataApplication.Entity
{
    public class Department : IEntity
    {
        public int id { get; set; }

        public string department { get; set; }
    }
}
