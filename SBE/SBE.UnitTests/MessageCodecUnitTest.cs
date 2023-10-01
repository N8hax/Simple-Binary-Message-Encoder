using FluentValidation.Results;
using Moq;
using Newtonsoft.Json;
using SBE.Logic.Abstraction;
using SBE.Logic.Models;
using SBE.Logic.Services;
using SBE.Logic.Validators;

namespace SBE.UnitTests
{
    public class MessageCodecUnitTest
    {
        private MessageCodec _messageCodec;
        private IEncoder _encoder;
        private IDecoder _decoder;
        private MessageValidator _messageValidator;

        [Test]
        public void Encode_UsingASCIIEncoderWithValidMessage_ShouldReturnASCIIEncodedMessage()
        {
            // Arrange
            var message = new Message()
            {
                Headers = new Dictionary<string, string>()
                {
                    {"a","b"}
                },
                Payload = new byte[] {2}
            };
            _encoder = new ASCIIEncoder();
            var _decoderMock = new Mock<IDecoder>();
            _messageValidator = new MessageValidator();
            _messageCodec = new MessageCodec(_encoder, _decoderMock.Object, _messageValidator);

            // Act
            var result = _messageCodec.Encode(message);

            // Assert
            CollectionAssert.AreEqual(result, new byte[] {1,1,97,1,98,1,0,0,0,2} );
        }

        [Test]
        public void Decode_UsingASCIIDecoderWithValidMessage_ShouldReturnASCIIDecodedMessage()
        {
            // Arrange
            var encodedMessage = new byte[] { 1, 1, 97, 1, 98, 1, 0, 0, 0, 2 };
            var _encoderMock = new Mock<IEncoder>();
            _decoder = new ASCIIDecoder();
            _messageValidator = new MessageValidator();
            _messageCodec = new MessageCodec(_encoderMock.Object, _decoder, _messageValidator);
            var expectedResult = new Message()
            {
                Headers = new Dictionary<string, string>()
                {
                    {"a","b"}
                },
                Payload = new byte[] { 2 }
            };

            // Act
            var result = _messageCodec.Decode(encodedMessage);

            // Assert
            Assert.That(JsonConvert.SerializeObject(expectedResult), Is.EqualTo(JsonConvert.SerializeObject(result)));
        }
    }
}
