using SBE.Logic.Services;

namespace SBE.UnitTests
{
    public class ASCIIEncoderUnitTest
    {
        private ASCIIEncoder _asciiEncoder;

        [SetUp]
        public void Setup()
        {
            _asciiEncoder = new ASCIIEncoder();
        }

        [TestCase("a", ExpectedResult = new byte[] { 97 })]
        [TestCase("ab", ExpectedResult = new byte[] { 97, 98 })]
        public byte[] Encode_ASCIIString_ShouldReturnAValidResult(string input)
        {
            var result = _asciiEncoder.Encode(input);
            return result;
        }

        [TestCase("ب")]
        public void Encode_NonASCIIString_ShouldThrowsAnException(string input)
        {
            Assert.Throws<ArgumentException>(() => _asciiEncoder.Encode(input));
        }
    }
}
