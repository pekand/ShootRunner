using System.Text;

#pragma warning disable IDE0130

namespace ShootRunner
{
    public class Generator
    {
        private static readonly string Characters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        private static readonly Random Random = new();

        public static string Uid(int length = 32)
        {
            StringBuilder result = new(length);
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
