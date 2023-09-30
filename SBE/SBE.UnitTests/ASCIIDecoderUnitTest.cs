using SBE.Logic.Services;

namespace SBE.UnitTests
{
    public class ASCIIDecoderUnitTest
    {
        private ASCIIDecoder _asciiDecoder;

        [SetUp]
        public void Setup()
        {
            _asciiDecoder = new ASCIIDecoder();
        }

        [TestCase(new byte[] { 97 }, ExpectedResult = "a" )]
        [TestCase(new byte[] { 97, 98 }, ExpectedResult = "ab" )]
        public string Decode_ASCIIBytes_ShouldReturnAValidResult(byte[] input)
        {
            var result = _asciiDecoder.Decode(input);
            return result;
        }

        [TestCase(new byte[] { 128 })]
        public void Decode_NonASCIIBytes_ShouldThrowsAnException(byte[] input)
        {
            Assert.Throws<ArgumentException>(() => _asciiDecoder.Decode(input));
        }
    }
}
