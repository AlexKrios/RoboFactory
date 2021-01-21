using System;

namespace Utils
{
    public class EnumParse
    {
        public static T ParseStringToEnum<T>(string value)
        {
            return (T)Enum.Parse(typeof(T), value, true);
        }
        
        public static TResult ParseEnumToEnum<TOriginal, TResult>(TOriginal original)
        {
            var originalString = original.ToString();
            
            return (TResult)Enum.Parse(typeof(TResult), originalString, true);
        }
    }
}
