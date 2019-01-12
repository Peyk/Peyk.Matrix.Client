using System.Net.Http;
using Framework;
using Newtonsoft.Json;
using Peyk.Matrix.Client.Requests;
using Xunit;

namespace UnitTests.Serialization
{
    [CollectionDefinition("Serialization")]
    public class ListPublicRoomsRequestTests
    {
        [Fact]
        public void Should_Serialize_Request_ListPublicRooms_Empty()
        {
            ListPublicRoomsRequest request = new ListPublicRoomsRequest();

            string json = JsonConvert.SerializeObject(request);

            Assert.Equal("{}", json);
        }

        [Fact]
        public void Should_Deserialize_Request_ListPublicRooms_Empty()
        {
            const string json = "{}";
            ListPublicRoomsRequest request = JsonConvert.DeserializeObject<ListPublicRoomsRequest>(json);

            Assert.False(request.RequiresAuth);
            Assert.Null(request.Limit);
            Assert.Null(request.Since);
            Assert.Null(request.Server);
            Assert.Equal(HttpMethod.Get, request.HttpMethod);
            Assert.Equal("/_matrix/client/r0/publicRooms", request.Url);
        }

        [Fact]
        public void Should_Serialize_Request_ListPublicRooms()
        {
            ListPublicRoomsRequest request = new ListPublicRoomsRequest
            {
                Limit = 0,
                Since = "p1902",
                Server = "matrix.org",
            };

            string json = JsonConvert.SerializeObject(request);

            Asserts.JsonEqual(
                @"{
                    limit: 0,
                    since: ""p1902"",
                    server: ""matrix.org""
                }",
                json
            );
        }

        [Fact]
        public void Should_Deserialize_Request_ListPublicRooms()
        {
            const string json = @"{
                    limit: 50,
                    since: ""p1902"",
                    server: ""matrix.org""
            }";
            ListPublicRoomsRequest request = JsonConvert.DeserializeObject<ListPublicRoomsRequest>(json);

            Assert.False(request.RequiresAuth);
            Assert.Equal(50, request.Limit);
            Assert.Equal("p1902", request.Since);
            Assert.Equal("matrix.org", request.Server);
            Assert.Equal(HttpMethod.Get, request.HttpMethod);
            Assert.Equal("/_matrix/client/r0/publicRooms?limit=50&since=p1902&server=matrix.org", request.Url);
        }
    }
}