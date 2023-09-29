using SBE.Logic;
using SBE.Logic.Models;
using SBE.Logic.Validators;

namespace SBE.UnitTests
{
    public class MessageValidatorUnitTest
    {
        private MessageValidator _validator;


        [SetUp]
        public void Setup()
        {
            _validator = new MessageValidator();
        }

        #region Message Payload validations unit tests
        [Test]
        public async Task MessagePayloadLength_ExceedMaxLength_ShouldRaiseError()
        {
            // Arrange
            var message = new Message()
            {
                Payload = new byte[AppSettings.MESSAGE_PAYLOAD_MAX_SIZE_IN_BYTES + 1]
            };

            // Act
            var result = await _validator.ValidateAsync(message);

            // Assert
            Assert.That(result.Errors.Any(o => o.PropertyName == "Payload"));
        }

        [Test]
        public async Task MessagePayload_ValidAndLengthIsLessThanMaxLength_ShouldBeValid()
        {
            // Arrange
            var message = new Message()
            {
                Payload = new byte[AppSettings.MESSAGE_PAYLOAD_MAX_SIZE_IN_BYTES - 1]
            };

            // Act
            var result = await _validator.ValidateAsync(message);

            // Assert
            Assert.That(result.IsValid);
        }

        [Test]
        public async Task MessagePayload_ValidAndLengthIsEqualsMaxLength_ShouldBeValid()
        {
            // Arrange
            var message = new Message()
            {
                Payload = new byte[AppSettings.MESSAGE_PAYLOAD_MAX_SIZE_IN_BYTES]
            };

            // Act
            var result = await _validator.ValidateAsync(message);

            // Assert
            Assert.That(result.IsValid);
        }

        [Test]
        public async Task MessagePayload_IsEmpty_ShouldBeValid()
        {
            // Arrange
            var message = new Message()
            {
                Payload = new byte[0]
            };

            // Act
            var result = await _validator.ValidateAsync(message);

            // Assert
            Assert.That(result.IsValid);
        }

        #endregion


        #region Message Headers validations unit tests
        [Test]
        public async Task MessageHeadersCount_ExceedMaxCount_ShouldRaiseError()
        {
            // Arrange
            var messageHeaders = new Dictionary<string, string>();
            for (int i = 1; i <= AppSettings.MESSAGE_HEADERS_MAX_COUNT + 1; i++)
            {
                messageHeaders.Add(i.ToString(), i.ToString());
            }
            var message = new Message()
            {
                Headers = messageHeaders
            };

            // Act
            var result = await _validator.ValidateAsync(message);

            // Assert
            Assert.That(result.Errors.Any(o => o.PropertyName == "Headers.Count"));
        }

        [Test]
        public async Task MessageHeaders_ValidAndCountIsLessThanMaxCount_ShouldBeValid()
        {
            // Arrange
            var messageHeaders = new Dictionary<string, string>();
            for (int i = 1; i <= AppSettings.MESSAGE_HEADERS_MAX_COUNT - 1; i++)
            {
                messageHeaders.Add(i.ToString(), i.ToString());
            }
            var message = new Message()
            {
                Headers = messageHeaders
            };

            // Act
            var result = await _validator.ValidateAsync(message);

            // Assert
            Assert.That(result.IsValid);
        }

        [Test]
        public async Task MessageHeaders_ValidAndCountEqualsMaxCount_ShouldBeValid()
        {
            // Arrange
            var messageHeaders = new Dictionary<string, string>();
            for (int i = 1; i <= AppSettings.MESSAGE_HEADERS_MAX_COUNT; i++)
            {
                messageHeaders.Add(i.ToString(), i.ToString());
            }
            var message = new Message()
            {
                Headers = messageHeaders
            };

            // Act
            var result = await _validator.ValidateAsync(message);

            // Assert
            Assert.That(result.IsValid);
        }

        [Test]
        public async Task MessageHeaders_IsEmpty_ShouldBeValid()
        {
            // Arrange
            var messageHeaders = new Dictionary<string, string>();
            for (int i = 1; i <= AppSettings.MESSAGE_HEADERS_MAX_COUNT; i++)
            {
                messageHeaders.Add(i.ToString(), i.ToString());
            }
            var message = new Message()
            {
                Headers = messageHeaders
            };

            // Act
            var result = await _validator.ValidateAsync(message);

            // Assert
            Assert.That(result.IsValid);
        }

        [Test]
        public async Task MessageHeaderNameLength_IsZero_ShouldBeValid()
        {
            // Arrange
            var messageHeaders = new Dictionary<string, string>()
            {
                {"", "1" }
            };
            var message = new Message()
            {
                Headers = messageHeaders
            };

            // Act
            var result = await _validator.ValidateAsync(message);

            // Assert
            Assert.That(result.IsValid);
        }

        [Test]
        public async Task MessageHeaderNameSize_ExceedMaxSize_ShouldRaiseError()
        {
            // Arrange
            // creating a Header Name string
            string repeatedSubstring = "1 ";
            int desiredLength = AppSettings.MESSAGE_HEADER_NAME_MAX_SIZE_IN_BYTES / sizeof(Char) + 1;
            int repeatCount = (int)Math.Ceiling((double)desiredLength / (double)repeatedSubstring.Length);
            string[] repeatedSubstrings = new string[repeatCount];
            for (int i = 0; i < repeatCount; i++)
            {
                repeatedSubstrings[i] = repeatedSubstring;
            }
            string headerName = string.Join("", repeatedSubstrings);
            headerName = headerName.Substring(0, desiredLength);
            var messageHeaders = new Dictionary<string, string>()
            {
                {headerName, "1" }
            };
            var message = new Message()
            {
                Headers = messageHeaders
            };

            // Act
            var result = await _validator.ValidateAsync(message);

            // Assert
            Assert.That(result.Errors.Any(o => o.PropertyName == "Headers[0]"));
        }

        [Test]
        public async Task MessageHeaderValueLength_IsZero_ShouldBeValid()
        {
            // Arrange
            var messageHeaders = new Dictionary<string, string>()
            {
                {"1", "" }
            };
            var message = new Message()
            {
                Headers = messageHeaders
            };

            // Act
            var result = await _validator.ValidateAsync(message);

            // Assert
            Assert.That(result.IsValid);
        }

        [Test]
        public async Task MessageHeaderValueSize_ExceedMaxSize_ShouldRaiseError()
        {
            // Arrange
            // creating a Header Value string
            string repeatedSubstring = "1 ";
            int desiredLength = AppSettings.MESSAGE_HEADER_VALUE_MAX_SIZE_IN_BYTES / sizeof(Char) + 1;
            int repeatCount = (int)Math.Ceiling((double)desiredLength / (double)repeatedSubstring.Length);
            string[] repeatedSubstrings = new string[repeatCount];
            for (int i = 0; i < repeatCount; i++)
            {
                repeatedSubstrings[i] = repeatedSubstring;
            }
            string headerValue = string.Join("", repeatedSubstrings);
            headerValue = headerValue.Substring(0, desiredLength);
            var messageHeaders = new Dictionary<string, string>()
            {
                {"1", headerValue }
            };
            var message = new Message()
            {
                Headers = messageHeaders
            };

            // Act
            var result = await _validator.ValidateAsync(message);

            // Assert
            Assert.That(result.Errors.Any(o => o.PropertyName == "Headers[0]"));
        }

        #endregion
    }
}
