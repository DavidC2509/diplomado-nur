using MediatR;

namespace Template.Services.Command.ClientCommand
{
    public class ModifiedRequestChangeDeliveryBlockCommand : IRequest<bool>
    {
        public required Guid IdClient { get; set; }
        public required Guid AddresGuid { get; set; }
        public required DateTime ToDate { get; set; }
        public required DateTime FromDate { get; set; }

    }
}