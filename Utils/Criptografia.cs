using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RpgApi.Models;


public class Criptografia
{
    public static void CriarPasswordHash(string password, out byte[] hash, out byte[] salt)
    {
        using (var hmac = new System.Security.Cryptography.HMACSHA512())
        {
            salt = hmac.Key;
            hash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
        }
    }

    public static bool VerificarPasswordHash(string password, byte[] hash, byte[] salt)
    {
        using (var hmac = new System.Security.Cryptography.HMACSHA512(salt))
        {
            var ComputeHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            for (int i = 0; i < ComputeHash.Length; i++)
            {
                if (ComputeHash[i] != hash[i])
                {
                    return false;
                }
            }
            return true;
        }
    }
}






