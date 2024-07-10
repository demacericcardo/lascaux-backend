namespace LascauxTest.Helpers
{
    public static class DateHelper
    {
        public static DateTime GetItalianNowDate()
        {
            DateTime utcNow = DateTime.UtcNow;
            TimeZoneInfo italianTimeZone = TimeZoneInfo.FindSystemTimeZoneById("W. Europe Standard Time");
            return TimeZoneInfo.ConvertTimeFromUtc(utcNow, italianTimeZone);
        }
    }
}
