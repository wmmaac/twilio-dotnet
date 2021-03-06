﻿using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twilio.Lookups;

namespace Twilio.Api.TaskRouter.Pcl.Tests
{
    [TestFixture]
    public class CoreTests
    {
        [Test]
        public void ShouldSetProperBaseUrl()
        {
            var client = new LookupsClient("XXXXXX", "XXXXXXX");

            Assert.AreEqual("https://lookups.twilio.com/", client.BaseUrl);
        }

        [Test]
        public void ShouldSetProperApiVersion()
        {
            var client = new LookupsClient("XXXXXX", "XXXXXXX");

            Assert.AreEqual("v1", client.ApiVersion);

        }

    }
}
