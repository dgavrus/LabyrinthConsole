using System;
using System.Collections;
using System.IO;

namespace LabirynthConsole
{
    class FileParser
    {

        private ArrayList mapInfo = new ArrayList();

        public FileParser(string fileName)
        {
            string path = Environment.CurrentDirectory.ToString() + "\\";
            StreamReader streamReader = new StreamReader(new FileStream(path + fileName, FileMode.Open));
            try
            {
                bool first = true;
                while (true)
                {
                    string s = streamReader.ReadLine();
                    if (streamReader.EndOfStream)
                    {
                        break;
                    }
                    if (s.Length == 0 || first)
                    {
                        mapInfo.Add(s.Length == 0 ? streamReader.ReadLine().Split(' ') : s.Split(' '));
                        first = false;
                    }
                }
            }
            catch (IOException)
            {
                Console.Clear();
                Console.WriteLine("Error in maps.mp file");
                mapInfo.Clear();
            }
            streamReader.Close();
        }

        public ArrayList getMapInfo()
        {
            return mapInfo;
        }
        
    }
}
