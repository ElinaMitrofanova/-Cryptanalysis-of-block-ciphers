using System;
using System.Collections.Generic;
using System.Linq;

namespace Lab_1
{
    public class Helper
    {
        public IEnumerable<int[]> SplitArray(int[] array, int chanks)
        {
            int counter = chanks;
            return array.GroupBy(_ => counter++ / chanks).Select(v => v.ToArray());
        }

        public IEnumerable<List<Tuple<int[], List<int[]>, double[]>>> SplitList(List<Tuple<int[], List<int[]>, double[]>> a,int  chanks)
        {
            int counter = chanks;
            return a.GroupBy(_ => counter++ / chanks).Select(v => v.ToList());
        }

        public List<int[]> Permutation(List<int[]> z)
        {
            List<int[]> result = new List<int[]>();
            for (int i = 0; i < 4; i++)
            {
                int[] temp = new int[4];
                result.Add(temp);
            }

            for (int j = 0; j < 4; j++)
            {
                var fragment_j = z[j];
                for (int i = 0; i < 4; i++)
                {
                    var bit = fragment_j[i];
                    result[i][j] = bit;
                }
            }
            return result;
        }
    }
}