using System;
using System.Collections.Generic;
using System.Text;

namespace FakeDataApplication.Entity
{
    public class Hobby : IEntity
    {
        public int id { get; set; }
        public string hobby { get; set; }
    }
}
