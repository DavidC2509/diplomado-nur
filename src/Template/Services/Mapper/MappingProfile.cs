using AutoMapper;
using Template.Domain.ClientAggregate;
using Template.Domain.MedicalConsultationAggregate;
using Template.Services.Models;

namespace Template.Services.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {

            CreateMap<Client, ClientModel>()
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email.Value));

            CreateMap<Address, AddresModel>();
            CreateMap<MedicalIllnesses, MedicaIllnessesModel>();
            CreateMap<Consultation, MedicalConsultModel>();

        }
    }
}