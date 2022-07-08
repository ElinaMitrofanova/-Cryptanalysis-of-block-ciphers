using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Lab_1
{
    internal class Files
    {
        private string path = @"C:\Users\Elina_Mitrofanova\Desktop\Univer\";
        public void WriteInFile(int [] array,string fileName)
        {
            var finalPath = path + fileName;
            foreach(int i in array)
            {
                var symbol = i.ToString();
                File.AppendAllText(finalPath, symbol);
            }
        }

        public void ToBinFile(string k, string fileName)
        {
            BinaryWriter bwStream = new BinaryWriter(new FileStream(fileName, FileMode.Create));

            Encoding ascii = Encoding.ASCII;
            BinaryWriter bwEncoder = new BinaryWriter(new FileStream(fileName, FileMode.Create), ascii);
            using (BinaryWriter binWriter =
               new BinaryWriter(File.Open(fileName, FileMode.Create)))
            {
                // Write string   
                binWriter.Write(k);
            }
        }
    }
}