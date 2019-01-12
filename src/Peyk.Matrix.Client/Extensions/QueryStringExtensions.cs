using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace Peyk.Matrix.Client.Extensions
{
    internal static class QueryStringExtensions
    {
        public static string AddOptionalQueryParams(this string url, params object[] qParams)
        {
            string result;
            if (qParams.Length == 0)
            {
                result = url;
            }
            else if (qParams.Length % 2 == 0)
            {
                var pairs = new List<string>(qParams.Length / 2);
                for (int i = 0; i < qParams.Length - 1; i += 2)
                {
                    string name = qParams[i].ToString();
                    var value = qParams[i + 1];

                    if (value != null)
                        pairs.Add(WebUtility.UrlEncode(name) + '=' + WebUtility.UrlEncode(value.ToString()));
                }

                if (pairs.Any())
                {
                    result = url + string.Join("&", pairs);
                }
                else
                {
                    result = url;
                }
            }
            else
            {
                throw new ArgumentException();
            }

            return result;
        }
    }
}