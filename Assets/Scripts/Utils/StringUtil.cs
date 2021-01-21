using System.Globalization;
using JetBrains.Annotations;

namespace Utils
{
    [UsedImplicitly]
    public class StringUtil
    {
        public static string PriceFullFormat(int price)
        {
            return price.ToString("N0", new NumberFormatInfo
            {
                NumberGroupSizes = new[] { 3 },
                NumberGroupSeparator = " "
            });
        }
        
        public static string PriceShortFormat(int price)
        {
            return price switch
            {
                >= 1000 and < 1000000 => $"{(float)price / 1000}K",
                >= 1000000 and < 1000000000 => $"{(float)price / 1000000}M",
                _ => string.Empty
            };
        }
    }
}
