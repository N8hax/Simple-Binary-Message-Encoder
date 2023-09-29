using FluentValidation;
using SBE.Logic.Models;

namespace SBE.Logic.Validators
{
    public class MessageValidator : AbstractValidator<Message>
    {
        public MessageValidator()
        {
            RuleFor(m => m.Payload.Length).LessThanOrEqualTo(AppSettings.MESSAGE_PAYLOAD_MAX_SIZE_IN_BYTES);
            RuleFor(m => m.Headers.Count).LessThanOrEqualTo(AppSettings.MESSAGE_HEADERS_MAX_COUNT);
            RuleForEach(m => m.Headers).Must((message, header, context) =>
            {
                if (header.Key.Length * sizeof(Char) > AppSettings.MESSAGE_HEADER_NAME_MAX_SIZE_IN_BYTES)
                {
                    context.AddFailure($"The size of header name should not exceed {AppSettings.MESSAGE_HEADER_NAME_MAX_SIZE_IN_BYTES} bytes. Header Name: {header.Key}");
                    return false;
                }
                if (header.Value.Length * sizeof(Char) > AppSettings.MESSAGE_HEADER_VALUE_MAX_SIZE_IN_BYTES)
                {
                    context.AddFailure($"The size of header value should not exceed {AppSettings.MESSAGE_HEADER_VALUE_MAX_SIZE_IN_BYTES} bytes. Header Value: {header.Value}");
                    return false;
                }
                return true; // Validation succeded
            });
        }
    }
}
