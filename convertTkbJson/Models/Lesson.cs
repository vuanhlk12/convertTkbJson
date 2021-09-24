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
    public class Lesson
    {
        public string Start { get; set; }
        public string End { get; set; }
        public string Title { get; set; }
        public string Teacher { get; set; }
    }
}
