using FakeDataApplication.Entity.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace FakeDataApplication.Entity
{
    public class Television : IEntity
    {
        public int id { get; set; }
        public string television { get; set; }
    }
}
