namespace Nammedia.Medboss
{
    using Nammedia.Medboss.controls;
    using Nammedia.Medboss.Log;
    using System;
    using System.Configuration;
    using System.Threading;
    using System.Windows.Forms;

    internal static class Program
    {
        public static ActiveACSource ACSource;

        [STAThread]
        private static void Main()
        {
            LogManager.InitialLogManager();
            ACSource = new ActiveACSource();
            ACSource.ActiveSource = CommonSource.All;
            Loading ld = new Loading();
            ACSource.AddDataChangeListener(ld);
            new Thread(new ThreadStart(ACSource.RefreshSource)).Start();
            bool isDebug = ConfigurationManager.AppSettings["isDebug"] == "debug";
            Application.EnableVisualStyles();
            if (!isDebug)
            {
                Password password = new Password();
                if (password.ShowDialog() == DialogResult.OK)
                {
                    if (!ld.IsClosed)
                    {
                        if (ld.ShowDialog() == DialogResult.Yes)
                        {
                            Application.Run(new MainForm());
                        }
                    }
                    else
                    {
                        Application.Run(new MainForm());
                    }
                }
            }
            else
            {
                Application.Run(new MainForm());
            }
        }
    }
}
