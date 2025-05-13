using DeansOffice.Database.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Security.RightsManagement;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DeansOffice.Database
{
    public class DatabaseQueries
    {
        private static readonly string key = Config.key;
        private static readonly string iv = Config.iv;

        public DatabaseQueries() { }
        public static bool RegisterNewUser(string lastName, string firstName, 
            string middleName, string email, string phoneNumber, string password, string department, string academicTitle)
        {
            var db = new DeanDbContext();

            Teacher teacher = new Teacher();
            teacher.FirstName = firstName;
            teacher.LastName = lastName;
            teacher.PhoneNumber = phoneNumber;
            teacher.Department = department;
            teacher.MiddleName = middleName;
            teacher.Email = email;
            teacher.AcademicTitle = academicTitle;
            teacher.Password_hashed = EncryptString(password);

            Teacher same_email = db.Teachers.FirstOrDefault(x => x.Email == email);

            if (same_email != null)
            {
                return false;
            }

            db.Teachers.Add(teacher);
            db.SaveChanges();

            return true;


        }
        public static void Login(string email, string password)
        {
            var db = new DeanDbContext();
            password = EncryptString(password);

            var teacher = db.Teachers.FirstOrDefault(x => x.Email == email && DecryptString(x.Password_hashed) == DecryptString(password));

        }
        private static string EncryptString(string plainText)
        {
           
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = Encoding.UTF8.GetBytes(key);
                aesAlg.IV = Encoding.UTF8.GetBytes(iv);

                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {
                            swEncrypt.Write(plainText);
                        }
                        return Convert.ToBase64String(msEncrypt.ToArray());
                    }
                }
            }
        }
        private static string DecryptString(string cipherText)
        {
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = Encoding.UTF8.GetBytes(key);
                aesAlg.IV = Encoding.UTF8.GetBytes(iv);

                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                using (MemoryStream msDecrypt = new MemoryStream(Convert.FromBase64String(cipherText)))
                {
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                        {
                            return srDecrypt.ReadToEnd();
                        }
                    }
                }
            }
        }

       

    }
}
