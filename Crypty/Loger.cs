using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Security.AccessControl;

namespace Crypty
{
    static class Loger
    {
        public enum LogKind
        {
            Info,
            Warning,
            Error
        }
        public const string JournalDirectoryName = "Logs";
        private const string MainDivider = "===================================================";
        private const string SlaveDivider = "---------------------------------------------------";
        public static void CheckDirectory()
        {
            var directoryInfo = new DirectoryInfo(Environment.CurrentDirectory + "\\" + JournalDirectoryName);
            if (directoryInfo.Exists) return;
            Directory.CreateDirectory(Environment.CurrentDirectory + "\\" + JournalDirectoryName);
        }

        private static void CreateJournal(DateTime dateTime)
        {
            CheckDirectory();
            var fileInfo = new FileInfo(Environment.CurrentDirectory + @"\" + JournalDirectoryName + @"\" + dateTime.ToShortDateString());
            if (fileInfo.Exists) return;
            File.Create(Environment.CurrentDirectory + @"\" + JournalDirectoryName + @"\" + dateTime.ToShortDateString()).Close();
            fileInfo.Attributes = FileAttributes.Normal;
            fileInfo.IsReadOnly = false;
        }

        public static void AddToJournal(LogKind logKind, string message)
        {
            CreateJournal(DateTime.Now);
            using (var streamWriter = new StreamWriter(Environment.CurrentDirectory + @"\" + JournalDirectoryName + @"\" + DateTime.Now.ToShortDateString(), true, Encoding.UTF8))
            {
                streamWriter.WriteLine(MainDivider);
                streamWriter.WriteLine("Date: " + DateTime.Now.ToShortDateString());
                streamWriter.WriteLine("Time: " + DateTime.Now.ToShortTimeString());
                streamWriter.WriteLine(SlaveDivider);
                streamWriter.WriteLine("Type: " + logKind);
                streamWriter.WriteLine("Message: " + message);
            }
        }
    }
}
