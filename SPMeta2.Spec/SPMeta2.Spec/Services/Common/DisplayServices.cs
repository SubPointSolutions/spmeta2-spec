using System;
using System.Collections.Generic;
using System.Linq;

namespace SPMeta2.Spec.Services.Common
{
    public class DisplayService
    {
        #region methods

        public string ToTitleCase(string value)
        {
            if (string.IsNullOrEmpty(value))
                return string.Empty;

            var result = new List<string>();
            var chars = value.ToCharArray().ToList();

            if (chars.Count > 0)
            {
                for (var i = 0; i < chars.Count; i++)
                {
                    var c = chars[i];

                    if (i == 0 && c == chars[0])
                    {
                        if (c == char.ToUpper(c))
                            result.Add(c.ToString());
                        else
                            result.Add(c.ToString().ToUpper());

                        continue;
                    }

                    if (i > 0
                        && !Char.IsWhiteSpace(chars[i - 1])
                        && chars[i - 1] != Char.ToUpper(chars[i - 1])
                        && !Char.IsWhiteSpace(c)
                        && c == char.ToUpper(c))
                    {
                        result.Add(" " + c.ToString().ToUpper());
                        continue;
                    }

                    result.Add(c.ToString());
                }
            }

            return string.Join(string.Empty, result).Trim();
        }

        public string Pluralize(string value)
        {
            var tmp = System.Data.Entity.Design.PluralizationServices.PluralizationService.CreateService(new System.Globalization.CultureInfo("en-US"));

            return tmp.Pluralize(value);
        }

        #endregion
    }
}
