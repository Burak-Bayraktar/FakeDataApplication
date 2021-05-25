using System;
using System.Collections.Generic;
using System.Text;

namespace FakeDataApplication.Entity
{
    public class Money : IEntity
    {
        public int id { get; set; }
        public string currency_code { get; set; }
        public string currency_unity_tr { get; set; }
        public string currency_symbol { get; set; }

    }
}
