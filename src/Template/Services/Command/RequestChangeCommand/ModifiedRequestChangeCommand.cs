using MediatR;

namespace Template.Services.Command.RequestChangeCommand
{
    public class ModifiedRequestChangeCommand : IRequest<bool>
    {
        public Guid IdAppointment { get; set; }
        public Guid IdClient { get; set; }

        public DateTime PreviusDate { get; set; }
        public DateTime NewDate { get; set; }
    }
}