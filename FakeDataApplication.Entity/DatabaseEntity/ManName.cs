using FakeDataApplication.Entity.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace FakeDataApplication.Entity
{
    public class ManName : IEntity, IName
    {
        public int id { get; set; }
        public string name { get; set; }
    }
}
