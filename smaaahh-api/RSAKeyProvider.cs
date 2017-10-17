using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using System.Web;

namespace smaaahh_api
{
    public class RSAKeyProvider
    {

        private string rsaKeyPath;

        public RSAKeyProvider()
        {
            rsaKeyPath = AppDomain.CurrentDomain.BaseDirectory + @"rsakey.txt";
        }

        public async Task<string> GetPrivateAndPublicKeyAsync()
        {
            string result = await ReadPrivateAndPublicKeyAsync();
            if (string.IsNullOrEmpty(result))
            {
                string key = CreatePrivateAndPublicKey();
                Boolean isInserted = await InsertPrivateAndPublicKeyAsync(key);
                if (isInserted)
                    result = key;
            }
            return result;
        }

        private string CreatePrivateAndPublicKey()
        {
            RSACryptoServiceProvider myRSA = new RSACryptoServiceProvider(2048);
            RSAParameters publicKey = myRSA.ExportParameters(true);
            string publicAndPrivateKey = myRSA.ToXmlString(true);
            return publicAndPrivateKey;
        }

        private async Task<Boolean> InsertPrivateAndPublicKeyAsync(string key)
        {
            Boolean result = false;
            try
            {
                using (StreamWriter fileStream = new StreamWriter(rsaKeyPath))
                {
                    await fileStream.WriteLineAsync(key);
                    result = true;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                result = false;
            }
            return result;
        }

        private async Task<string> ReadPrivateAndPublicKeyAsync()
        {
            String result = null;
            try
            {
                using (StreamReader fileStream = new StreamReader(rsaKeyPath))
                {
                    result = await fileStream.ReadToEndAsync();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            return result;
        }
    }
}