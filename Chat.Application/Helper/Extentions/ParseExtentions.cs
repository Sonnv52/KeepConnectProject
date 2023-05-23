namespace Chat.Application.Helper.Extentions
{
    public static class ParseExtensions
    {
        public static T Parse<T>(this string input) where T : struct
        {
            return (T)Convert.ChangeType(input, typeof(T));
        }
    }
}
