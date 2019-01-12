using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;

namespace Peyk.Matrix.Client
{
    [JsonObject(
        ItemNullValueHandling = NullValueHandling.Ignore,
        NamingStrategyType = typeof(SnakeCaseNamingStrategy)
    )]
    public struct MatrixClientError
    {
        [JsonProperty("errcode")] public string Code { get; }

        [JsonProperty("error")] public string Message { get; }

        public int HttpStatusCode { get; }

        public IReadOnlyDictionary<string, JToken> Extras { get; }

        internal MatrixClientError(JObject jObject, int httpStatusCode)
        {
            if (jObject is null)
                throw new ArgumentNullException(nameof(jObject));

            string code = jObject["errcode"]?.ToString();
            string message = jObject["error"]?.ToString();

            if (string.IsNullOrWhiteSpace(code))
                throw new ArgumentException("Required key \"errcode\" has no value.", nameof(jObject));

            if (string.IsNullOrWhiteSpace(message))
                throw new ArgumentException("Required key \"error\" has no value.", nameof(jObject));

            Code = code;
            Message = message;
            HttpStatusCode = httpStatusCode;
            Extras = jObject.Properties()
                .Where(p => !new[] {"errcode", "error"}.Contains(p.Name))
                .ToDictionary(
                    p => p.Name,
                    p => p.Value
                );
        }
    }
}