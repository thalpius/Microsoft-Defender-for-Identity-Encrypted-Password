using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft_Defender_for_Identity_Encrypted_Password
{
    internal class Program
    {
        static void Thalpius()
        {
            Console.WriteLine("  _______ _           _       _           ");
            Console.WriteLine(" |__   __| |         | |     (_)          ");
            Console.WriteLine("    | |  | |__   __ _| |_ __  _ _   _ ___ ");
            Console.WriteLine("    | |  | '_ \\ / _` | | '_ \\| | | | / __|");
            Console.WriteLine("    | |  | | | | (_| | | |_) | | |_| \\__ \\");
            Console.WriteLine("    |_|  |_| |_|\\__,_|_| .__/|_|\\__,_|___/");
            Console.WriteLine("                       | |                ");
            Console.WriteLine("                       |_|                ");
            Console.WriteLine("");
            Console.WriteLine("Version: 1.0.0.0");
            Console.WriteLine("Description: Encrypt or Decrypt a Microsoft Defender for Identity password found in the sensor configuration");
            Console.WriteLine("");
        }
        static void Help()
        {
            Console.WriteLine("Error: Arguments doesn't seem to be right...");
            Console.WriteLine("");
            Console.WriteLine("Encrypt: MicrosoftDefenderForIdentityPassword.exe /encrypt /certificatethumbprint:\"E44826<SNIP>2C657B\" /password:\"Thalpius!\"");
            Console.WriteLine("Decrypt: MicrosoftDefenderForIdentityPassword.exe /decrypt /certificatethumbprint:\"E44826<SNIP>2C657B\" /encryptedbytes:\"Egt3I2U<SNIP>o5SgjQ==\"");
            System.Environment.Exit(1);
        }
        static private string EncryptPassword(string CertificateThumbprint, string Password)
        {
            string EncryptedPassword = string.Empty;
            X509Certificate2 cert = GetCertificate(CertificateThumbprint);
            using (RSA publicKey = cert.GetRSAPublicKey())
            {
                byte[] dataToEncrypt = Encoding.Unicode.GetBytes(Password);
                byte[] bytesDecrypted = publicKey.Encrypt(dataToEncrypt, RSAEncryptionPadding.OaepSHA256);
                EncryptedPassword = Convert.ToBase64String(bytesDecrypted);
            }
            return EncryptedPassword;
        }
        static private string DecryptPassword(string CertificateThumbprint, string EncryptedBytes)
        {
            string DecryptedPassword = string.Empty;
            X509Certificate2 cert = GetCertificate(CertificateThumbprint);
            using (RSA privateKey = cert.GetRSAPrivateKey())
            {
                byte[] bytesEncrypted = Convert.FromBase64String(EncryptedBytes);
                byte[] bytesDecrypted = privateKey.Decrypt(bytesEncrypted, RSAEncryptionPadding.OaepSHA256);
                DecryptedPassword = Encoding.Unicode.GetString(bytesDecrypted);
            }
            return DecryptedPassword;
        }
        static private X509Certificate2 GetCertificate(string CertificateThumbprint)
        {
            X509Store Store = new X509Store(StoreName.My, StoreLocation.LocalMachine);
            Store.Open(OpenFlags.ReadOnly);
            X509Certificate2Collection certificate = Store.Certificates.Find(X509FindType.FindByThumbprint, CertificateThumbprint, false);
            return certificate[0];
        }
        static void Main(string[] args)
        {
            Thalpius();

            if (args.Length != 3)
            {
                Help();
            }
            if (args[0].StartsWith("/encrypt") && args[1].StartsWith("/certificatethumbprint") && args[2].StartsWith("/password"))
            {
                string CertificateThumbprint = args[1].Remove(0, 23);
                string Password = args[2].Remove(0, 10);
                Console.WriteLine("Encrypted Password: " + EncryptPassword(CertificateThumbprint, Password));
            }
            else if (args[0].StartsWith("/decrypt") && args[1].StartsWith("/certificatethumbprint") && args[2].StartsWith("/encryptedbytes"))
            {
                string CertificateThumbprint = args[1].Remove(0, 23);
                string EncryptedBytes = args[2].Remove(0, 16);
                Console.WriteLine("Decrypted Password: " + DecryptPassword(CertificateThumbprint, EncryptedBytes));
            }
            else
            {
                Help();
            }
        }
    }
}
