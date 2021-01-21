using System;

namespace Utils
{
    public class DateUtil
    {
        public static string DateCraftTimer(int time)
        {
            var result = TimeSpan.FromSeconds(time);
            if (result.Hours > 0) 
                return $"{result.Hours:#0}:{result.Minutes:00}:{result.Seconds:00}";
            
            if (result.Minutes > 0)
                return $"{result.Minutes:#0}:{result.Seconds:00}";

            return $"{result.Seconds:#0}";
        }

        public static int GetTime(long time)
        {
            var timeEnd = DateTime.FromFileTime(time);
            
            if (DateTime.Now < timeEnd)
                return (int)(timeEnd - DateTime.Now).TotalSeconds;

            return 0;
        }
        
        public static DateTime StartOfTheDay(DateTime d) => new DateTime(d.Year, d.Month, d.Day, 0, 0,0);
        public static DateTime EndOfTheDay(DateTime d) => new DateTime(d.Year, d.Month, d.Day, 23, 59,59);

    }
}
