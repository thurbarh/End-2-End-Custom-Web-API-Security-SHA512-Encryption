
using System.Security.Cryptography;

namespace WebAPIEnd2EndEncryption.SHA512EncryptionEngine
{
    public static class EncryptionHelper
    {
        const string HASHSALT = "d0k8hRzrjRkWOVLd28fwpDZlrm1Vuv3eoyEhzJeBIVpXbInxkoSOr5qZWRFINjsl7SVyW1hdVZ5dbJKmOu9Bibcv8qFIxs5hvgKvTReEggMlmByVBMdzKvSuDwtwscG9mKuSt505ulVPD6GcbYDrcZ";
        public static string EncryptSha512(string value)
        {
            System.Text.ASCIIEncoding encoding = new System.Text.ASCIIEncoding();
            byte[] keyByte = encoding.GetBytes(HASHSALT);
            HMACSHA512 hMACSHA = new HMACSHA512(keyByte);

            byte[] messageBytes = encoding.GetBytes(value);
            byte[] hashmessage = hMACSHA.ComputeHash(messageBytes);

            return ByteToString(hashmessage);
        }

        private static string ByteToString(byte[] buff)
        {
            string sbinary = "";

            for (int i = 0; i < buff.Length; i++)
            {
                sbinary += buff[i].ToString("X2"); // hex format
            }
            return (sbinary);
        }
    }
}