namespace EnaApp.Helpers
{
    public static class StringUtils
    {
        public static string CapitalizeFirstLetter(string s)
        {
            // TODO: There is a better version in SubtitleEdit (with handle unicode)
            if (s.Length > 1)
            {
                return char.ToUpper(s[0]) + s.Substring(1).ToLower();
            }
            return s;
        }
    }
}