using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace flydekasser
{
    class ClassMatriale : INotifyPropertyChanged
    {
        private string _strMatName = "";
        public string strMatName { get { return _strMatName; } set { _strMatName = value; Notify("strMatName"); } }
        private string _strThickness = "";
        public string strThickness{ get { return _strThickness; } set{ _strThickness = value;} }
        private string _strWight = "";
        public string strWight { get { return _strWight; } set { _strWight = value; Notify("strWight"); } }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void Notify(string propertyName)
        {
            if (this.PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public ClassMatriale()
        {

        }
        public ClassMatriale(ClassMatriale cm)
        {
            if (cm != null)
            {
                this.strMatName = cm.strMatName;
                this.strThickness = cm.strThickness;
                this.strWight = cm.strWight;
            }
        }
        public void RefreshData(ClassMatriale cm)
        {
            if (cm != null)
            {
                this.strMatName = cm.strMatName;
                this.strThickness = cm.strThickness;
                this.strWight = cm.strWight;
            }
        }

    }
}
