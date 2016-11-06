using System;
using System.Text;
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
        MD5 md5 = MD5.Create();

        if (string.IsNullOrEmpty(datetime))
        {
            datetime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        }

        string random = "iKr&9Yz#1M";
        string salted = password + random + datetime;
        byte[] salt = Encoding.ASCII.GetBytes(salted);
        byte[] hash = md5.ComputeHash(salt);
        

        StringBuilder hashedPassword = new StringBuilder();

        for (int i = 0; i < hash.Length; i++)

        {

            hashedPassword.Append(hash[i].ToString("x2"));

        }

        

        return hashedPassword.ToString();
    }

}