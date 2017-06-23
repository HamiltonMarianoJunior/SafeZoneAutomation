using System;
using System.Linq;

namespace AutomationFramework.Generators
{
    public static class TextValuesGenerator
    {
        private static Random random = new Random();
        private static string chars = "abcdefghijklmnopqrstuvwxyz";
        private static string numbers = "0123456789";

        private const string UserName = "";
        private const string MasterUserName = "";
        private const string Password = "";

        public static string GetTeamAdministratorName()
        {
            return MasterUserName;
        }

        public static string GetPlayerName()
        {
            return UserName;
        }

        public static string GetUserPassword()
        {
            return Password;
        }

        public static string TextGenerator(int maxValue)
        {            
            return new string(Enumerable.Repeat(chars, maxValue)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public static string EmailGenerator()
        {
            return string.Format("{0}@{1}.com", TextGenerator(5), TextGenerator(5));
        }

        public static string PhoneGenerator()
        {
            return string.Format("{0}-{1}",
                new string(Enumerable.Repeat(numbers, 4).Select(s => s[random.Next(s.Length)]).ToArray()),
                new string(Enumerable.Repeat(numbers, 4).Select(s => s[random.Next(s.Length)]).ToArray()));            
        }
    }
}