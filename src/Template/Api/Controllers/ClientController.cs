using ControllerCqrs;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Template.Services.Command.ClientCommand;
using Template.Services.Models;
using Template.Services.Query.ClientQuery;

namespace Template.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/client")]
    public class ClientController : ServiceBaseController
    {
        public ClientController(IMediator mediator) : base(mediator) { }

        ///<summary>
        ///Listado Clientes
        ///</summary>
        [HttpGet("list/test/deploy")]
        public Task<ActionResult<IEnumerable<ClientModel>>> ListClient() => SendRequest(new ListClientQuery());

        ///<summary>
        ///Obtener Cliente
        ///</summary>
        [HttpGet("{id}")]
        public Task<ActionResult<ClientModel>> GetClient(Guid id) => SendRequest(new GetClientQuery(id));


        ///<summary>
        ///Crear Cliente
        ///</summary>
        [HttpPost()]
        public Task<ActionResult<bool>> StoreAccout([FromBody] StoreClientCommand command) => SendRequest(command);

        ///<summary>
        ///Agregar Direccion
        ///</summary>
        [HttpPost("addres")]
        public Task<ActionResult<bool>> AddAddresClient([FromBody] AddAddresByClientCommand command) => SendRequest(command);

        ///<summary>
        ///Agregar Enfermedad
        ///</summary>
        [HttpPost("medical-illneses")]
        public Task<ActionResult<bool>> AddMedicalIllneses([FromBody] AddMedicalIllnessesCommand command) => SendRequest(command);
    }
}