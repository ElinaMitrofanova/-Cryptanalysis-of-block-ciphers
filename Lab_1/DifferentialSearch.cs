using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Lab_1
{
    public class DifferentialSearch
    {
        Logicrithmetic _logicrithmetic;
        Heys _heys;
        Helper _helper;
        public DifferentialSearch()
        {
            _logicrithmetic = new Logicrithmetic();
            _heys = new Heys();
            _helper = new Helper();
        }


        public List<Tuple<int[], List<int[]>, double[]>> initData = new List<Tuple<int[], List<int[]>, double[]>>();

        List<int[]> x =new List<int[]>();
        double[] b = new double[(int)Math.Pow(2, 16)];
        //public void GenerateAllBinaryStrings(int n,int[] arr, int i)
        //{
        //    if (i == n)
        //    {
        //        x.Add(arr);
        //        return;
        //    }
        //    arr[i] = 0;
        //    GenerateAllBinaryStrings(n, arr, i + 1);

        //    arr[i] = 1;
        //    GenerateAllBinaryStrings(n, arr, i + 1);
        //}

        public void Vectora(int n)
        {
            for (int i = 1; i < Math.Pow(2, n); i++)
            {
                string binary_string = "";
                binary_string = Convert.ToString(i, 2);
                //List<int> vector = new List<int>();
                int[] vector = new int[16];
                while(binary_string.Length < n)
                {
                    binary_string = "0" + binary_string;
                }
                //for (int j = 0; j < binary_string.Length; j++)
                //{
                //    if (binary_string[j] == '0') { vector.Add(0); }
                //    if (binary_string[j] == '1') { vector.Add(1); }
                //}

                vector = binary_string.Select(s => int.Parse(s.ToString())).ToArray();
                x.Add(vector);
            }
        }

        public void Init()
        {
            Vectora(16);
            foreach (var item in x)
            {
                Tuple<int[], List<int[]>, double[]> tuple = new Tuple<int[], List<int[]>, double[]>(item, x, b);
                initData.Add(tuple);
            }
        }

        public List<int[]> OneRound_x()
        {
            var x = initData[0].Item2;

            List<int[]> x_round = new List<int[]>();
            for (int i = 0; i < x.Count; i++)
            {
                x_round.Add(_heys.OneRound(x[i]));
            }
            return x_round;
        }

        public void CountDif()
        {
            var count = 1 / Math.Pow(2, 16);
            List<int[]> x_round = OneRound_x();
            //для каждого тьюпла

            Parallel.ForEach(initData, tuple =>

            //foreach (var tuple in initData)
            {
                new ParallelOptions
                {
                    MaxDegreeOfParallelism = Convert.ToInt32(Math.Ceiling((Environment.ProcessorCount * 0.75) * 2.0))
                };
            var a = tuple.Item1;
            //для каждого списка векторов икс
            //Parallel.For(0, tuple.Item2.Count, j =>
            for (int j = 0; j < tuple.Item2.Count; j++)
            {
                //new ParallelOptions
                //{
                //    MaxDegreeOfParallelism = Convert.ToInt32(Math.Ceiling((Environment.ProcessorCount * 0.75) * 2.0))
                //};
                var x_i = tuple.Item2[j];
                var x_iXORa = _logicrithmetic.XOR(x_i, a);

                //var fx_i = _heys.OneRound(x_i);
                var fx_iXORa = _heys.OneRound(x_iXORa);

                var b = _logicrithmetic.XOR(x_round[j], fx_iXORa);

                var index = tuple.Item2.FindIndex(x => x.SequenceEqual(b));
                tuple.Item3[index] = tuple.Item3[index] + count;
            }
                
            });
        }

        public void Temp (List<Tuple<int[], List<int[]>, double[]>> initData)
        {

            var count = 1 / Math.Pow(2, 16);
           for(int i = 0; i< initData.Count; i++)
            { 
                var a = initData[i].Item1;
                int temp = initData[i].Item2.Count;
                for (int j = 0; j < temp; j++)
                {
                    var x_i = initData[i].Item2[j];
                    var x_iXORa = _logicrithmetic.XOR(x_i, a);

                    var fx_i = _heys.OneRound(x_i);
                    var fx_iXORa = _heys.OneRound(x_iXORa);

                    var b = _logicrithmetic.XOR(fx_i, fx_iXORa);

                    var index = initData[i].Item2.FindIndex(x => x.SequenceEqual(b));
                    initData[i].Item3[index] = initData[i].Item3[index] + count;
                }
                Console.WriteLine(i);
            }

            
          
        }
        public void Search()
        {

        }
        public void General()
        {
            Init();
            var splittedArray = _helper.SplitList(initData, initData.Count/8);

            //foreach( var item in splittedArray)
            //{
            //    System.Threading.Thread t = new System.Threading.Thread(() => Temp(item));
            //    t.Start();
            //    Console.WriteLine(t.ThreadState);
            //}

            var last = splittedArray.Last();
            Temp(last);


            //CountDif();
        }
    }
}
