using ApiApp.Infrastructure;
using ApiApp.Model;
using Microsoft.Azure.Cosmos.Table;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApiApp.Repository
{
    public class UserRepository : IUserRepository
    {
        public UserRepository(ISettings settings)
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(settings.TableConnectionString);

            var tableClient = storageAccount.CreateCloudTableClient();

            UserTable = tableClient.GetTableReference("User");
            UserTable.CreateIfNotExists();
        }

        public CloudTable UserTable { get; private set; }

        public IEnumerable<User> GetUsersByName(string name) 
        {
            var query = new TableQuery<User>().Where(TableQuery.GenerateFilterCondition(nameof(User.UserName),QueryComparisons.Equal, name));
            return UserTable.ExecuteQuery(query);
        }

        public async Task AddUser(User user) 
        {
            if (user.PartitionKey == null)
                user.PartitionKey = DateTime.Today.Year.ToString();
            if (user.RowKey == null)
                user.RowKey = Guid.NewGuid().ToString();

            var operation = TableOperation.InsertOrReplace(user);

            await UserTable.ExecuteAsync(operation);
        }
    }
}
