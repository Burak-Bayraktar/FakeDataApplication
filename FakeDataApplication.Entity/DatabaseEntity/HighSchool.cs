using FakeDataApplication.Entity.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace FakeDataApplication.Entity
{
    public class HighSchool : IEntity
    {
        public int id { get; set; }
        public string province { get; set; }
        public string high_school { get; set; }

    }
}
