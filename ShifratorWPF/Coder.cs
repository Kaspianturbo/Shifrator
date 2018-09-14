using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoderDecoder;

namespace ShifratorWPF
{
    public interface ICoder
    {
        void CreateKey(string content, string filePath, Encoding encoding);
        void CreateKey(string content, string filePath);
        void UpDateText(string content, string filePath, Encoding encoding);
        void UpDateText(string content, string filePath);
        void Decipher(string content, string keyPath, string filePath);
        string ReadKey(string filePath);
        bool IsExist(string filePath);
        string GetKeyPath(string keyPath);
    }

    class Coder : ICoder
    {
        private readonly Encoding _defaultEncoding = Encoding.GetEncoding(1251);

        Encrypter encrypter;

        public bool IsExist(string keyPath)
        {
            bool isExist = File.Exists(keyPath);
            return isExist;
        }

        public void UpDateText(string content, string filePath, Encoding encoding)
        {
            File.WriteAllText(filePath, encrypter.CryptStr, encoding);
        }

        public void UpDateText(string content, string filePath)
        {
            UpDateText(content, filePath, _defaultEncoding);
        }

        public void CreateKey(string content, string keyPath, Encoding encoding)
        {
            
            encrypter = new Encrypter(content);
            string _keyPath = keyPath + "\\key.kft";
            
            File.WriteAllText(_keyPath, encrypter.KeyStr, encoding);
        }

        public void CreateKey(string content, string keyPath)
        {
            CreateKey(content, keyPath, _defaultEncoding);
        }

        public void Decipher(string content, string keyPath, string filePath)
        {
            string key = File.ReadAllText(keyPath, _defaultEncoding);
            Decipher decipher = new Decipher(content, key);

            File.WriteAllText(filePath, decipher.CryptStr, _defaultEncoding);
        }

        public string ReadKey(string keyPath)
        {
            return File.ReadAllText(keyPath);
        }

        public string GetKeyPath(string keyPath)
        {
            string path = "";
            int index = 0;

            for (int i = keyPath.Length -1; i > 0; i--)
            {
                if (keyPath[i] == '\\')
                {
                    index = i;
                    break;
                }
            }

            for (int i = 0; i < index; i++)
            {
                path += keyPath[i];
            }
            return path;
        }
    }
}
