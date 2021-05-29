using FakeDataApplication.Entity.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace FakeDataApplication.Entity
{
    public class CountryLanguage : IEntity
    {
        public int id { get; set; }
        public string country { get; set; }
        public string country_language { get; set; }
    }
}
