using System;
using System.Collections.Generic;
using System.Text;

namespace FakeDataApplication.Entity
{
    public class MacAdress : IEntity
    {
        public int id { get; set; }
        public string mac_adress { get; set; }
    }
}
