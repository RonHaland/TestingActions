using Microsoft.Azure.Cosmos.Table;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApiApp.Model
{
    public interface IUserRepository
    {
        IEnumerable<User> GetUsersByName(string name);
        Task AddUser(User user);
    }

    public class UserRepository : IUserRepository
    {
        public UserRepository()
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse("DefaultEndpointsProtocol=http;AccountName=devstoreaccount1;AccountKey=Eby8vdM02xNOcqFlqUwJPLlmEtlCDXJ1OUzFT50uSRZ6IFsuFq2UVErCz4I6tq/K1SZFPTOtr/KBHBeksoGMGw==;BlobEndpoint=http://127.0.0.1:10000/devstoreaccount1;QueueEndpoint=http://127.0.0.1:10001/devstoreaccount1;TableEndpoint=http://127.0.0.1:10002/devstoreaccount1;");

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
            var operation = TableOperation.InsertOrReplace(user);

            await UserTable.ExecuteAsync(operation);
        }
    }

    public class User : TableEntity
    {
        public string UserName { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
