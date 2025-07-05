using MediatR;

namespace Template.Services.Command.ClientCommand
{
    public class AddAddresByClientCommand : IRequest<bool>
    {
        public Guid IdClient { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public decimal Latituded { get; set; }
        public decimal Longitud { get; set; }
        public DateTime DateDelivery { get; set; }

        public AddAddresByClientCommand()
        {
            Street = string.Empty;
            City = string.Empty;
        }


        public void SetClientGuid(Guid accountId)
        {
            IdClient = accountId;
        }
    }
}