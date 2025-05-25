using APBD_Tutorial11.Application.DTOs;
using APBD_Tutorial11.Domain.Models;
using AutoMapper;

namespace APBD_Tutorial11.Application.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<PatientDto, Patient>()
            .ForMember(dest => dest.IdPatient, opt => opt.Ignore());
    }
}