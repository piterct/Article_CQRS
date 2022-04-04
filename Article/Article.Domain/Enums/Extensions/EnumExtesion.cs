using System;
using System.ComponentModel;

namespace Article.Domain.Enums.Extensions
{
    public static  class EnumExtesion
    {
        public static string DescricaoParaString<TEnum>(TEnum valueEnum) where TEnum : struct, IConvertible, IComparable, IFormattable
        {
            if (!typeof(TEnum).IsEnum)
            {
                throw new ArgumentException("Parametro enviado não é um enumerador!");
            }

            DescriptionAttribute[] attributes = (DescriptionAttribute[])valueEnum
               .GetType()
               .GetField(valueEnum.ToString())
               .GetCustomAttributes(typeof(DescriptionAttribute), false);
            return attributes.Length > 0 ? attributes[0].Description : string.Empty;
        }
    }
}
