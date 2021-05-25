using System;
using System.Collections.Generic;
using System.Text;

namespace FakeDataApplication.Entity
{
    public class Surname : IEntity
    {
        public string id { get; set; }
        public string surname { get; set; }
    }
}
