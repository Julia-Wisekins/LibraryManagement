using System;
using System.ComponentModel;
using System.Reflection;

namespace LibraryManagementFrontEnd
{
    public static class Extensions
    {
        /// <summary>
        /// Gets the Enum in a readable formatt
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string GetEnumDescription(this Enum value)
        {
            FieldInfo fi = value.GetType().GetField(value.ToString());

            DescriptionAttribute[] attributes = fi.GetCustomAttributes(typeof(DescriptionAttribute), false) as DescriptionAttribute[];

            if (attributes != null && attributes.Any())
            {
                return attributes.First().Description;
            }

            return value.ToString();
        }


    } 
}
