using System;
using System.Collections.Generic;
using System.Text;

namespace FakeDataApplication.Entity
{
    public class Neighborhood : IEntity
    {
        public int id { get; set; }
        public string neighborhood { get; set; }
    }
}
