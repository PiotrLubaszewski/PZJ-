using System.Linq;
using System.Text;

namespace Timesheet.Core.Extensions
{
    public static class StringExtensions
    {
        public static string ToCamelCase(this string str) => 
            string.IsNullOrEmpty(str) 
            ? string.Empty 
            : str[0].ToString().ToLower() + str.Substring(1, str.Length - 1);

        public static string ToPascalCase(this string str) =>
            string.IsNullOrEmpty(str)
            ? string.Empty
            : str[0].ToString().ToUpper() + str.Substring(1, str.Length - 1);

        public static string InsertSpaces(this string str)
        {
            if (!str.Any(x => x.ToString().ToUpper().First() == x))
                return str;

            var sb = new StringBuilder();
            for (int i = 0; i < str.Length; i++)
            {
                if (i == 0 || str[i].ToString().ToUpper()[0] != str[i])
                    sb.Append(str[i]);

                else sb.Append(" " + str[i]);
            }

            return sb.ToString();
        }
    }
}
