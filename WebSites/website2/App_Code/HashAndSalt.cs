using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

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
        if (string.IsNullOrEmpty(datetime))
        {
            datetime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        }

        string salt = password + datetime;
        byte[] array = new byte[salt.Length];
        int i = 0;

        foreach (char bit in salt)
        {
            array[i] = Convert.ToByte(bit);
            i++;
        }

        return Convert.ToBase64String(array);
    }

}