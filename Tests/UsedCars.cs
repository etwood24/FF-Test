using RestSharp;
using TradeMeAPITests.DataEntities;
using System.Collections.Generic;
using Newtonsoft.Json;
using System;
using NUnit.Framework;
using System.Configuration;
using Microsoft.Extensions.Configuration;

namespace TradeMeAPITests
{
    
    
    [TestFixture]
    public class UsedCars
    {

        [Test]
        public void GetMakeCount()
        {
            var config = new ConfigurationBuilder().AddJsonFile("testConfig.json").Build();
            var _uri = config["URI"];
            RestClient restClient = new RestClient(_uri);
            RestRequest restRequest = new RestRequest(Method.GET);
            IRestResponse restResponse = restClient.Execute(restRequest);
            string responseBody = restResponse.Content;
            CarsResponse carsResponse = JsonConvert.DeserializeObject<CarsResponse>(responseBody);
            int numMakes = carsResponse.Makes.Count;
            Console.WriteLine(numMakes);
        }

        [Test]
        public void VerifyMakeExists()
        {
            var config = new ConfigurationBuilder().AddJsonFile("testConfig.json").Build();
            var _uri = config["URI"];
            RestClient restClient = new RestClient(_uri);
            RestRequest restRequest = new RestRequest(Method.GET);
            IRestResponse restResponse = restClient.Execute(restRequest);
            string responseBody = restResponse.Content;
            CarsResponse carsResponse = JsonConvert.DeserializeObject<CarsResponse>(responseBody);
            List<Make> makes = carsResponse.Makes;
            bool brandExists = makes.Exists(x => x.MakeName == "Kia");
            Assert.True(brandExists);
        }

        [Test]
        public void GetCarCountForBrand()
        {
            var config = new ConfigurationBuilder().AddJsonFile("testConfig.json").Build();
            var _uri = config["URI"];
            RestClient restClient = new RestClient(_uri);
            RestRequest restRequest = new RestRequest(Method.GET);
            IRestResponse restResponse = restClient.Execute(restRequest);
            string responseBody = restResponse.Content;
            CarsResponse carsResponse = JsonConvert.DeserializeObject<CarsResponse>(responseBody);
            List<Make> makes = carsResponse.Makes;

            var carInstance = makes.Find(x => x.MakeName.Equals("Kia"));
            int carCount = carInstance.Count;
            Console.WriteLine(carCount);
        }

        [Test]
        public void VerifyMakeDoesntExist()
        {
            var config = new ConfigurationBuilder().AddJsonFile("testConfig.json").Build();
            var _uri = config["URI"];
            RestClient restClient = new RestClient(_uri);
            RestRequest restRequest = new RestRequest(Method.GET);
            IRestResponse restResponse = restClient.Execute(restRequest);
            string responseBody = restResponse.Content;
            CarsResponse carsResponse = JsonConvert.DeserializeObject<CarsResponse>(responseBody);
            List<Make> makes = carsResponse.Makes;
            bool brandExists = makes.Exists(x => x.MakeName == "Hispano Suiza");
            Assert.False(brandExists);

        }

    }
    
}
