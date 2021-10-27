using System;
using System.IO;
using OfficeOpenXml;
using System.Reflection;
using System.Globalization;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

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

        public (string, string) getAfternoon(int number)
        {
            switch (number)
            {
                case 0: return ("13:30", "14:55");
                //case 1: return ("08:05", "08:50");
                case 2: return ("15:05", "16:30");
                //case 3: return ("09:50", "10:35");
                //case 4: return ("10:40", "11:25");
                default: return ("", "");
            }
        }


        public void ReadExcelFile(string filePath)
        {
            FileInfo file = new FileInfo(filePath);
            if (!file.Exists) throw new Exception("Excel file is not exist.");
            if (Path.GetExtension(filePath) != ".xlsx") throw new Exception("Config file had to .xlsx file");
            List<Classroom> classrooms = new List<Classroom>();
            List<Classroom> afternoon = new List<Classroom>();

            using (ExcelPackage package = new ExcelPackage(file))
            {
                ExcelWorksheet sheet = package.Workbook.Worksheets["TKB"];
                if (sheet == null) throw new Exception($"Sheet {"TKB"} is not exist.");

                for (int i = 4; (string)sheet.Cells[2, i].Value != "Thứ"; i++)
                {
                    if (sheet.Cells[2, i].Value == null) continue;

                    Classroom classroom = new Classroom();
                    classroom.Name = sheet.Cells[2, i].Value.ToString();
                    classroom.Room = sheet.Cells[3, i].Value.ToString();

                    for (int k = 4; k <= 54; k += 10)
                    {
                        List<Lesson> lessons = new List<Lesson>();

                        for (int j = 0; j < 5; j++)
                        {
                            (string start, string end) = getTime(j);
                            string Title = sheet.Cells[k + j, i].Value?.ToString() ?? "";
                            string Note = sheet.Cells[k + j, i + 1].Value?.ToString() ?? "";
                            if (Title != "") lessons.Add(new Lesson() { Title = Title, Note = $"GV: {Note}", Start = start, End = end });
                        }

                        classroom.Lessons.Add(lessons);
                    }
                    classrooms.Add(classroom);
                }

                for (int i = 67; i < 131; i++)
                {
                    if (sheet.Cells[2, i].Value == null) continue;

                    Classroom classroom = new Classroom();
                    classroom.Name = sheet.Cells[2, i].Value.ToString();
                    classroom.Room = sheet.Cells[3, i].Value?.ToString() ?? "";

                    for (int k = 9; k <= 59; k += 10)
                    {
                        List<Lesson> lessons = new List<Lesson>();

                        for (int j = 0; j < 5; j += 2)
                        {
                            (string start, string end) = getAfternoon(j);
                            string Title = sheet.Cells[k + j, i].Value?.ToString() ?? "";
                            string Note = sheet.Cells[k + j, i + 1].Value?.ToString() ?? "";
                            if (Title != "") lessons.Add(new Lesson() { Title = Title, Note = $"GV. {Note}", Start = start, End = end });
                        }

                        classroom.Lessons.Add(lessons);
                    }
                    afternoon.Add(classroom);
                }
            }

            var serializerSeetings = new JsonSerializerSettings();
            serializerSeetings.ContractResolver = new CamelCasePropertyNamesContractResolver();

            string json = JsonConvert.SerializeObject(classrooms, serializerSeetings);
            File.WriteAllText($"{FolderPath}/{DateTimeOffset.Now.ToUnixTimeSeconds()}_converted.json", json);

            string json2 = JsonConvert.SerializeObject(afternoon, serializerSeetings);
            File.WriteAllText($"{FolderPath}/{DateTimeOffset.Now.ToUnixTimeSeconds()}_afternoon.json", json2);
        }

        public void ReadMissionsExcelFile(string filePath)
        {
            FileInfo file = new FileInfo(filePath);
            if (!file.Exists) throw new Exception("Excel file is not exist.");
            if (Path.GetExtension(filePath) != ".xlsx") throw new Exception("Config file had to .xlsx file");
            List<Mission> missions = new List<Mission>();

            using (ExcelPackage package = new ExcelPackage(file))
            {
                ExcelWorksheet sheet = package.Workbook.Worksheets["Missions"];
                if (sheet == null) throw new Exception($"Sheet {"Missions"} is not exist.");

                for (int i = 2; !string.IsNullOrEmpty((string)sheet.Cells[i, 1].Value); i++)
                {
                    var Key = sheet.Cells[i, 1].Value?.ToString() ?? "";
                    var Title = sheet.Cells[i, 2].Value?.ToString() ?? "";
                    var Body = sheet.Cells[i, 3].Value?.ToString() ?? "";
                    var Point = sheet.Cells[i, 4].Value?.ToString() ?? "";
                    missions.Add(new Mission() { Key = Key, Title = Title, Body = Body, Point = Point });
                }

            }

            var serializerSeetings = new JsonSerializerSettings();
            serializerSeetings.ContractResolver = new CamelCasePropertyNamesContractResolver();

            string json = JsonConvert.SerializeObject(missions, serializerSeetings);
            File.WriteAllText($"{FolderPath}/{DateTimeOffset.Now.ToUnixTimeSeconds()}_missions.json", json);

        }
    }
}
