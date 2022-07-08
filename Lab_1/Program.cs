
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Lab_1
{
    static class Program
    {
       
       

       
        static void Main(string[] args)
        {

            //int[] key = new int[112];
            //Random random = new Random();
            //for (int i = 0; i < key.Length; i++)
            //{
            //    key[i] = random.Next(2);
            //}

            //Heys heys = new Heys();
            //heys.HeysCipher();


            Stopwatch sw = Stopwatch.StartNew();
            sw.Start();
            DifferentialSearch a = new DifferentialSearch();


            a.General();
            sw.Stop();

            Console.WriteLine(sw.Elapsed.Minutes);
            //Console.WriteLine(time.ElapsedMilliseconds);
        }
    }
}
