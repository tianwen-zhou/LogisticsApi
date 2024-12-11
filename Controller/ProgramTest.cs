using System;
using System.Security.Cryptography;
using System.Text;

class ProgramTest
{
    static void Main(string[] args)
    {
        Console.Write("Enter a password to hash: ");
        string password = Console.ReadLine();

        string hashedPassword = GeneratePasswordHash(password);

        Console.WriteLine($"Password: {password}");
        Console.WriteLine($"Hashed Password: {hashedPassword}");
    }

    private static string GeneratePasswordHash(string password)
    {
        using (var sha256 = SHA256.Create())
        {
            var hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(hashBytes);
        }
    }
}
