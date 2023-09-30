using FluentValidation;
using SBE.Logic.Abstraction;
using SBE.Logic.Models;
using SBE.Logic.Validators;

namespace SBE.Logic.Services
{
    public class MessageCodec : IMessageCodec
    {
        private IEncoder _encoder { get; set; }
        private IDecoder _decoder { get; set; }
        private MessageValidator _messageValidator { get; set; }
        public MessageCodec(IEncoder encoder, IDecoder decoder, MessageValidator messageValidator)
        {
            _encoder = encoder;
            _decoder = decoder;
            _messageValidator = messageValidator;
        }
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
