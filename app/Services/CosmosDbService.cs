using Microsoft.Azure.Cosmos;
using FamigliaPlus.Api.Models;

namespace FamigliaPlus.Api.Services
{
    public class CosmosDbService
    {
        private readonly Container _container;

        public CosmosDbService(IConfiguration configuration)
        {
            var client = new CosmosClient(configuration["CosmosDb:Endpoint"], configuration["CosmosDb:Key"]);
            _container = client.GetContainer(configuration["CosmosDb:DatabaseId"], configuration["CosmosDb:ContainerId"]);
        }
    }
}