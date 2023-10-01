namespace SBE.Logic.Abstraction
{
    /// <summary>
    /// Represents an interface for encoding strings into byte arrays.
    /// Implementing classes must provide a method to encode a string into a byte array.
    /// </summary>
    public interface IEncoder
    {
        /// <summary>
        /// Encodes a string into a byte array.
        /// </summary>
        /// <param name="input">The string to encode.</param>
        /// <returns>A byte array representing the encoded data.</returns>
        public byte[] Encode(string input);
    }
}
