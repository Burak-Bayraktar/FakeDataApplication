using FakeDataApplication.Entity.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace FakeDataApplication.Entity
{
    public class WomanName : IEntity, IName
    {
        public int id { get; set; }
        public string name { get; set; }
    }
}
