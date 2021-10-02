using System;
using System.IO;
using OfficeOpenXml;
using System.Reflection;
using System.Globalization;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Newtonsoft.Json;

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

        public (string, string) getTime(int number)
        {
            switch (number)
            {
                case 0: return ("07:15", "08:00");
                case 1: return ("08:05", "08:50");
                case 2: return ("09:00", "09:45");
                case 3: return ("09:50", "10:35");
                case 4: return ("10:40", "11:25");
                default: return ("", "");
            }
        }


        public void ReadExcelFile(string filePath)
        {
            FileInfo file = new FileInfo(filePath);
            if (!file.Exists) throw new Exception("Excel file is not exist.");
            if (Path.GetExtension(filePath) != ".xlsx") throw new Exception("Config file had to .xlsx file");
            List<Classroom> classrooms = new List<Classroom>();

            using (ExcelPackage package = new ExcelPackage(file))
            {
                ExcelWorksheet sheet = package.Workbook.Worksheets["TKB"];
                if (sheet == null) throw new Exception($"Sheet {"TKB"} is not exist.");

                for (int i = 4; (string)sheet.Cells[2, i].Value != "Thứ"; i++)
                {
                    if (sheet.Cells[2, i].Value == null) continue;

                    Classroom classroom = new Classroom();
                    classroom.Name = sheet.Cells[2, i].Value.ToString();
                    classroom.ClassroomNumber = sheet.Cells[3, i].Value.ToString();

                    for (int k = 4; k <= 54; k += 10)
                    {
                        List<Lesson> lessons = new List<Lesson>();

                        for (int j = 0; j < 5; j++)
                        {
                            (string start, string end) = getTime(j);
                            string Title = sheet.Cells[k + j, i].Value?.ToString() ?? "";
                            string Teacher = sheet.Cells[k + j, i + 1].Value?.ToString() ?? "";
                            if (Title != "") lessons.Add(new Lesson() { Title = Title, Teacher = Teacher, Start = start, End = end });
                        }

                        classroom.Lessons.Add(lessons);
                    }
                    classrooms.Add(classroom);
                }
            }

            string json = JsonConvert.SerializeObject(classrooms);
            File.WriteAllText($"{DateTimeOffset.Now.ToUnixTimeSeconds()}_converted.json", json);
        }
    }
}
