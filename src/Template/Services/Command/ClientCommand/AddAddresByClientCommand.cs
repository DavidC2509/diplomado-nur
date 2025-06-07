using MediatR;

namespace Template.Services.Command.ClientCommand
{
    public class AddAddresByClientCommand : IRequest<bool>
    {
        public required Guid IdClient { get; set; }
        public required string Street { get; set; }
        public required string City { get; set; }
        public required decimal Latituded { get; set; }
        public required decimal Longitud { get; set; }
        public DateTime DateDelivery { get; set; }
    }
}