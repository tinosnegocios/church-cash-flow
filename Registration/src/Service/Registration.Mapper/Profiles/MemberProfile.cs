﻿using AutoMapper;
using Registration.DomainBase.Entities.Registrations;
using Registration.Mapper.DTOs.Registration.Member;

namespace ChurchCashFlow.Profiles;
public class MemberProfile : Profile
{
	public MemberProfile()
	{
		CreateMap<Member, ReadMemberDto>()
			.ForMember(dto => dto.Church, map =>
			map.MapFrom(member => member.Church.Name));
		CreateMap<EditMemberDto, Member>();
	}
}