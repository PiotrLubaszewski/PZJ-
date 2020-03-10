namespace Timesheet.Core.Extensions
{
    public static class StringExtensions
    {
        public static string ToCamelCase(this string str) => str[0].ToString().ToLower() + str.Substring(1, str.Length - 1);
    }
}
