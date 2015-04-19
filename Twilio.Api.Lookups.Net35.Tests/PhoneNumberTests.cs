﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Moq;
using NUnit.Framework;
using System.Threading;
using Twilio.Lookups;
using System.IO;
using Simple;
using Twilio.Api.Tests;
using System.Reflection;

namespace Twilio.Lookups.Tests
{
    [TestFixture]
    public class PhoneNumberTests
    {
        private string NUMBER = "+14158675309";
        private string NUMBER_LOCALIZED = "(415) 867-5309";

        ManualResetEvent manualResetEvent = null;

        private Mock<LookupsClient> mockClient;

        private string BASE_NAME = String.Empty;
        private Assembly asm;

        [SetUp]
        public void Setup()
        {
            mockClient = new Mock<LookupsClient>(Credentials.AccountSid, Credentials.AuthToken);
            mockClient.CallBase = true;

            asm = Assembly.GetExecutingAssembly();
            BASE_NAME = asm.GetName().Name + ".Resources.";
        }

        [Test]
        public void ShouldGetPhoneNumber()
        {
            RestRequest savedRequest = null;
            mockClient.Setup(trc => trc.Execute<Number>(It.IsAny<RestRequest>()))
                .Callback<RestRequest>((request) => savedRequest = request)
                .Returns(new Number());
            var client = mockClient.Object;

            client.GetPhoneNumber(NUMBER);

            mockClient.Verify(trc => trc.Execute<Number>(It.IsAny<RestRequest>()), Times.Once);
            Assert.IsNotNull(savedRequest);
            Assert.AreEqual("PhoneNumbers/{PhoneNumber}", savedRequest.Resource);
            Assert.AreEqual("GET", savedRequest.Method);
            Assert.AreEqual(1, savedRequest.Parameters.Count);
            var numberParam = savedRequest.Parameters.Find(x => x.Name == "PhoneNumber");
            Assert.AreEqual(NUMBER, numberParam.Value);
        }

        [Test]
        public void ShouldGetPhoneNumberAsynchronously()
        {

            RestRequest savedRequest = null;
            mockClient.Setup(trc => trc.ExecuteAsync<Number>(It.IsAny<RestRequest>(), It.IsAny<Action<Number>>()))
                .Callback<RestRequest, Action< Number>>((request, action) => savedRequest = request);
            var client = mockClient.Object;
            manualResetEvent = new ManualResetEvent(false);

            client.GetPhoneNumber(NUMBER, r =>
            {
                manualResetEvent.Set();
            });
            manualResetEvent.WaitOne(1);

            mockClient.Verify(trc => trc.ExecuteAsync<Number>(It.IsAny<RestRequest>(), It.IsAny<Action<Number>>()), Times.Once);
            
            Assert.IsNotNull(savedRequest);
            Assert.AreEqual("PhoneNumbers/{PhoneNumber}", savedRequest.Resource);
            Assert.AreEqual("GET", savedRequest.Method);
            Assert.AreEqual(1, savedRequest.Parameters.Count);
            var numberParam = savedRequest.Parameters.Find(x => x.Name == "PhoneNumber");
            Assert.AreEqual(NUMBER, numberParam.Value);
        }

        [Test]
        public void ShouldGetPhoneNumberWithCarrierInfo()
        {
            RestRequest savedRequest = null;
            mockClient.Setup(trc => trc.Execute<Number>(It.IsAny<RestRequest>()))
                .Callback<RestRequest>((request) => savedRequest = request)
                .Returns(new Number());
            var client = mockClient.Object;

            client.GetPhoneNumber(NUMBER, true);

            mockClient.Verify(trc => trc.Execute<Number>(It.IsAny<RestRequest>()), Times.Once);
            Assert.IsNotNull(savedRequest);
            Assert.AreEqual("PhoneNumbers/{PhoneNumber}", savedRequest.Resource);
            Assert.AreEqual("GET", savedRequest.Method);
            Assert.AreEqual(2, savedRequest.Parameters.Count);
            var numberParam = savedRequest.Parameters.Find(x => x.Name == "PhoneNumber");
            Assert.AreEqual(NUMBER, numberParam.Value);
            var typeParam = savedRequest.Parameters.Find(x => x.Name == "Type");
            Assert.AreEqual("carrier", typeParam.Value);
        }

