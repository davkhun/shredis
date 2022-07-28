using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using shredis.Infrastucture;
using System;
using System.Collections.Generic;

namespace shredisTest
{

    [TestClass]
    public class ShredisCacheTests
    {
        Dictionary<string, string> testConfiguration = new Dictionary<string, string>
        {
            {"CacheSizeLimit", "10" }
        };

        [TestMethod]
        public void KeyNotExistsSuccess()
        {
            var configuration = new ConfigurationBuilder().AddInMemoryCollection(testConfiguration).Build();
            var cache = new ShredisCache(configuration);
            var notExists = cache.KeyExists("sampleKey");
            Assert.IsFalse(notExists);
        }

        [TestMethod]
        public void KeyNotExistsFail()
        {
            try
            {
                var configuration = new ConfigurationBuilder().AddInMemoryCollection(testConfiguration).Build();
                var cache = new ShredisCache(configuration);
                var notExists = cache.KeyExists("sampleKey");
                Assert.Fail();
            }
            catch 
            {
                Assert.IsTrue(true);
            }
        }

        [TestMethod]
        public void SetValueSuccess()
        {
            var configuration = new ConfigurationBuilder().AddInMemoryCollection(testConfiguration).Build();
            var cache = new ShredisCache(configuration);
            cache.SetValue(new shredis.Models.Request
            {
                Key = "sampleKey",
                Value = "value"
            });
            Assert.IsTrue(true);
        }

        [TestMethod]
        public void KeyExitstsSuccess()
        {
            var configuration = new ConfigurationBuilder().AddInMemoryCollection(testConfiguration).Build();
            var cache = new ShredisCache(configuration);
            var notExists = cache.KeyExists("sampleKey");
            Assert.IsTrue(notExists);
        }
    }
}