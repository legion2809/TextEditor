using System;
using System.IO;

namespace Text_Editor
{
    class FileOper
    {
        private string filename;
        private bool IsFileSaved;
        private string filePath;

        public string Filename { get => filename; set => filename = value; }
        public bool isFileSaved { get => IsFileSaved; set => IsFileSaved = value; }
        public string FilePath { get => filePath; set => filePath = value; }

        public void InitNewFile()
        {
            this.Filename = "Unnamed.txt";
            this.isFileSaved = true;
        }

        public void SaveFile(string filePath, string[] lines)
        {
            this.FilePath = filePath;
            Stream st = File.Open(FilePath, FileMode.OpenOrCreate, FileAccess.Write);

            using (StreamWriter sw = new StreamWriter(st))
            {
                foreach (string line in lines)
                {
                    sw.WriteLine(line);
                }
            }
            UpdateFileStatus();
        }

        public string OpenFile(string filePath)
        {
            string text;
            this.FilePath = filePath;
            Stream st = File.Open(filePath, FileMode.Open, FileAccess.ReadWrite);

            using (StreamReader sr = new StreamReader(st))
            {
                text = sr.ReadToEnd();
            }
            UpdateFileStatus();
            return text;
        }

        private void UpdateFileStatus()
        {
            string filename = FilePath.Substring(FilePath.LastIndexOf("\\") + 1);
            this.Filename = filename;
            this.IsFileSaved = true;
        }
    }
}
