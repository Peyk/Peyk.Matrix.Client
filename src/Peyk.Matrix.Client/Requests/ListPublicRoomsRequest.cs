using System.Net.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Peyk.Matrix.Client.Extensions;
using Peyk.Matrix.Client.Types;

namespace Peyk.Matrix.Client.Requests
{
    /// <summary>
    /// List the public rooms on the server.
    /// This API returns paginated responses.
    /// The rooms are ordered by the number of joined members, with the largest rooms first.
    /// </summary>
    /// <see href="https://matrix.org/docs/spec/client_server/r0.4.0.html#get-matrix-client-r0-publicrooms"/>
    [JsonObject(
        ItemNullValueHandling = NullValueHandling.Ignore,
        NamingStrategyType = typeof(SnakeCaseNamingStrategy)
    )]
    public class ListPublicRoomsRequest : IRequest<PaginatedResponse<PublicRoomsChunk>>
    {
        /// <summary>
        /// Limit the number of results returned.
        /// </summary>
        public int? Limit { get; set; }

        /// <summary>
        /// A pagination token from a previous request, allowing clients to get the next (or previous) batch of rooms.
        /// The direction of pagination is specified solely by which token is supplied, rather than via an explicit
        /// flag.
        /// </summary>
        public string Since { get; set; }

        /// <summary>
        /// The server to fetch the public room lists from. Defaults to the local server.
        /// </summary>
        public string Server { get; set; }

        /// <inheritdoc />
        [JsonIgnore]
        public string Url
        {
            get
            {
                string query = "?".AddOptionalQueryParams(
                    "limit", Limit,
                    "since", Since,
                    "server", Server
                );
                return "/_matrix/client/r0/publicRooms" + (query.Length > 1 ? query : "");
            }
        }

        /// <inheritdoc />
        [JsonIgnore]
        public HttpMethod HttpMethod => HttpMethod.Get;

        /// <inheritdoc />
        [JsonIgnore]
        public bool RequiresAuth => false;

        /// <inheritdoc />
        public HttpContent ToHttpContent() => null;
    }
}