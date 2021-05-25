using System;
using System.Collections.Generic;
using System.Text;

namespace FakeDataApplication.Entity
{
    public class NameSurname : IEntity
    {
        public int id { get; set; }
        public string name { get; set; }
        public string surname { get; set; }
    }
}
