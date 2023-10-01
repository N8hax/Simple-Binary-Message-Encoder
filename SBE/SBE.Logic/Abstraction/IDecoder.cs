namespace SBE.Logic.Abstraction
{
    /// <summary>
    /// Represents an interface for decoding byte arrays into strings.
    /// Implementing classes must provide a method to decode a byte array into a string.
    /// </summary>
    public interface IDecoder
    {
        /// <summary>
        /// Decodes a byte array into a string.
        /// </summary>
        /// <param name="input">The byte array to decode.</param>
        /// <returns>A string representing the decoded data.</returns>
        public string Decode(byte[] input);
    }
}
