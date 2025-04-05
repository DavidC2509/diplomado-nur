using MediatR;
using Template.Services.Models;

namespace Template.Services.Query.ClientQuery
{
    public class GetClientQuery : IRequest<ClientModel>
    {
        public Guid Id { get; set; }

        public GetClientQuery(Guid id)
        {
            Id = id;
        }
    }
}