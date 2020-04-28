using RestSharp;
using TradeMeAPITests.DataEntities;
using System.Collections.Generic;
using Newtonsoft.Json;
using System;
using NUnit.Framework;
using Microsoft.Extensions.Configuration;
using AventStack.ExtentReports;
using System.IO;
using AventStack.ExtentReports.Reporter;
using NUnit.Framework.Interfaces;
using System.Runtime.InteropServices;

namespace TradeMeAPITests
{
    
    
    [TestFixture]
    public class UsedCars
    {
        protected AventStack.ExtentReports.ExtentReports _extent;
        protected ExtentTest _test;

        [OneTimeSetUp]
        public void BeforeClass()
        {
            try
            {
                //To create report directory and add HTML report into it
                _extent = new AventStack.ExtentReports.ExtentReports();
                if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                {
                    var dir = AppDomain.CurrentDomain.BaseDirectory.Replace("\\bin\\Debug\\netcoreapp3.1", "");
                    DirectoryInfo di = Directory.CreateDirectory(dir + "\\Test_Execution_Reports");
                    var htmlReporter = new ExtentHtmlReporter(dir + "\\Test_Execution_Reports" + "\\Automation_Report" + ".html");
                    _extent.AddSystemInfo("Environment", "TradeMe Sandbox API");
                    _extent.AddSystemInfo("User Name", "Evan Wood");
                    _extent.AttachReporter(htmlReporter);
                } else
                {
                    var dir = AppDomain.CurrentDomain.BaseDirectory.Replace("/bin/Debug/netcoreapp3.1", "");
                    DirectoryInfo di = Directory.CreateDirectory(dir + "/Test_Execution_Reports");
                    var htmlReporter = new ExtentHtmlReporter(dir + "/Test_Execution_Reports" + "/Automation_Report" + ".html");
                    _extent.AddSystemInfo("Environment", "TradeMe Sandbox API");
                    _extent.AddSystemInfo("User Name", "Evan Wood");
                    _extent.AttachReporter(htmlReporter);
                }
            }
            catch (Exception e)
            {
                throw (e);
            }
        }

        [SetUp]
        public void BeforeTest()
        {
            try
            {
                _test = _extent.CreateTest(TestContext.CurrentContext.Test.Name);
            }
            catch (Exception e)
            {
                throw (e);
            }
        }

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
            _test.Info("Total number of car makes is " + numMakes);

        }

        [Test]
        public void VerifyMakeExists()
        {
            var config = new ConfigurationBuilder().AddJsonFile("testConfig.json").Build();
            var _uri = config["URI"];
            var carBrand = config["existingBrand"];
            RestClient restClient = new RestClient(_uri);
            RestRequest restRequest = new RestRequest(Method.GET);
            IRestResponse restResponse = restClient.Execute(restRequest);
            string responseBody = restResponse.Content;
            CarsResponse carsResponse = JsonConvert.DeserializeObject<CarsResponse>(responseBody);
            List<Make> makes = carsResponse.Makes;
            bool brandExists = makes.Exists(x => x.MakeName == carBrand);
            Assert.True(brandExists);
            _test.Info($"{ carBrand } exists as a make");
        }

        [Test]
        public void GetCarCountForBrand()
        {
            var config = new ConfigurationBuilder().AddJsonFile("testConfig.json").Build();
            var _uri = config["URI"];
            var carBrand = config["existingBrand"];
            RestClient restClient = new RestClient(_uri);
            RestRequest restRequest = new RestRequest(Method.GET);
            IRestResponse restResponse = restClient.Execute(restRequest);
            string responseBody = restResponse.Content;
            CarsResponse carsResponse = JsonConvert.DeserializeObject<CarsResponse>(responseBody);
            List<Make> makes = carsResponse.Makes;
            var carInstance = makes.Find(x => x.MakeName.Equals(carBrand));
            int carCount = carInstance.Count;
            _test.Info($"Number of cars listed for { carBrand } is " + carCount);
        }

        [Test]
        public void VerifyMakeDoesntExist()
        {
            var config = new ConfigurationBuilder().AddJsonFile("testConfig.json").Build();
            var _uri = config["URI"];
            var carBrand = config["nonExistingBrand"];
            RestClient restClient = new RestClient(_uri);
            RestRequest restRequest = new RestRequest(Method.GET);
            IRestResponse restResponse = restClient.Execute(restRequest);
            string responseBody = restResponse.Content;
            CarsResponse carsResponse = JsonConvert.DeserializeObject<CarsResponse>(responseBody);
            List<Make> makes = carsResponse.Makes;
            bool brandExists = makes.Exists(x => x.MakeName == carBrand);
            Assert.False(brandExists);
            _test.Info($"{carBrand} make doesn't exist");

        }

        [TearDown]
        public void AfterTest()
        {
            try
            {
                var status = TestContext.CurrentContext.Result.Outcome.Status;
                var stacktrace = "" +TestContext.CurrentContext.Result.StackTrace + "";
                var errorMessage = TestContext.CurrentContext.Result.Message;
                Status logstatus;
                switch (status)
                {
                    case TestStatus.Failed:
                        logstatus = Status.Fail;
                        
                        _test.Log(logstatus, "Test ended with " +logstatus + " – " +errorMessage);

                        break;
                    case TestStatus.Skipped:
                        logstatus = Status.Skip;
                        _test.Log(logstatus, "Test ended with " +logstatus);
                        break;
                    default:
                        logstatus = Status.Pass;
                        break;
                }
            }
            catch (Exception e)
            {
                throw (e);
            }
        }

        [OneTimeTearDown]
        public void AfterClass()
        {
            try
            {
                _extent.Flush();
            }
            catch (Exception e)
            {
                throw (e);
            }
        }

    }

    
}
