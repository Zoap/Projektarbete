using System;
using System.Text;
using System.Security.Cryptography;

/// <summary>
/// Summary description for User
/// </summary>
public class User
{
    private string _userName;
    private string _password;
    private string _email;
    private string _createTime;
    private string password;
    
    public string Username { get { return _userName; } }
    public string Password { get { return _password; } }
    public string Email { get { return _email; } }
    public string Createtime { get { return _createTime; } }

    public User(string username, string password, string email = null)
    {
        _createTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        _userName = username;
        _password = Hash(password, _createTime);
        this.password = password;
        _email = email;
    }
    private string Hash(string password, string datetime)
    {
        SHA256 md5 = SHA256.Create();

        string salt = password.GetHashCode().ToString();
        string salted = password + salt + datetime;
        byte[] newPassword = Encoding.ASCII.GetBytes(salted);
        byte[] hash = md5.ComputeHash(newPassword);

        StringBuilder hashedPassword = new StringBuilder();

        for (int i = 0; i < hash.Length; i++)
        {
            hashedPassword.Append(hash[i].ToString("x2"));
        }

        return hashedPassword.ToString();
    }

    public string Login(string date)
    {
        return Hash(password, date);
    }
}