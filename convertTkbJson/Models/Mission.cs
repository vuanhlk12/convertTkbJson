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
    public class Mission
    {
        public string Key { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public string Point { get; set; }

    }
}