        [Test]
        public void ShouldGetPhoneNumberWithCarrierInfoAsynchronously()
        {
            RestRequest savedRequest = null;
            mockClient.Setup(trc => trc.ExecuteAsync<Number>(It.IsAny<RestRequest>(), It.IsAny<Action<Number>>()))
                .Callback<RestRequest, Action<Number>>((request, action) => savedRequest = request);
            var client = mockClient.Object;
            manualResetEvent = new ManualResetEvent(false);

            client.GetPhoneNumber(NUMBER, true, r =>
            {
                manualResetEvent.Set();
            });
            manualResetEvent.WaitOne(1);

            mockClient.Verify(trc => trc.ExecuteAsync<Number>(It.IsAny<RestRequest>(), It.IsAny<Action<Number>>()), Times.Once);
            
            Assert.IsNotNull(savedRequest);
            Assert.AreEqual("PhoneNumbers/{PhoneNumber}", savedRequest.Resource);
            Assert.AreEqual("GET", savedRequest.Method);
            Assert.AreEqual(2, savedRequest.Parameters.Count);
            var numberParam = savedRequest.Parameters.Find(x => x.Name == "PhoneNumber");
            Assert.AreEqual(NUMBER, numberParam.Value);
            var typeParam = savedRequest.Parameters.Find(x => x.Name == "Type");
            Assert.AreEqual("carrier", typeParam.Value);
        }


        [Test]
        public void ShouldGetPhoneNumberWithCountryCode()
        {
            RestRequest savedRequest = null;
            mockClient.Setup(trc => trc.Execute<Number>(It.IsAny<RestRequest>()))
                .Callback<RestRequest>((request) => savedRequest = request)
                .Returns(new Number());
            var client = mockClient.Object;

            client.GetPhoneNumber(NUMBER_LOCALIZED, "US", true);

            mockClient.Verify(trc => trc.Execute<Number>(It.IsAny<RestRequest>()), Times.Once);
            Assert.IsNotNull(savedRequest);
            Assert.AreEqual("PhoneNumbers/{PhoneNumber}", savedRequest.Resource);
            Assert.AreEqual("GET", savedRequest.Method);
            Assert.AreEqual(3, savedRequest.Parameters.Count);
            var numberParam = savedRequest.Parameters.Find(x => x.Name == "PhoneNumber");
            Assert.AreEqual(NUMBER_LOCALIZED, numberParam.Value);
            var typeParam = savedRequest.Parameters.Find(x => x.Name == "Type");
            Assert.AreEqual("carrier", typeParam.Value);
            var countryCodeParam = savedRequest.Parameters.Find(x => x.Name == "CountryCode");
            Assert.AreEqual("US", countryCodeParam.Value);
        }

        [Test]
        public void ShouldGetPhoneNumberWithCountryCodeAsynchronously()
        {
            RestRequest savedRequest = null;
            mockClient.Setup(trc => trc.ExecuteAsync<Number>(It.IsAny<RestRequest>(), It.IsAny<Action<Number>>()))
                .Callback<RestRequest, Action<Number>>((request, action) => savedRequest = request);
            var client = mockClient.Object;
            manualResetEvent = new ManualResetEvent(false);

            client.GetPhoneNumber(NUMBER_LOCALIZED, "US", true, r =>
            {
                manualResetEvent.Set();
            });
            manualResetEvent.WaitOne(1);

            mockClient.Verify(trc => trc.ExecuteAsync<Number>(It.IsAny<RestRequest>(), It.IsAny<Action<Number>>()), Times.Once);
            
            Assert.IsNotNull(savedRequest);
            Assert.AreEqual("PhoneNumbers/{PhoneNumber}", savedRequest.Resource);
            Assert.AreEqual("GET", savedRequest.Method);
            Assert.AreEqual(3, savedRequest.Parameters.Count);
            var numberParam = savedRequest.Parameters.Find(x => x.Name == "PhoneNumber");
            Assert.AreEqual(NUMBER_LOCALIZED, numberParam.Value);
            var typeParam = savedRequest.Parameters.Find(x => x.Name == "Type");
            Assert.AreEqual("carrier", typeParam.Value);
            var countryCodeParam = savedRequest.Parameters.Find(x => x.Name == "CountryCode");
            Assert.AreEqual("US", countryCodeParam.Value);
        }

        [Test]
        public void testDeserializeResponse()
        {
            //var doc = File.ReadAllText(Path.Combine("Resources", "phone_number.json"));
            var doc = Twilio.Api.Tests.Utilities.UnPack(BASE_NAME + "phone_number.json");

            var json = new JsonDeserializer();
            var output = json.Deserialize<Number>(new RestResponse { Content = doc });

            Assert.NotNull(output);
            Assert.AreEqual("+15108675309", output.PhoneNumber);
            Assert.AreEqual("(510) 867-5309", output.NationalFormat);
            Assert.AreEqual("US", output.CountryCode);

            Assert.NotNull(output.Carrier);
            Assert.AreEqual("310", output.Carrier.MobileCountryCode);
            Assert.AreEqual("456", output.Carrier.MobileNetworkCode);
            Assert.AreEqual("mobile", output.Carrier.Type);
            Assert.AreEqual("verizon", output.Carrier.Name);
            Assert.IsNull(output.Carrier.ErrorCode);
        }
    }
}