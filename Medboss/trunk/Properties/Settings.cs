namespace Nammedia.Medboss.Properties
{
    using System;
    using System.CodeDom.Compiler;
    using System.ComponentModel;
    using System.Configuration;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;

    [GeneratedCode("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "8.0.0.0"), CompilerGenerated]
    internal class Settings : ApplicationSettingsBase
    {
        private static Settings defaultInstance = ((Settings) SettingsBase.Synchronized(new Settings()));

        private void SettingChangingEventHandler(object sender, SettingChangingEventArgs e)
        {
        }

        private void SettingsSavingEventHandler(object sender, CancelEventArgs e)
        {
        }

        public static Settings Default
        {
            get
            {
                return defaultInstance;
            }
        }

        [DefaultSettingValue(""), ApplicationScopedSetting, DebuggerNonUserCode]
        public string FavorFile
        {
            get
            {
                return (string) this["FavorFile"];
            }
        }
    }
}
