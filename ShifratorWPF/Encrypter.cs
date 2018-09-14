using System;
using System.Text;

namespace CoderDecoder
{
    class Encrypter
    {
        public string KeyStr { get; private set; }

        public byte[] KeyByte { get; private set; }

        public string CryptStr { get; private set; }

        public byte[] CryptByte { get; private set; }

        public Encrypter(string str)
        {
            Random rand = new Random();
            KeyByte = new byte[str.Length * 2];
            rand.NextBytes(KeyByte);
            Crypt(Encoding.GetEncoding(1251).GetBytes(str), KeyByte);
            CryptToStr();
            KeyToStr();
        }

        private void Crypt(byte[] str, byte[] keygen)
        {

            CryptByte = new byte[str.Length];

            for (int i = 0; i < CryptByte.Length; i++)
            {                
                CryptByte[i] = (byte)(str[i] + keygen[i]);
            }
        }

        private void CryptToStr()
        {
            CryptStr = Encoding.GetEncoding(1251).GetString(CryptByte);
        }

        private void KeyToStr()
        {
            KeyStr = Encoding.GetEncoding(1251).GetString(KeyByte);
        }
    }
}
