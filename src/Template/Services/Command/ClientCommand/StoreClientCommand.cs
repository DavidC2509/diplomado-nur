using MediatR;

namespace Template.Services.Command.ClientCommand
{
    public class StoreClientCommand : IRequest<bool>
    {
        public required string Name { get; set; }
        public required string Phone { get; set; }
        public required string Email { get; set; }
    }
}