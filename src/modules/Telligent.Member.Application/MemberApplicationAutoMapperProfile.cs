using Telligent.Member.Application.Dtos.Account;
using Telligent.Member.Application.Dtos.Channels;
using Telligent.Member.Application.Dtos.ChannelSetting;
using Telligent.Member.Application.Dtos.ChannelMapping;
using Telligent.Member.Application.Dtos.CompanyMapping;
using Telligent.Member.Application.Dtos.Config;
using Telligent.Member.Application.Dtos.Member;
using Telligent.Member.Application.Dtos.MemberMapping;
using Telligent.Member.Application.Dtos.Membership;
using Telligent.Member.Application.Dtos.Organization;
using Telligent.Member.Application.Dtos.Profile;
using Telligent.Member.Application.Dtos.Prospect;
using Telligent.Member.Application.Dtos.ProspectMapping;
using Telligent.Member.Application.Dtos.User;
using Telligent.Member.Domain.Account;
using Telligent.Member.Domain.Channels;
using Telligent.Member.Domain.Configs;
using Telligent.Member.Domain.Members;
using Telligent.Member.Domain.Organizations;
using Telligent.Member.Domain.Profiles;
using Telligent.Member.Domain.Prospect;
using Telligent.Member.Domain.Users;
using Profile = AutoMapper.Profile;

namespace Telligent.Member.Application;

public class MemberApplicationAutoMapperProfile : Profile
{
    public MemberApplicationAutoMapperProfile()
    {
        ShouldMapProperty = prop =>
            prop.GetMethod is not null && (prop.GetMethod.IsAssembly || prop.GetMethod.IsPublic);

        CreateMap<Company, CompanyDto>();
        CreateMap<CompanyDto, Company>();
        CreateMap<Company, GraphQL.Type.Company>();
        CreateProjection<Company, GraphQL.Type.Company>();

        CreateMap<Domain.Profiles.Profile, ProfileDto>();
        CreateMap<ProfileDto, Domain.Profiles.Profile>();

        CreateMap<CustomProfile, CustomProfileDto>();
        CreateMap<CustomProfileDto, CustomProfile>();

        CreateMap<CustomProfileItem, CustomProfileItemDto>();
        CreateMap<CustomProfileItemDto, CustomProfileItem>();

        CreateMap<ChannelSetting, ChannelSettingDto>();
        CreateMap<ChannelSettingDto, ChannelSetting>();

        CreateMap<Domain.Members.Member, MemberDto>();
        CreateMap<MemberDto, Domain.Members.Member>();
        CreateMap<CreateMemberDto, Domain.Members.Member>();
        CreateMap<Domain.Members.Member, GraphQL.Type.Member>();

        CreateMap<Membership, MembershipDto>();
        CreateMap<CreateMembershipDto, Membership>();
        CreateMap<UpdateMembershipDto, Membership>();

        CreateMap<Account, AccountDto>();
        CreateMap<AccountDto, Account>();
        CreateMap<CreateAccountDto, Account>();
        CreateMap<UpdateAccountDto, Account>();
        CreateMap<Account, GraphQL.Type.Account>();

        CreateMap<Prospect, ProspectDto>();
        CreateMap<ProspectDto, Prospect>();
        CreateMap<CreateProspectDto, Prospect>();
        CreateMap<UpdateProspectDto, Prospect>();

        CreateMap<ThirdPartyLoginConfig, ThirdPartyLoginConfigDto>();
        CreateMap<ThirdPartyLoginConfigDto, ThirdPartyLoginConfig>();

        CreateMap<Channel, ChannelDto>();
        CreateMap<ChannelDto, Channel>();

        CreateMap<ChannelMapping, ChannelMappingDto>();
        CreateMap<ChannelMappingDto, ChannelMapping>();

        CreateMap<CompanyMapping, CompanyMappingDto>();
        CreateMap<CompanyMappingDto, CompanyMapping>();

        CreateMap<MemberMapping, MemberMappingDto>();
        CreateMap<MemberMappingDto, MemberMapping>();
        CreateMap<MemberMappingDto, MemberMappingBaseDto>();

        CreateMap<ProspectMapping, ProspectMappingDto>();
        CreateMap<ProspectMappingDto, ProspectMapping>();

        CreateMap<User, UserDto>();
        CreateMap<UserDto, User>();
    }
}