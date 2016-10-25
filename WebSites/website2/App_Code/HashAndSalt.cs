using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Cryptography;

/// <summary>
/// Summary description for HashAndSalt
/// </summary>
public class HashAndSalt
{
    public HashAndSalt()
    {
    }

    public string Hash(string password, string datetime = null)
    {
        MD5 md5 = System.Security.Cryptography.MD5.Create();

        if (string.IsNullOrEmpty(datetime))
        {
            datetime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        }

        byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(password);
        byte[] inputSalt = System.Text.Encoding.ASCII.GetBytes(datetime);
        byte[] salt = new byte[inputBytes.Length + inputSalt.Length];
        byte[] hash = md5.ComputeHash(inputBytes);

        StringBuilder hashedPassword = new StringBuilder();

        for (int i = 0; i < hash.Length; i++)

        {

            hashedPassword.Append(hash[i].ToString("x2"));

        }

        return hashedPassword.ToString();
    }

}