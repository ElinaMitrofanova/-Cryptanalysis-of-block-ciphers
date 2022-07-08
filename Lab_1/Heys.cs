using System;
using System.Collections.Generic;
using System.Linq;

namespace Lab_1
{
    internal class Heys
    {
        private int[] key = new int[112];
       
        public int[] plainText = new int[16];

        public string[] S = new string[16] { "F", "0", "E", "6", "8", "D", "5", "9", "A", "3", "1", "C", "4", "B", "7", "2" };

        public IEnumerable<int[]> roundKeys;

        public List<List<int[]>> result = new List<List<int[]>>();

        Logicrithmetic _logicrithmetic;
        Helper _helper;

        public Heys()
        {
            _logicrithmetic = new Logicrithmetic();
            _helper = new Helper();
        }
        public void GenerateBits(int[] array)
        {
            Random random = new Random();
            for (int i = 0; i < array.Length; i++)
            {
                key[i] = random.Next(2);
            }
        }
        public string[] HexToBin(string[] hex)
        {
            string [] result = new string[hex.Length];
            for (int i = 0; i < result.Length; i++)
            {
                result[i] = Convert.ToString(Convert.ToInt32(hex[i], 16), 2);
            }
            return result;
        }
        public string SX(string x)
        {
            int index = Convert.ToInt32(x, 2);
            string sxHex = S[index];
            var sx_strBinary = Convert.ToString(Convert.ToInt64(sxHex, 16), 2);
            while (sx_strBinary.Length != 4)
            {
                string zero = "0";
                sx_strBinary = zero + sx_strBinary;
            }
            return sx_strBinary;
        }

        public int[] OneRound(int[] y)
        {
            var yChanks = _helper.SplitArray(y, 4);
            List<int[]> z = new List<int[]>();
            foreach (var yChank in yChanks)
            {
                var sx_strBinary = SX(string.Join("", yChank));
                var sx = sx_strBinary.Select(n => int.Parse(n.ToString())).ToArray();
                z.Add(sx);
            }
            z = _helper.Permutation(z);
            return z.SelectMany(x => x).ToArray();
        }

        public int[] ReverseRound(int[] k, int[] C)
        {
            var C_Chanks = _helper.SplitArray(C, 4).ToList();
            List<int[]> C_perm = _helper.Permutation(C_Chanks);
            List<int[]> C_Sbox = new List<int[]>();
            foreach (var C_permChank in C_perm)
            {
                var sx_strBinary = SX(string.Join("", C_perm));
                var sx = sx_strBinary.Select(n => int.Parse(n.ToString())).ToArray();
                C_Sbox.Add(sx);
            }
            var C_Sbox_arr = C_Sbox.SelectMany(x => x).ToArray();
            int[] p = _logicrithmetic.XOR(k, C_Sbox_arr);
            return p;
        }
        public int[] SixRounds(int[] plainText)
        {
            var firstRoundKEys = roundKeys.Take(roundKeys.Count() - 1);
            var result_round = plainText;
            foreach (var roundKey in firstRoundKEys)
            {
                int[] y = _logicrithmetic.XOR(roundKey, result_round);
                result_round = OneRound(y);
            }
            result_round = _logicrithmetic.XOR(roundKeys.Last(), result_round);
            return result_round;
        }

        public void HeysCipher()
        {
            Files file = new Files();
            string k = "1010111111011111111010101010010011011001100010001010000001111101000111111101111101001010011011001110001100110111";
            key = k.Select(n => int.Parse(n.ToString())).ToArray();
            file.WriteInFile(key, "key.txt");
            //RoundKys
            roundKeys = _helper.SplitArray(key, 16);
            //Generate plain text
            //GenerateBits(plainText);
            plainText[plainText.Length - 1] = 1;
            file.WriteInFile(plainText, "input.txt");
            //Rounds
            var result = SixRounds(plainText);
            file.WriteInFile(result, "output.txt");
        }
    }
}