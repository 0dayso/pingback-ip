namespace ePlus.Properties {
    
    
    // This class allows you to handle specific events on the settings class:
    //  The SettingChanging event is raised before a setting's value is changed.
    //  The PropertyChanged event is raised after a setting's value is changed.
    //  The SettingsLoaded event is raised after the setting values are loaded.
    //  The SettingsSaving event is raised before the setting values are saved.
    internal sealed partial class Settings {
        
        public Settings() {
            // // To add event handlers for saving and changing settings, uncomment the lines below:
            //
            // this.SettingChanging += this.SettingChangingEventHandler;
            //
            this.SettingsSaving += this.SettingsSavingEventHandler;
            //
        }
        
        private void SettingChangingEventHandler(object sender, System.Configuration.SettingChangingEventArgs e) {
            // Add code to handle the SettingChangingEvent event here.
        }
        
        private void SettingsSavingEventHandler(object sender, System.ComponentModel.CancelEventArgs e) {
            // Add code to handle the SettingsSaving event here.
            try
            {
                string configFileName = System.Windows.Forms.Application.ExecutablePath + ".config";
                System.Xml.XmlDocument doc = new System.Xml.XmlDocument();
                doc.Load(configFileName);
                string configString = @"configuration/userSettings/ePlus.Properties.Settings/setting[@name='{0}']/value";
                System.Reflection.PropertyInfo[] configNodes = Settings.Default.GetType().GetProperties();
                System.Xml.XmlNode configNode;

                for (int i = 0; i < configNodes.Length; i++)
                {
                    configNode = doc.SelectSingleNode(string.Format(configString, configNodes[i].Name));
                    if (configNode != null)
                    {
                        configNode.InnerText = System.Convert.ToString(configNodes[i].GetValue(Settings.Default, null));
                    }
                }

                doc.Save(configFileName);
            }
            catch (System.Exception ee)
            {
                //System.Windows.Forms.MessageBox.Show("保存用户配置文件失败！详细信息如下：" + System.Environment.NewLine + System.Environment.NewLine + ee.Message);
            }
        }
    }
}
