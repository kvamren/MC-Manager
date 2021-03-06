﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using MCManager.Backups;

namespace MCManager
{
    public static class DataHolder
    {
        private static LoginInfo login;
        private static List<IBackup> backups = new List<IBackup>();

        public static void AddBackup(IBackup backup)
        {
            backups.Add(backup);
        }

        public static void RemoveBackup(string name)
        {
            File.Delete(backups.Find(p => p.GetName() == name).GetFilePath());
            backups.RemoveAll(b => b.GetName() == name);
        }

        public static void RemoveBackup(int index)
        {
            File.Delete(backups[index].GetFilePath());
            backups.RemoveAt(index);
        }

        public static void RemoveBackup(IBackup backup)
        {
            File.Delete(backups.Find(b => b.GetName() == backup.GetName()).GetFilePath());
            backups.Remove(backup);
        }

        public static List<IBackup> GetBackups()
        {
            return backups;
        }

        public static void SetBackups(List<IBackup> Backups)
        {
            backups = Backups;
        }

        public static LoginInfo GetLoginInfo()
        {
            return login;
        }

        public static void SetLoginInfo(LoginInfo Login)
        {
            login = Login;
        }

        public static void DeleteLoginInfo()
        {
            login = null;
        }

        public static bool HasLoginInfo
        {
            get
            {
                return login != null;
            }
        }
    }
}