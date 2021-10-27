using convertTkbJson.Controller;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace convertTkbJson
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void openFileButton_Click(object sender, EventArgs e)
        {
            fbdSource.RootFolder = Environment.SpecialFolder.MyComputer;
            if (fbdSource.ShowDialog() == DialogResult.OK)
            {
                if (!string.IsNullOrEmpty(fbdSource.SelectedPath))
                {
                    ReadFile readExcelFile = new ReadFile();
                    readExcelFile.FolderPath = fbdSource.SelectedPath;
                    readExcelFile.SearchPattern = "*.xlsx";
                    List<string> filePaths = readExcelFile.GetAllFile().ToList();
                    foreach (string filePath in filePaths)
                    {
                        readExcelFile.ReadExcelFile(filePath);
                    }
                }
            }
        }

        private void openMissionButton_Click(object sender, EventArgs e)
        {
            fbdSource.RootFolder = Environment.SpecialFolder.MyComputer;
            if (fbdSource.ShowDialog() == DialogResult.OK)
            {
                if (!string.IsNullOrEmpty(fbdSource.SelectedPath))
                {
                    ReadFile readExcelFile = new ReadFile();
                    readExcelFile.FolderPath = fbdSource.SelectedPath;
                    readExcelFile.SearchPattern = "*.xlsx";
                    List<string> filePaths = readExcelFile.GetAllFile().ToList();
                    foreach (string filePath in filePaths)
                    {
                        readExcelFile.ReadMissionsExcelFile(filePath);
                    }
                }
            }
        }
    }
}
