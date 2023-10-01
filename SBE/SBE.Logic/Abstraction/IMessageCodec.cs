using SBE.Logic.Models;

namespace SBE.Logic.Abstraction
{
    /// <summary>
    /// Represents an interface for encoding and decoding messages.
    /// Implementing classes must provide methods to encode a message into a byte array
    /// and decode a byte array into a message.
    /// </summary>
    public interface IMessageCodec
    {
        /// <summary>
        /// Encodes a Message object into a byte array.
        /// </summary>
        /// <param name="message">The Message object to encode.</param>
        /// <returns>A byte array representing the encoded message.</returns>
        byte[] Encode(Message message);

        /// <summary>
        /// Decodes a byte array into a Message object.
        /// </summary>
        /// <param name="data">The byte array to decode into a Message.</param>
        /// <returns>A Message object representing the decoded data.</returns>
        Message Decode(byte[] data);
    }
}
