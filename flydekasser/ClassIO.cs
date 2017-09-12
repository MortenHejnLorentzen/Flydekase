using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace flydekasser
{
    class ClassIO
    {
        public ClassIO()
        {

        }
        protected List<string> ReadListOfStringFromFile(string strDocPath)
        {
            List<string> ListRes = new List<string>();

            try
            {
                string strLine;


                StreamReader file = new StreamReader(strDocPath, Encoding.Default);
                while ((strLine = file.ReadLine()) != null)
                {
                    ListRes.Add(strLine);
                }
                file.Close();
            }
            catch
            {

            }
            return ListRes;
        }
        protected void WriteListOfStringToFile(List<string> ListLine, string strDocPath)
        {
            using (StreamWriter outputFile = new StreamWriter(strDocPath, false, Encoding.Default))
            {
                foreach (string Line in ListLine)
                {
                    outputFile.WriteLine(Line);
                }
            }
        }
    }
}
