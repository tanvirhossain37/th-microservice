using AutoMapper;
using TH.Common.Lang;
using TH.Common.Model;
using TH.CompanyMS.App;
using TH.CompanyMS.Core;

namespace TH.CompanyMS.API;

public class CompanyMappingProfile : Profile
{
    public CompanyMappingProfile()
    {
        //filter
        CreateMap<Branch, BranchFilterModel>().ReverseMap();
        CreateMap<BranchUser, BranchUserFilterModel>().ReverseMap();
        CreateMap<Company, CompanyFilterModel>().ReverseMap();
        CreateMap<CompanySetting, CompanySettingFilterModel>().ReverseMap();
        CreateMap<Module, ModuleFilterModel>().ReverseMap();
        CreateMap<Permission, PermissionFilterModel>().ReverseMap();
        CreateMap<Role, RoleFilterModel>().ReverseMap();
        CreateMap<SpaceSubscription, SpaceSubscriptionFilterModel>().ReverseMap();
        CreateMap<User, UserFilterModel>().ReverseMap();
        CreateMap<UserCompany, UserCompanyFilterModel>().ReverseMap();
        CreateMap<UserRole, UserRoleFilterModel>().ReverseMap();

        //input
        CreateMap<BranchInputModel, Branch>().ReverseMap();
        CreateMap<BranchUserInputModel, BranchUser>().ReverseMap();
        CreateMap<CompanyInputModel, Company>().ReverseMap();
        CreateMap<CompanySettingInputModel, CompanySetting>().ReverseMap();
        CreateMap<ModuleInputModel, Module>().ReverseMap();
        CreateMap<PermissionInputModel, Permission>().ReverseMap();
        CreateMap<RoleInputModel, Role>().ReverseMap();
        CreateMap<SpaceSubscriptionInputModel, SpaceSubscription>().ReverseMap();
        CreateMap<UserInputModel, User>().ReverseMap();
        CreateMap<UserCompanyInputModel, UserCompany>().ReverseMap();
        CreateMap<UserRoleInputModel, UserRole>().ReverseMap();

        //view
        CreateMap<BranchViewModel, Branch>().ReverseMap();
        CreateMap<BranchUserViewModel, BranchUser>().ReverseMap();
        CreateMap<CompanyViewModel, Company>().ReverseMap();
        CreateMap<ModuleViewModel, Module>().ReverseMap();
        CreateMap<PermissionViewModel, Permission>()
            .ReverseMap()
            .ForMember(dest => dest.ControllerName, m => m.MapFrom(src => src.Module.ControllerName));
        CreateMap<RoleViewModel, Role>().ReverseMap();
        CreateMap<UserViewModel, User>().ReverseMap();
        CreateMap<UserRoleViewModel, UserRole>().ReverseMap();

        CreateMap<BranchViewModel, Branch>().ReverseMap();
        CreateMap<BranchUserViewModel, BranchUser>().ReverseMap();
        CreateMap<CompanyViewModel, Company>().ReverseMap();
        CreateMap<CompanySettingViewModel, CompanySetting>().ReverseMap();
        CreateMap<ModuleViewModel, Module>().ReverseMap();
        CreateMap<PermissionViewModel, Permission>()
            .ReverseMap()
            .ForMember(dest => dest.SpaceName, m => m.MapFrom(mapExpression: src => $"{string.Format(Lang.Find("space_name"), src.Role.UserRoles.SingleOrDefault(x => x.RoleId == src.RoleId).User.Name)}"))
            .ForMember(dest => dest.ControllerName, m => m.MapFrom(src => src.Module.ControllerName));
        //no need
        //.ForMember(dest => dest.ModuleName, m => m.MapFrom(src => Lang.Find(src.Module.Name)));
        CreateMap<RoleViewModel, Role>().ReverseMap();
        CreateMap<UserViewModel, User>().ReverseMap();
        CreateMap<UserCompanyViewModel, UserCompany>().ReverseMap()
            .ForMember(dest => dest.TypeName, m => m.MapFrom(src => GetTypeName(src)))
            .ForMember(dest => dest.StatusName, m => m.MapFrom(src => GetStatusName(src)))
            .ForMember(dest => dest.UserName, m => m.MapFrom(mapExpression: src => src.User.UserName))
            .ForMember(dest => dest.SpaceName, m => m.MapFrom(mapExpression: src => $"{string.Format(Lang.Find("space_name"), src.User.Name)}"))
            .ForMember(dest => dest.DefaultBranchId, m => m.MapFrom(mapExpression: src => src.Company.Branches.FirstOrDefault(x => x.IsDefault == true).Id))
            .ForMember(dest => dest.DefaultBranchName, m => m.MapFrom(mapExpression: src => src.Company.Branches.FirstOrDefault(x => x.IsDefault == true).Name))
            .ForMember(dest => dest.DefaultBranchCode, m => m.MapFrom(mapExpression: src => src.Company.Branches.FirstOrDefault(x => x.IsDefault == true).Code));
        CreateMap<UserRoleViewModel, UserRole>().ReverseMap();
        CreateMap<SpaceSubscriptionViewModel, SpaceSubscription>().ReverseMap();

        //filters
        CreateMap<Branch, BranchFilterModel>().ReverseMap();
        CreateMap<BranchUser, BranchUserFilterModel>().ReverseMap();
        CreateMap<Company, CompanyFilterModel>().ReverseMap();
        CreateMap<Module, ModuleFilterModel>().ReverseMap();
        CreateMap<Permission, PermissionFilterModel>().ReverseMap();
        CreateMap<Role, RoleFilterModel>().ReverseMap();
        CreateMap<SpaceSubscription, SpaceSubscriptionFilterModel>().ReverseMap();
        CreateMap<User, UserFilterModel>().ReverseMap();
        CreateMap<UserCompany, UserCompanyFilterModel>().ReverseMap();
        CreateMap<UserRole, UserRoleFilterModel>().ReverseMap();



        //protobuf
        //CreateMap<PermissionRequest, PermissionFilterModel>().ReverseMap();
        //CreateMap<Permission, PermissionResponse>().ReverseMap();
    }

    private string GetStatusName(UserCompany src)
    {
        if (src.TypeId == (int)InvitationStatusEnum.Pending)
        {
            return Lang.Find("pending");
        }
        else if (src.TypeId == (int)InvitationStatusEnum.Accept)
        {
            return Lang.Find("accept");
        }
        else if (src.TypeId == (int)InvitationStatusEnum.Deny)
        {
            return Lang.Find("deny");
        }
        else
        {
            return string.Empty;
        }
    }

    private string GetTypeName(UserCompany src)
    {
        if (src.TypeId == (int)CompanyTypeEnum.Owner)
        {
            return Lang.Find("owner");
        }
        else if (src.TypeId == (int)CompanyTypeEnum.Owner)
        {
            return Lang.Find("guest");
        }
        else
        {
            return string.Empty;
        }
    }
}