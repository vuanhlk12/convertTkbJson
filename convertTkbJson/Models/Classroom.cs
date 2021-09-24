using System;
using System.IO;
using OfficeOpenXml;
using System.Reflection;
using System.Globalization;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace convertTkbJson.Controller
{
    public class Classroom
    {
        public Classroom()
        {
            Lessons = new List<List<Lesson>>();
        }
        public string Name { get; set; }
        public string ClassroomNumber { get; set; }
        public List<List<Lesson>> Lessons { get; set; }
    }
}
