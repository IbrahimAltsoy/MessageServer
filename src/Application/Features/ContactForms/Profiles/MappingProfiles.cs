using Application.Features.ContactForms.Commands.Create;
using Application.Features.ContactForms.Commands.Delete;
using Application.Features.ContactForms.Queries.GetList;
using AutoMapper;
using Core.Application.Responses;
using Core.Persistence.Paging;
using Domain.Entities;

namespace Application.Features.ContactForms.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<ContactForm, CreateContactFormCommand>().ReverseMap();
        CreateMap<ContactForm, DeleteContactFormCommand>().ReverseMap();
        CreateMap<ContactForm, GetListContactFormListItemDto>().ReverseMap();
        CreateMap<IPaginate<ContactForm>, GetListResponse<GetListContactFormListItemDto>>().ReverseMap();
    }
}
