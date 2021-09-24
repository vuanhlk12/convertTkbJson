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
    public class ReadFile
    {
        public string SearchPattern { get; set; }
        public string FolderPath { get; set; }
        public string[] GetAllFile()
        {
            return Directory.GetFiles(@FolderPath, SearchPattern, SearchOption.TopDirectoryOnly);
        }

        public void ReadExcelFile(string filePath)
        {
            FileInfo file = new FileInfo(filePath);
            if (!file.Exists) throw new Exception("Excel file is not exist.");
            if (Path.GetExtension(filePath) != ".xlsx") throw new Exception("Config file had to .xlsx file");

            using (ExcelPackage package = new ExcelPackage(file))
            {

            }

        }
    }
}
