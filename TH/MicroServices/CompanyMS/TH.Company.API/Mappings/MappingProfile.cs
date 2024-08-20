using AutoMapper;
using TH.CompanyMS.App;
using TH.CompanyMS.Core;

namespace TH.CompanyMS.API;

public class MappingProfile:Profile
{
    public MappingProfile()
    {
        //inputs
        CreateMap<BranchInputModel, Branch>().ReverseMap();
        CreateMap<BranchUserInputModel, BranchUser>().ReverseMap();
        CreateMap<CompanyInputModel, Company>().ReverseMap();
        CreateMap<ModuleInputModel, Module>().ReverseMap();
        CreateMap<PermissionInputModel, Permission>().ReverseMap();
        CreateMap<RoleInputModel, Role>().ReverseMap();
        CreateMap<UserInputModel, User>().ReverseMap();
        CreateMap<UserRoleInputModel, UserRole>().ReverseMap();

        //view
        CreateMap<BranchViewModel, Branch>().ReverseMap();
        CreateMap<BranchUserViewModel, BranchUser>().ReverseMap();
        CreateMap<CompanyViewModel, Company>().ReverseMap();
        CreateMap<ModuleViewModel, Module>().ReverseMap();
        CreateMap<PermissionViewModel, Permission>()
            .ReverseMap()
            .ForMember(dest=>dest.ControllerName, m=>m.MapFrom(src=>src.Module.ControllerName));
        CreateMap<RoleViewModel, Role>().ReverseMap();
        CreateMap<UserViewModel, User>().ReverseMap();
        CreateMap<UserRoleViewModel, UserRole>().ReverseMap();

        //filters
        CreateMap<Branch, BranchFilterModel>().ReverseMap();
        CreateMap<BranchUser, BranchUserFilterModel>().ReverseMap();
        CreateMap<Company, CompanyFilterModel>().ReverseMap();
        CreateMap<Module, ModuleFilterModel>().ReverseMap();
        CreateMap<Permission, PermissionFilterModel>().ReverseMap();
        CreateMap<Role, RoleFilterModel>().ReverseMap();
        CreateMap<User, UserFilterModel>().ReverseMap();
        CreateMap<UserRole, UserRoleFilterModel>().ReverseMap();

        //protobuf
        //CreateMap<PermissionRequest, PermissionFilterModel>().ReverseMap();
        //CreateMap<Permission, PermissionResponse>().ReverseMap();
    }
}