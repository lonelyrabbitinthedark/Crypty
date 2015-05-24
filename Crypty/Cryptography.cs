using System.Linq;
using System.Text;

namespace Crypty
{
    internal static class Cryprography
    {
        public class RC4
        {
            private int x;
            private int y;
            private readonly byte[] S = new byte[256];

            public RC4(string key)
            {
                byte[] keyBytes = ASCIIEncoding.ASCII.GetBytes(key);
                Init(keyBytes);
            }
            public RC4(byte[] key)
            {
                Init(key);
            }

            // Key-Scheduling Algorithm 
            // Алгоритм ключевого расписания 
            private void Init(byte[] key)
            {
                var keyLength = key.Length;

                for (var i = 0; i < 256; i++)
                {
                    S[i] = (byte) i;
                }

                var j = 0;
                for (var i = 0; i < 256; i++)
                {
                    j = (j + S[i] + key[i%keyLength])%256;
                    S.Swap(i, j);
                }
            }

            public byte[] Encode(byte[] dataB, int size)
            {
                var data = dataB.Take(size).ToArray();

                var cipher = new byte[data.Length];

                for (var m = 0; m < data.Length; m++)
                {
                    cipher[m] = (byte) (data[m] ^ KeyItem());
                }

                return cipher;
            }

            public byte[] Decode(byte[] dataB, int size)
            {
                return Encode(dataB, size);
            }

            // Pseudo-Random Generation Algorithm 
            // Генератор псевдослучайной последовательности 
            private byte KeyItem()
            {
                x = (x + 1)%256;
                y = (y + S[x])%256;

                S.Swap(x, y);

                return S[(S[x] + S[y])%256];
            }
        }
    }

    internal static class SwapExt
    {
        public static void Swap<T>(this T[] array, int index1, int index2)
        {
            var temp = array[index1];
            array[index1] = array[index2];
            array[index2] = temp;
        }
    }
}