using Bogus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Bogus
{
    public static class MethodsFramework
    {

        public static string lowerCaseChars = "abcdefghijkmnopqrstuvwxyz";
        public static string upperCaseChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

        public static string specialCharacters = @"@!?%#&*()-";
        public static string digits = "0123456789";

        // This methods generates random characters, digits, uppercase and lowercase
        // letters and adds on random positions to a string passed as the parameter
        // To make sure that password will contain all necessary characters
        // Earlier I thought to use just aA7@ prefix before a password, but such approach
        // seems to be more realistic
        public static string MakePasswordValid(this string password)
        {
            var randomCharacters = new string[]
            {
                GetRandomCharFromString(specialCharacters),
                GetRandomCharFromString(digits),
                GetRandomCharFromString(lowerCaseChars),
                GetRandomCharFromString(upperCaseChars)
             };
            password = password.InsertCharacters(randomCharacters);
            return password;
        }

        public static string GetRandomCharFromString(string s)
        {
            Random random = new Random();
            return s[random.Next(0, s.Length)].ToString();
        }

        public static string InsertCharacters(this string s, string[] characters)
        {
            Random random = new Random();
            foreach (string character in characters)
            {
                s = s.Insert(random.Next(0, s.Length), character);
            }
            return s;
        }

    }
}
