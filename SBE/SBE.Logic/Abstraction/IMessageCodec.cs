using SBE.Logic.Models;

namespace SBE.Logic.Abstraction
{
    public interface IMessageCodec
    {
        byte[] Encode(Message message);
        Message Decode(byte[] data);
    }
}
