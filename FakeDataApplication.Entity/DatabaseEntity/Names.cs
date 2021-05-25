using System;
using System.Collections.Generic;
using System.Text;

namespace FakeDataApplication.Entity
{
    public class Names : IEntity
    {
        public int id { get; set; }
        public string man_name { get; set; }
        public string women_name { get; set; }
        public string surname { get; set; }

    }
}
