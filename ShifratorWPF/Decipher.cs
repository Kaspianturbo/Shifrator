using System;
using System.Text;

namespace CoderDecoder
{
    class Decipher
    {
        public string CryptStr { get; private set; }

        public byte[] CryptByte { get; private set; }

        public byte[] KeyByte { get; private set; }

        public string KeyStr { get; private set; }

        public Decipher(string str, string key)
        {
            KeyStr = key;
            KeyToByte(key);
            DeCrypt(Encoding.GetEncoding(1251).GetBytes(str));
            CryptToStr();
        }

        private void DeCrypt(byte[] str)
        {
            CryptByte = new byte[str.Length];

            for (int i = 0; i < CryptByte.Length; i++)
            {                
                CryptByte[i] = (byte)(str[i] - KeyByte[i]);              
            }
        }

        private void CryptToStr()
        {
            CryptStr = Encoding.GetEncoding(1251).GetString(CryptByte);
        }

        private void KeyToByte(string key)
        {
            KeyByte = Encoding.GetEncoding(1251).GetBytes(key);
        }
    }
}
