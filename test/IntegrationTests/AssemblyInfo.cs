using Framework;
using IntegrationTests.Shared;
using Xunit;

[assembly: CollectionBehavior(DisableTestParallelization = true)]

[assembly: TestCollectionOrderer(
    "IntegrationTests.Shared." + nameof(TestCollectionOrderer),
    "IntegrationTests"
)]

[assembly: TestCaseOrderer(TestConstants.TestCaseOrderer, TestConstants.AssemblyName)]