using SBE.Logic.Extensions;
using SBE.Logic.Models;
using SBE.Logic.Services;
using SBE.Logic.Validators;

var messageCodec = new MessageCodec(new ASCIIEncoder(), new ASCIIDecoder(), new MessageValidator());
var message = new Message()
{
    Headers = new Dictionary<string, string>()
    {
        {"Key1", "Value1" },
        {"Key2", "Value2" },
        {"Key3", "Value3" }
    },
    Payload = new byte[] { 1,2,3}
};
Console.ForegroundColor = ConsoleColor.Red;
Console.WriteLine("---Welcome to Simple Binary Message Encoder---");
Console.WriteLine("");
Console.ForegroundColor = ConsoleColor.Green;
Console.WriteLine($"This is the plain message: {message.ToReadableString()}");
Console.WriteLine("");
var encodedMessage = messageCodec.Encode(message);
Console.ForegroundColor = ConsoleColor.Blue;
Console.WriteLine($"This is the message after ecnoding: {encodedMessage.ToReadableString()}");
Console.WriteLine("");
var decodedMessage = messageCodec.Decode(encodedMessage);
Console.ForegroundColor = ConsoleColor.Green;
Console.WriteLine($"This is the message after denoding: {decodedMessage.ToReadableString()}");
Console.WriteLine("");
Console.ForegroundColor = ConsoleColor.Black;


