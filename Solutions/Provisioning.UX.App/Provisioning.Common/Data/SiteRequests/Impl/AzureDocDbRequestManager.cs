using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using Microsoft.Azure.Documents.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Provisioning.Common.Data.SiteRequests.Impl
{
    class AzureDocDbRequestManager : AbstractModule, ISiteRequestManager
    {
        public virtual void UsingContext(Action<DocumentClient> action)
        {
            if (string.IsNullOrWhiteSpace(this.ConnectionString)) throw new Exception("ConnectionString is null");
            if (string.IsNullOrWhiteSpace(this.Container)) throw new Exception("ConnectionString is null");

            var connectionProps = this.ConnectionString.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
            var dict = connectionProps
                 .Select(x => x.Split('='))
                 .ToDictionary(i => i[0], i => i[1]);

            var _url = new Uri(dict["AccountEndpoint"]);
            var _authKey = dict["AccountKey"];

            using (DocumentClient _client = new DocumentClient(_url, _authKey))
            {
                action(_client);
            }
        }


        public void CreateNewSiteRequest(SiteRequestInformation siteRequest)
        {

            var database = this.GetDatabase();
            var d = database.Result;

            
            Console.Write("BUB");

            //Task.Run(async () =>
            //{
            //    var d = this.GetDatabase();
            //    Console.Write("BUB");
            //}).Wait();
           
        }

        #region Site REquest Members
        public SiteRequestInformation GetSiteRequestByUrl(string url)
        {
            throw new NotImplementedException();
        }

        public ICollection<SiteRequestInformation> GetNewRequests()
        {
            throw new NotImplementedException();
        }

        public ICollection<SiteRequestInformation> GetApprovedRequests()
        {
            throw new NotImplementedException();
        }

        public bool DoesSiteRequestExist(string url)
        {
            throw new NotImplementedException();
        }

        public void UpdateRequestStatus(string url, SiteRequestStatus status)
        {
            throw new NotImplementedException();
        }

        public void UpdateRequestStatus(string url, SiteRequestStatus status, string statusMessage)
        {
            throw new NotImplementedException();
        }
        #endregion

        private async Task<Database> GetDatabase()
        {

            //if (string.IsNullOrWhiteSpace(this.ConnectionString)) throw new Exception("ConnectionString is null");
            //if (string.IsNullOrWhiteSpace(this.Container)) throw new Exception("ConnectionString is null");

            //var connectionProps = this.ConnectionString.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);

            //var dict = connectionProps
            //     .Select(x => x.Split('='))
            //     .ToDictionary(i => i[0], i => i[1]);

            //var _url = new Uri(dict["AccountEndpoint"]);
            //var _authKey = dict["AccountKey"];

            //var _client = new DocumentClient(_url, _authKey);

            //Database database = _client.CreateDatabaseQuery().Where(db => db.Id == this.Container).ToArray().FirstOrDefault();
            //if (database == null)
            //{
            //    database = await client.CreateDatabaseAsync(new Database { Id = this.Container });
            //}

            Database database = null;

            UsingContext(async _client =>
            {
                database = _client.CreateDatabaseQuery().Where(db => db.Id == this.Container).ToArray().FirstOrDefault();
                if (database == null)
                {
                   database = await _client.CreateDatabaseAsync(new Database { Id = this.Container });
                }
            });
    
            return database;
        }

    }
}
