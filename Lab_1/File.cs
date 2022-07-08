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
        
    }
}