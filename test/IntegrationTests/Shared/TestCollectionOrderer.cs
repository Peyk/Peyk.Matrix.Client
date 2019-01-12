using Framework;

namespace IntegrationTests.Shared
{
    public class TestCollectionOrderer : TestCollectionOrdererBase
    {
        private static readonly string[] Collections =
        {
            "public rooms",
        };

        public TestCollectionOrderer()
            : base(Collections)
        {
        }
    }
}