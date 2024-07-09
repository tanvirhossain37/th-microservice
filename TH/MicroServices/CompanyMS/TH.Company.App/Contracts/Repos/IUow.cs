using System;

namespace TH.CompanyMS.App;

public interface IUow : IDisposable
{   
	IBranchRepo BranchRepo { get; set; }
	IBranchUserRepo BranchUserRepo { get; set; }
	ICompanyRepo CompanyRepo { get; set; }
	IModuleRepo ModuleRepo { get; set; }
	IPermissionRepo PermissionRepo { get; set; }
	IRoleRepo RoleRepo { get; set; }
	IUserRepo UserRepo { get; set; }
	IUserRoleRepo UserRoleRepo { get; set; }

    Task<int> SaveChangesAsync();
}