using NUnit.Framework;
using Input;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InputReaderTests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
           InputReader.SessionKey = "your key";
        }

        [Test]
        public async Task DataNotEmpty()
        {
            var data = await InputReader.GetInput(1);
            Assert.IsFalse(string.IsNullOrEmpty(data));
        }
    }
}