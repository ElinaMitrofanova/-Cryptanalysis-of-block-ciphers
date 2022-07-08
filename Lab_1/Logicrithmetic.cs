namespace Lab_1
{
    public class Logicrithmetic
    {
        public int[] XOR(int[] roundKey, int[] plainText)
        {
            int[] y = new int[roundKey.Length];

            for (int i = 0; i < roundKey.Length; i++)
            {
                y[i] = roundKey[i] ^ plainText[i];
            }
            return y;
        }
    }
}