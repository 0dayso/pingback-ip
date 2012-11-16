using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IAClass.Entity
{
    [Serializable]
    public class t_Interface
    {
        private int _Id;
        private string _Interface_Name;
        private string _IOC_Class_Alias;
        private string _IOC_Class_Parameters;
        private string _Description;

        public int Id
        {
            set{_Id = value;}
            get{return _Id;}
        }

        public string Interface_Name
        {
            set { _Interface_Name = value; }
            get { return _Interface_Name; }
        }

        public string IOC_Class_Alias
        {
            set { _IOC_Class_Alias = value; }
            get { return _IOC_Class_Alias; }
        }

        public string IOC_Class_Parameters
        {
            set { _IOC_Class_Parameters = value; }
            get { return _IOC_Class_Parameters; }
        }

        public string Description
        {
            set { _Description = value; }
            get { return _Description; }
        }
    }
}
