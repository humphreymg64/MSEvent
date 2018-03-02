﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using ContosoBaggage.Common.Models;


namespace ContosoBaggage.Backend.Functions.Services
{
    public partial class CosmosDataService
    {
        private static string _databaseId = "bagDatabase";
        private static string _collectionId = "";
        DocumentClient _client;

        Uri _collectionLink = UriFactory.CreateDocumentCollectionUri(_databaseId, _collectionId);

        private static readonly Lazy<CosmosDataService> lazy =
            new Lazy<CosmosDataService>(() => new CosmosDataService());


        public CosmosDataService()
        {
            _client = new DocumentClient(new Uri(Keys.Cosmos.Url), Keys.Cosmos.Key, ConnectionPolicy.Default);
        }

        static CosmosDataService _instance;
        public static CosmosDataService Instance(string collectionId)
        {
            _collectionId = collectionId;

            return _instance ?? (_instance = new CosmosDataService());
        }

        Uri GetCollectionUri()
        {
            return UriFactory.CreateDocumentCollectionUri(_databaseId, _collectionId);
        }

        Uri GetDocumentUri(string id)
        {
            return UriFactory.CreateDocumentUri(_databaseId, _collectionId, id);
        }

        /// <summary>
        /// Ensures the database and collection are created
        /// </summary>
        async Task EnsureDatabaseConfigured()
        {
            var db = new Database { Id = _databaseId };
            var collection = new DocumentCollection { Id = _collectionId };

            var result = await _client.CreateDatabaseIfNotExistsAsync(db);

            if (result.StatusCode == HttpStatusCode.Created || result.StatusCode == HttpStatusCode.OK)
            {
                var dbLink = UriFactory.CreateDatabaseUri(_databaseId);
                var colResult = await _client.CreateDocumentCollectionIfNotExistsAsync(dbLink, collection);
            }
        }

        /// <summary>
        /// Fetches an item based on it's Id
        /// </summary>
        /// <returns>The serialized item object</returns>
        /// <param name="id">The Id of the item to retrieve</param>
        public async Task<T> GetItemAsync<T>(string id) where T : BaseModel, new()
        {
            try
            {
                var docUri = GetDocumentUri(id);
                var result = await _client.ReadDocumentAsync<T>(docUri);

                if (result.StatusCode == HttpStatusCode.OK)
                {
                    return result.Document;
                }

                return null;
            }
            catch (DocumentClientException dce)
            {
                if (dce.StatusCode == HttpStatusCode.NotFound)
                    await EnsureDatabaseConfigured();

                return null;
            }
        }

        /// <summary>
        /// Inserts the document into the collection and creates the database and collection if it has not yet been created
        /// </summary>
        /// <param name="item">The item to add</param>
        public async Task InsertItemAsync<T>(T item) where T : BaseModel
        {
            try
            {
                var result = await _client.CreateDocumentAsync(_collectionLink, item);
                item.Id = result.Resource.Id;
            }
            catch (DocumentClientException dce)
            {
                if (dce.StatusCode == HttpStatusCode.NotFound)
                {
                    await EnsureDatabaseConfigured();
                    await InsertItemAsync(item);
                }
            }
        }

        /// <summary>
        /// Updates the document
        /// </summary>
        /// <param name="item">The item to update</param>
        public async Task UpdateItemAsync<T>(T item) where T : BaseModel
        {
            await _client.ReplaceDocumentAsync(GetDocumentUri(item.Id), item);
        }

        /// <summary>
		/// Returns the first open/existing game that is either coordinated or joined by the email provided
		/// </summary>
		/// <param name="email">The players's email address</param>
		/// <returns>A dynamic Game, should one exist</returns>
		public List<Flight> GetFlights()
        {
            try
            {
                {
                    //First lets check to see if the user has a game they're coordinating
                    var sql = $"SELECT * FROM c";
                    var query = _client.CreateDocumentQuery<Flight>(GetCollectionUri(), sql, new FeedOptions { EnableCrossPartitionQuery = true });

                    return query.ToList<Flight>();
                }                

                return null;
            }
            catch (Exception e)
            {
                var dce = e.GetBaseException() as DocumentClientException;

                if (dce != null && dce.StatusCode == HttpStatusCode.NotFound)
                {
                    EnsureDatabaseConfigured().Wait();
                    return null;
                }

                throw;
            }
        }

        /// <summary>
		/// Returns the first open/existing game that is either coordinated or joined by the email provided
		/// </summary>
		/// <param name="email">The players's email address</param>
		/// <returns>A dynamic Game, should one exist</returns>
		public List<BaggageItem> GetBaggageForFlight(string flightNumber)
        {
            try
            {
                {
                    var sql = $"SELECT * FROM c WHERE c.flightNumber = '{flightNumber}'";
                    var query = _client.CreateDocumentQuery<BaggageItem>(GetCollectionUri(), sql);

                    return query.ToList<BaggageItem>();
                }

                return null;
            }
            catch (Exception e)
            {
                var dce = e.GetBaseException() as DocumentClientException;

                if (dce != null && dce.StatusCode == HttpStatusCode.NotFound)
                {
                    EnsureDatabaseConfigured().Wait();
                    return null;
                }

                throw;
            }
        }

    }

}
