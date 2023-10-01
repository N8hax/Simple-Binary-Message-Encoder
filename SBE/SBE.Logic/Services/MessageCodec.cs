using FluentValidation;
using SBE.Logic.Abstraction;
using SBE.Logic.Models;
using SBE.Logic.Validators;

namespace SBE.Logic.Services
{
    /// <summary>
    /// Represents a message codec that encodes and decodes messages using an encoder, decoder, and message validator.
    /// </summary>
    public class MessageCodec : IMessageCodec
    {
        private IEncoder _encoder { get; set; }
        private IDecoder _decoder { get; set; }
        private MessageValidator _messageValidator { get; set; }

        /// <summary>
        /// Initializes a new instance of the MessageCodec class with the provided encoder, decoder, and message validator.
        /// </summary>
        /// <param name="encoder">The encoder used to encode messages.</param>
        /// <param name="decoder">The decoder used to decode messages.</param>
        /// <param name="messageValidator">The message validator used to validate messages.</param>
        public MessageCodec(IEncoder encoder, IDecoder decoder, MessageValidator messageValidator)
        {
            _encoder = encoder;
            _decoder = decoder;
            _messageValidator = messageValidator;
        }

        /// <summary>
        /// Encodes a Message object into a byte array.
        /// </summary>
        /// <param name="message">The Message object to encode.</param>
        /// <returns>A byte array representing the encoded message.</returns>
        /// <exception cref="ValidationException">Thrown if the message fails validation.</exception>
        public byte[] Encode(Message message)
        {
            // Validating the message
            _messageValidator.ValidateAndThrow(message);
            
            using (MemoryStream stream = new MemoryStream())
            using (BinaryWriter writer = new BinaryWriter(stream))
            {
                // Encode the number of headers
                byte headerCount = (byte)Math.Min(message.Headers.Count, AppSettings.MESSAGE_HEADERS_MAX_COUNT);
                writer.Write(headerCount);

                // Encode each header
                foreach (var header in message.Headers.Take(headerCount))
                {
                    var headerKeyEncoded = _encoder.Encode(header.Key);
                    writer.Write((byte)headerKeyEncoded.Length);
                    writer.Write(headerKeyEncoded);
                    var headerValueEncoded = _encoder.Encode(header.Value);
                    writer.Write((byte)headerValueEncoded.Length);
                    writer.Write(headerValueEncoded);
                }

                // Encode the payload length
                writer.Write(message.Payload.Length);

                // Encode the payload
                writer.Write(message.Payload);

                return stream.ToArray();
            }
        }

        /// <summary>
        /// Decodes a byte array into a Message object.
        /// </summary>
        /// <param name="input">The byte array to decode into a Message.</param>
        /// <returns>A Message object representing the decoded data.</returns>
        public Message Decode(byte[] input)
        {
            using (MemoryStream stream = new MemoryStream(input))
            using (BinaryReader reader = new BinaryReader(stream))
            {
                Message message = new Message();

                // Decode the number of headers
                byte headerCount = reader.ReadByte();
                var length = new byte();
                // Decode each header
                for (int i = 0; i < headerCount; i++)
                {
                    length = reader.ReadByte();
                    byte[] data = reader.ReadBytes(length);
                    string key = _decoder.Decode(data);
                    length = reader.ReadByte();
                    data = reader.ReadBytes(length);
                    string value = _decoder.Decode(data);
                    message.Headers.Add(key, value);
                }

                // Decode the payload length
                var payloadLength = reader.ReadInt32();

                // Decode the payload
                message.Payload = reader.ReadBytes(payloadLength);

                return message;
            }
        }
    }
}
