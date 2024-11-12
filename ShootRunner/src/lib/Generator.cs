using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShootRunner
{
    public class Generator
    {
        private static readonly string Characters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        private static readonly Random Random = new Random();

        public static string uid(int length = 32)
        {
            StringBuilder result = new StringBuilder(length);
            int charactersLength = Characters.Length;

            for (int i = 0; i < length; i++)
            {
                int randomIndex = Random.Next(charactersLength);
                result.Append(Characters[randomIndex]);
            }

            return result.ToString();
        }

    }
}
