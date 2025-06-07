using ControllerCqrs;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Template.Services.Command.MedicalConsultCommand;
using Template.Services.Models;
using Template.Services.Query.MedicalConsultationQuery;

namespace Template.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/medical-consultation")]
    public class MedicalConsultationController : ServiceBaseController
    {
        public MedicalConsultationController(IMediator mediator) : base(mediator) { }

        ///<summary>
        ///Obtener una consulta medica
        ///</summary>
        [HttpGet("{id}")]
        public Task<ActionResult<MedicalConsultModel>> GetMedialConsultation(Guid id) => SendRequest(new GetMedicalConsultationByIdQuery(id));

        ///<summary>
        ///Obtener consulta medica por cliente
        ///</summary>
        [HttpGet("client/{idClient}")]
        public Task<ActionResult<IEnumerable<MedicalConsultModel>>> GetListMedialConsultationByClient(Guid idClient) => SendRequest(new ListMedicalConsultationByClientQuery(idClient));


        ///<summary>
        ///Crear consulta medica
        ///</summary>
        [HttpPost()]
        public Task<ActionResult<bool>> StoreConsultMedical([FromBody] StoreMedicalConsultCommand command) => SendRequest(command);

    }
}