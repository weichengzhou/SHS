using System.Collections.Generic;


namespace SHS.Models.ValidAttributes
{
    public class AttributeMethod
    {
        public static bool MergeAttribute(IDictionary<string, string> attributes, string key, string value)
        {
            if(attributes.ContainsKey(key))
            {
                return false;
            }
            attributes.Add(key, value);
            return true;
        }
    }
}