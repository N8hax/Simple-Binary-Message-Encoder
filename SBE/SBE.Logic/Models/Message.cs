namespace SBE.Logic.Models
{
    /// <summary>
    /// Represents a message structure consisting of headers and payload.
    /// </summary>
    public class Message
    {
        /// <summary>
        /// Gets or sets a dictionary of headers associated with the message.
        /// Headers are key-value pairs typically used for metadata.
        /// </summary>
        public Dictionary<string, string> Headers { get; set; } = new Dictionary<string, string>();

        /// <summary>
        /// Gets or sets the payload data of the message as a byte array.
        /// The payload contains the actual content of the message.
        /// </summary>
        public byte[] Payload { get; set; } = new byte[0];
    }
}
