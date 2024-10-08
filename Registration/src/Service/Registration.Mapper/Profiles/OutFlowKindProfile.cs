﻿using AutoMapper;
using Registration.DomainBase.Entities.Registrations;
using Registration.Mapper.DTOs.Registration.OutFlowKind;

namespace Registration.Mapper.Profiles;
public class OutFlowKindProfile : Profile
{
    public OutFlowKindProfile()
    {
        CreateMap<OutFlowKind, ReadOutFlowKindDto>();
        CreateMap<EditOutFlowKindDto, OutFlowKind>();
    }
}