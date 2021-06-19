using Microsoft.Azure.Cosmos.Table;
using System;

namespace ApiApp.Model
{
    public class User : TableEntity
    {
        public string UserName { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
