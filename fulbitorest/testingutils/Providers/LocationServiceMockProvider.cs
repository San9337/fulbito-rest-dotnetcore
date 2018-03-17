using FulbitoRest.Services.Contracts;
using Moq;

namespace testingutils.Providers
{
    public static class LocationServiceMockProvider
    {
        public static Mock<ILocationService> Get()
        {
            var mock = new Mock<ILocationService>();

            return mock;
        }
    }
}
