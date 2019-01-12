using System;
using Peyk.Matrix.Client.Types;
using Xunit;

// ReSharper disable once CheckNamespace
namespace IntegrationTests.Shared
{
    public static partial class MatrixAssert
    {
        /// <summary>Verifies that a <see cref="PublicRoomsChunk"/> instance is valid.</summary>
        /// <exception cref="Xunit.Sdk.TrueException">Thrown when the instance is invalid.</exception>
        public static void ValidRoomChunk(PublicRoomsChunk roomChunk)
        {
            Assert.True(0 < roomChunk.NumJoinedMembers);
            Assert.NotEmpty(roomChunk.RoomId);

            if (roomChunk.Aliases != null)
                Assert.All(roomChunk.Aliases, Assert.NotEmpty);

            if (roomChunk.CanonicalAlias != null)
                Assert.NotEmpty(roomChunk.CanonicalAlias);

            if (roomChunk.Name != null)
                Assert.NotEmpty(roomChunk.Name);

            if (roomChunk.Topic != null)
                Assert.NotEmpty(roomChunk.Topic);

            if (roomChunk.AvatarUrl != null)
            {
                Assert.NotEmpty(roomChunk.AvatarUrl);
                Assert.True(Uri.TryCreate(roomChunk.AvatarUrl, UriKind.Absolute, out _));
            }
        }
    }
}