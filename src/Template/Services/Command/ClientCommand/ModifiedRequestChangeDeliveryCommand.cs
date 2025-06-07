using MediatR;

namespace Template.Services.Command.ClientCommand
{
    public class ModifiedRequestChangeDeliveryCommand : IRequest<bool>
    {
        public required Guid IdClient { get; set; }
        public required Guid AddresGuid { get; set; }
        public DateTime DateDelivery { get; set; }
    }
}