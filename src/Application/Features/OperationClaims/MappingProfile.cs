using Application.Features.OperationClaims.Command.Create;
using Application.Features.OperationClaims.Command.Delete;
using Application.Features.OperationClaims.Command.Update;
using Application.Features.OperationClaims.Queries.GetAll;
using Application.Features.OperationClaims.Queries.GetById;
using AutoMapper;
using Domain.Entities;

namespace Application.Features.OperationClaims
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<OperationClaim, OperationClaimGetAllQueryResponse>().ReverseMap();

            CreateMap<OperationClaim, OperationClaimGetByIdQueryResponse>().ReverseMap();

            CreateMap<OperationClaim, CreateOperationClaimsCommandRequest>().ReverseMap();
            CreateMap<OperationClaim, CreateOperationClaimsCommandResponse>().ReverseMap();

            CreateMap<OperationClaim, UpdateOperationClaimCommandRequest>().ReverseMap();
            CreateMap<OperationClaim, UpdateOperationClaimCommandResponse>().ReverseMap();

            CreateMap<OperationClaim, DeleteOperationClaimCommandResponse>().ReverseMap();
        }
    }
}
