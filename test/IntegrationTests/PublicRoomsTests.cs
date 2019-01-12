using System.Linq;
using System.Threading.Tasks;
using Framework;
using IntegrationTests.Shared;
using Peyk.Matrix.Client;
using Peyk.Matrix.Client.Requests;
using Peyk.Matrix.Client.Types;
using Xunit;

namespace IntegrationTests
{
    [Collection("public rooms")]
    public class PublicRoomsTests : IClassFixture<Fixture>
    {
        private Fixture Fixture { get; }

        public PublicRoomsTests(Fixture fixture)
        {
            Fixture = fixture;
        }

        [OrderedFact(DisplayName = "Should register a user successfully")]
        public async Task Should_Register_User()
        {
            IMatrixClient client = new MatrixClient(
                Fixture.Settings.Server
            );

            PaginatedResponse<PublicRoomsChunk> response = await client.MakeRequestAsync(
                new ListPublicRoomsRequest {Limit = 8}
            );

            Assert.Null(response.PrevBatch);
            Assert.NotEmpty(response.NextBatch);

            if (response.TotalRoomCountEstimate != null)
                Assert.True(
                    0 < response.TotalRoomCountEstimate,
                    "Rooms count estimate should be greater than 0."
                );

            Assert.NotNull(response.Chunk);
            Assert.Equal(8, response.Chunk.Count());

            foreach (PublicRoomsChunk roomChunk in response.Chunk)
            {
                MatrixAssert.ValidRoomChunk(roomChunk);
            }
        }
    }
}