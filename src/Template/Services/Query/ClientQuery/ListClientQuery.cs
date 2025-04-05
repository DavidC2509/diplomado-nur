using MediatR;
using Template.Services.Models;

namespace Template.Services.Query.ClientQuery
{
    public class ListClientQuery : IRequest<IEnumerable<ClientModel>>
    {
    }
}