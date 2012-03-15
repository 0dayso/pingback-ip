using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.IO;

namespace IAClass
{
    /// <summary>
    /// 配置文件的抽象基类，必须被重载
    /// </summary>
    public abstract class XMLConfig
    {
        public XMLConfig Read()
        {
            string fileName = this.GetType().Name + ".xml";
            return Read(fileName);
        }

        public XMLConfig Read(string fileName)
        {
            XMLConfig data = null;
            XmlSerializer serializer = new XmlSerializer(this.GetType());
            FileStream fs = null;

            try
            {
                fs = new FileStream(fileName, FileMode.Open);
                data = serializer.Deserialize(fs) as XMLConfig;
                fs.Close();
            }
            catch (Exception e)
            {
                if (fs != null)
                    fs.Close();
                Common.LogIt(e.ToString());
                //System.Windows.Forms.MessageBox.Show("读取 " + fileName + " 配置文件失败，将使用默认值。", "警告", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);
                data = Activator.CreateInstance(this.GetType()) as XMLConfig;
            }

            return data;
        }

        public void Save()
        {
            string fileName = this.GetType().Name + ".xml";
            Save(fileName);
        }

        public void Save(string fileName)
        {
            XmlSerializer serializer = new XmlSerializer(this.GetType());

            // serialize the object
            using (FileStream fs = new FileStream(fileName, FileMode.Create))
            {
                serializer.Serialize(fs, this);
                fs.Close();
            }
        }
    }
}
