using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Peyk.Matrix.Client.Types
{
    [JsonObject(
        ItemNullValueHandling = NullValueHandling.Ignore,
        NamingStrategyType = typeof(SnakeCaseNamingStrategy)
    )]
    public struct PaginatedResponse<TElement>
    {
        /// <summary>
        /// Required. A paginated chunk of public rooms.
        /// </summary>
        public IEnumerable<TElement> Chunk { get; set; }

        /// <summary>
        /// A pagination token for the response.
        /// The absence of this token means there are no more results to fetch and the client should stop paginating.
        /// </summary>
        public string NextBatch { get; set; }

        /// <summary>
        /// A pagination token that allows fetching previous results.
        /// The absence of this token means there are no results before this batch, i.e. this is the first batch.
        /// </summary>
        public string PrevBatch { get; set; }

        /// <summary>
        /// An estimate on the total number of public rooms, if the server has an estimate.
        /// </summary>
        public int? TotalRoomCountEstimate { get; set; }
    }
}