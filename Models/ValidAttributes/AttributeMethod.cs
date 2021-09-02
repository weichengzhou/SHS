using System.Collections.Generic;


namespace SHS.Models.ValidAttributes
{
    /// <summary>
    /// About field attribute method, use for customize validator.
    /// </summary>
    public class AttributeMethod
    {
        /// <summary>
        /// Merge attribute into field if the key not exist.
        /// </summary>
        /// <param name="attributes">Context attribute about the field.</param>
        /// <param name="key">The key of attribute will add to field.</param>
        /// <param name="value">The value of attribute will add to field.</param>
        public static void MergeAttribute(IDictionary<string, string> attributes, string key, string value)
        {
            if(!attributes.ContainsKey(key))
            {
                attributes.Add(key, value);
            }
        }
    }
}