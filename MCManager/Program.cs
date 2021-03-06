﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MCManager
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            Data.CheckStartupFolders();
            PluginLoader.LoadPlugins();
            if (File.Exists(Data.logininfo))
            {
                DataHolder.SetLoginInfo(LoginInfo.Load(Data.logininfo));
            }
            DataHolder.SetBackups(BackupLoader.LoadBackups());
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
            BackupLoader.SaveBackups(DataHolder.GetBackups());
            if (DataHolder.HasLoginInfo)
            {
                DataHolder.GetLoginInfo().Save(Data.logininfo);
            }
            else
            {
                File.Delete(Data.logininfo);
            }
        }
    }
}