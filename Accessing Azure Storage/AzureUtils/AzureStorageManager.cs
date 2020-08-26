using AzureUtils.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Cosmos.Table;
using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Threading.Tasks;

namespace AzureUtils
{
    public class AzureStorageManager
    {
        private CloudStorageAccount _storageAccount = null;        
        private string _connectionString;
        private string _partitionKey;

        public string PartitionKey { get => _partitionKey; set => _partitionKey = value; }
        public string ConnectionString { get => _connectionString; set => _connectionString = value; }

        public AzureStorageManager(string connectionString, string partitionKey)
        {
            ConnectionString = connectionString;
            PartitionKey = partitionKey;
            _storageAccount = CloudStorageAccount.Parse(connectionString);
        }


        private async Task<CloudTable> CreateTableAsync(string tableName)
        {
            // Create a table client for interacting with the table service
            CloudTableClient tableClient = _storageAccount.CreateCloudTableClient();

            // Create a table client for interacting with the table service 
            CloudTable table = tableClient.GetTableReference(tableName);
            if (await table.CreateIfNotExistsAsync())
            {
                Console.WriteLine("Created Table named: {0}", tableName);
            }
            else
            {
                Console.WriteLine("Table {0} already exists", tableName);
            }
            
            return table;
        }

        /// <summary>
        /// Saves a record to Azure table storage
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="record"></param>
        /// <returns></returns>
        public async Task<ActionResult> SaveAutomationRecord(string tableName, TableEntity record)
        {
            try
            {
                if (record == null)
                {
                    throw new ArgumentNullException("automation record null");
                }

                CloudTable table = await CreateTableAsync(tableName);

                // Create the InsertOrReplace table operation
                TableOperation insertOrMergeOperation = TableOperation.InsertOrMerge(record);
                await table.ExecuteAsync(insertOrMergeOperation);

                return new OkResult();
            }
            catch (StorageException e)
            {
                Console.WriteLine(e.Message);
                Console.ReadLine();
                throw;
            }
        }

        /// <summary>
        /// Retrieves a specific workitem record from the log
        /// </summary>
        /// <param name="workItemId"></param>
        /// <returns></returns>
        public async Task<DesignAutomationRecord[]> ReadAutomationRecord(string logTableName, string workItemId = null)
        {
            TableQuery<DesignAutomationRecord> query;

            if (!String.IsNullOrEmpty(workItemId))
            {
                query = new TableQuery<DesignAutomationRecord>()
                        .Where(TableQuery.GenerateFilterCondition("WorkItemId", QueryComparisons.Equal, workItemId.Trim()));
            } else //get all records
            {
                query = new TableQuery<DesignAutomationRecord>();
            }

            CloudTableClient client = _storageAccount.CreateCloudTableClient();
            CloudTable table = client.GetTableReference(logTableName);
            TableContinuationToken continuationToken = null;
            TableQuerySegment<DesignAutomationRecord> res = await table.ExecuteQuerySegmentedAsync(query, continuationToken);
            return res.ToArray<DesignAutomationRecord>();
        }

        /// <summary>
        /// Update an entity
        /// </summary>
        /// <param name="status"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<ActionResult> UpdateLoggedWorkItemStatus(string status, DesignAutomationRecord entity, string logTableName)
        {
            try
            {
                CloudTableClient client = _storageAccount.CreateCloudTableClient();
                CloudTable table = client.GetTableReference(logTableName);
                entity.WorkItemStatus = status;             
                await table.ExecuteAsync(TableOperation.Merge(entity));
                return new OkResult();
            }
            catch (StorageException se)
            {
                throw new Exception("Exception in UpdateLoggedWorkItemStatus ", se);
            }
        }

    }
}

