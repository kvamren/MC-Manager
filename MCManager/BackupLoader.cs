﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MCManager.Backups;

namespace MCManager
{
    public class BackupLoader
    {
        internal static List<IBackupFormat> formats = new List<IBackupFormat>();

        public static void AddFormat(IBackupFormat format)
        {
            formats.Add(format);
        }

        public static List<IBackup> LoadBackups()
        {
            List<IBackup> backups = new List<IBackup>();

            foreach (string file in Directory.GetFiles(Data.backupdir))
            {
                BinaryReader br = new BinaryReader(new FileStream(file, FileMode.Open));
                byte sig = br.ReadByte();
                br.Close();
                foreach (IBackupFormat format in formats)
                {
                    if (format.getSignature() == sig)
                    {
                        IBackup backup = format.Load(file);
                        backups.Add(backup);
                    }
                }
            }

            return backups;
        }

        public static void SaveBackups(List<IBackup> backups)
        {
            foreach (IBackup backup in backups)
            {
                IBackupFormat format = backup.GetFormat();
                format.Save(backup.GetFilePath(), backup);
            }
        }
    }
}