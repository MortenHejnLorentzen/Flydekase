using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;


namespace flydekasser
{
    class ClassFyldekasseBizz
    {
        ClassFileHandler CFH = new ClassFileHandler();
        ClassMatriale CM = new ClassMatriale();
        ClassBox CB = new ClassBox();
        public ClassFyldekasseBizz()
        {

        }
        bool _bolFromDatafile = true;
        public bool bolFromDatafile
        {
            get
            {
                return _bolFromDatafile;
            }
            set
            {
                if(_bolFromDatafile != value)
                {
                    _bolFromDatafile = value;
                }
            }
        }
        bool _bolToDatafile = true;
        public bool bolToDatafile
        {
            get
            {
                return _bolToDatafile;
            }
            set
            {
                if (_bolToDatafile != value)
                {
                    _bolToDatafile = value;
                }
            }
        }
        ObservableCollection<ClassMatriale> _MatList = new ObservableCollection<ClassMatriale>();
        public ObservableCollection<ClassMatriale> MatList
        {
            get
            {
                if (_MatList.Count <= 0)
                {
                    List<string> ListLoaddata = CFH.GetData(bolFromDatafile);
                    foreach (string data in ListLoaddata)
                    {
                        if (data.Contains(";"))
                        {
                            string[] strArray = data.Split(';');
                            ClassMatriale myCM = new ClassMatriale();
                            myCM.strMatName = strArray[0];
                            myCM.strWight = strArray[1];

                            _MatList.Add(myCM);
                        }
                    }
                }
                return _MatList;
            }
        }
        ObservableCollection<ClassMatriale> _ChosenMatList = new ObservableCollection<ClassMatriale>();
        public ObservableCollection<ClassMatriale> ChosenMatList
        {
            get
            {
                
                return _ChosenMatList;
            }
        }
        ObservableCollection<ClassBox> _BoxDimen = new ObservableCollection<ClassBox>();
        public ObservableCollection<ClassBox> BoxDimen
        {
            get
            {

                return _BoxDimen;
            }
        }

        public void ValgAfMat(ComboBox cb,string thick)
        {
            ClassMatriale cm = new ClassMatriale((ClassMatriale)cb.SelectedItem);
            cm.strThickness = thick;
            _ChosenMatList.Add(cm);
        }
        //her fraviger jeg
        public void SetBoxDimen(decimal H, decimal W, decimal D)
        {
            ClassBox cb = new ClassBox(H, W, D);
            _BoxDimen.Add(cb);
        }
        public void MakeReport()
        {
            List<string> ListLine = new List<string>();
            ListLine.Add($"\t\t{DateTime.Now.ToString("yyyy-MM-dd_hh-mm-ss")}-Flyderaport.txt");
            ListLine.Add($"{Environment.NewLine}\tFlydekasse raport den:{DateTime.Now.ToString("dd-MM-yyyy - hh:mm")}{Environment.NewLine}");
            foreach (ClassMatriale cm in ChosenMatList)
            {
                ListLine.Add($" * * {cm.strMatName} - {cm.strThickness}mm * * ");
                foreach(ClassBox cb in BoxDimen)
                {
                    ListLine.Add(FOpdrift(cm,cb));
                }
            }
            CFH.WriteListOfString(ListLine);
            CFH.FileOpener();
        }
        //her fraviger jeg
        private string FOpdrift(ClassMatriale cm, ClassBox cb)
        {
            decimal T = 0.000M;
            int vaegt = 0;
            decimal H = 0.000M;
            decimal W = 0.000M;
            decimal D = 0.000M;
            decimal.TryParse(cb.strHight, out H);
            decimal.TryParse(cb.strwidth, out W);
            decimal.TryParse(cb.strDepth, out D);
            decimal.TryParse(cm.strThickness, out T);
            int.TryParse(cm.strWight, out vaegt);
            decimal V = H * W * D;
            decimal Matm3 = V - ((H - (2 * (T/1000))) * (W - (2 * (T / 1000))) * (D - (2 * (T / 1000))));
            return $"{Environment.NewLine}\tKassemål:\t{H}m * {W}m * {D}m - Matriale tykelse{cm.strThickness}mm{Environment.NewLine}\tKasse m^3: {V} - Matriale m^3: {Matm3}{Environment.NewLine}\tVægt af Kasse {vaegt*Matm3}kg{Environment.NewLine}\tFlyde kapasiteten af kassen: {(V*1000)-(Matm3*vaegt)}kg{Environment.NewLine}";
        }
        public void DeleteData(ClassMatriale cm)
        {
            if (bolFromDatafile == true)
            {
                try
                {
                    //brug af LINQ
                    ClassMatriale valgtCm = MatList.Where(x => x.strMatName == cm.strMatName).FirstOrDefault();
                    if (valgtCm != null)
                    {
                        _MatList.RemoveAt(MatList.IndexOf(valgtCm));
                    }
                }
                catch
                {

                }
            }
            else
            {
                CFH.DeleteFromSQL(cm.strMatName, cm.strWight);
                ClassMatriale valgtCm = MatList.Where(x => x.strMatName == cm.strMatName).FirstOrDefault();
                if (valgtCm != null)
                {
                    _MatList.RemoveAt(MatList.IndexOf(valgtCm));
                }
            }

        }
        public void UpdateData(string strMatName, string strWight, ClassMatriale cm)
        {
            if (bolFromDatafile == true)
            {
                //brug af LINQ
                ClassMatriale valgtCm = MatList.FirstOrDefault(Cm => Cm.strMatName == cm.strMatName);
                if (valgtCm != null)
                {
                    valgtCm.strMatName = strMatName;
                    valgtCm.strWight = strWight;
                }
            }
            else
            {
                CFH.UpdateSQL(strMatName, strWight);
            }
        }
        public void UpdateDataView(ClassMatriale cm)
        {
            CM.RefreshData(cm);
        }
        public void AddNewMatriale(string strMatName,string strWight)
        {
            if (strMatName != "")
            {
                ClassMatriale  myCM = new flydekasser.ClassMatriale();
                myCM.strMatName  = strMatName;
                myCM.strWight = strWight;
                MatList.Add(myCM);

            }
        }
        public void SaveData()
        {
                try
                {
                    string strTemp = "";
                    List<string> ListData = new List<string>();
                    foreach (ClassMatriale CM in _MatList)
                    {
                        strTemp = $"{CM.strMatName};{CM.strWight}";
                        ListData.Add(strTemp);
                    }
                    ListData.Add("");
                    CFH.SaveData(ListData, bolToDatafile);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
        }
        public void SetToFile()
        {
            bolFromDatafile = true;
            _MatList.Clear();
            List<string> ListLoaddata = CFH.GetData(bolFromDatafile);
            foreach (string data in ListLoaddata)
            {
                if (data.Contains(";"))
                {
                    string[] strArray = data.Split(';');
                    ClassMatriale myCM = new ClassMatriale();
                    myCM.strMatName = strArray[0];
                    myCM.strWight = strArray[1];

                    _MatList.Add(myCM);
                }
            }
        }
        public void SetToDatabase()
        {
            bolFromDatafile = false;
            _MatList.Clear();
            List<string> ListLoaddata = CFH.GetData(bolFromDatafile);
            foreach (string data in ListLoaddata)
            {
                if (data.Contains(";"))
                {
                    string[] strArray = data.Split(';');
                    ClassMatriale myCM = new ClassMatriale();
                    myCM.strMatName = strArray[0];
                    myCM.strWight = strArray[1];

                    _MatList.Add(myCM);
                }
            }
        }

    }
}
