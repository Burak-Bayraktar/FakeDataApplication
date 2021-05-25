using System;
using System.Collections.Generic;
using System.Text;

namespace FakeDataApplication.Entity
{
    public class Plant : IEntity
    {
        public int id { get; set; }
        public string plant { get; set; }
    }
}
