using FakeDataApplication.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

namespace FakeDataApplication.Business
{
    public interface IFluentBase
    {
        string CreateAsJSON(string folderName);
        void CreateAsXML(string folderName);
    }
}
