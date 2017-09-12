using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;
using AspITDBClassLibrary;

namespace flydekasser
{
    class ClassFileHandler : ClassIO
    {
        Class_YourProjectName CYPN = new Class_YourProjectName(@"Server=CV-BB-5461\SQLEXPRESS2014;DataBase=test22;Trusted_Connection=True;");
        private string strFolderpath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\Flyderaporter";
        private string strDocPathRead = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\MaterialData.data";
        private string strDocPathWrite = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\Flyderaporter\Flyderaport" + DateTime.Now.ToString("yyyy-dd-MM_hh-mm") + ".txt";
        public ClassFileHandler()
        {

        }
        public List<string> GetData(bool ReadFromFile)
        {
            if (ReadFromFile == true)
            {
                return ReadListOfStringFromFile(strDocPathRead);
            }
            else
            {

                return CYPN.ReturnListString("SELECT * FROM FlydekasseDB");
            }
        }
        public void WriteListOfString(List<string> ListLine)
        {
            if (Directory.Exists(strFolderpath))
            {
                WriteListOfStringToFile(ListLine, strDocPathWrite);
            }
            else
            {
                Directory.CreateDirectory(strFolderpath);
                WriteListOfStringToFile(ListLine, strDocPathWrite);
            }
        }
        public void FileOpener()
        {
            Process.Start(strDocPathWrite);
        }
        public void SaveData(List<string> ListData, bool WriteToFile)
        {
            if (WriteToFile == true)
            {
                WriteListOfStringToFile(ListData, strDocPathRead);
            }
            else
            {
                CYPN.SaveToDatabase(ListData);
            }
        }
        public void UpdateSQL(string strMatName,string strWeight)
        {
            CYPN.UpdateDatabase(strMatName, strWeight);
        }
        public void DeleteFromSQL(string strMatName,string strWeight)
        {
            CYPN.DeleteFromSQL(strMatName, strWeight);
        }
    }
}
