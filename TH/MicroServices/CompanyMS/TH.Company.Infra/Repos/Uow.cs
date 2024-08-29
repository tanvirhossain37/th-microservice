using TH.CompanyMS.App;
using TH.CompanyMS.Infra;

namespace TH.CompanyMS.infra;

public class Uow : IUow
{
    private readonly CompanyDbContext _dbContext;
    
	public IBranchRepo BranchRepo { get; set; }
	public IBranchUserRepo BranchUserRepo { get; set; }
	public ICompanyRepo CompanyRepo { get; set; }
	public IModuleRepo ModuleRepo { get; set; }
	public IPermissionRepo PermissionRepo { get; set; }
	public IRoleRepo RoleRepo { get; set; }
	public IUserRepo UserRepo { get; set; }
	public IUserCompanyRepo UserCompanyRepo { get; set; }
	public IUserRoleRepo UserRoleRepo { get; set; }

    public Uow(CompanyDbContext dbContext, IBranchRepo branchRepo, IBranchUserRepo branchUserRepo, ICompanyRepo companyRepo, IModuleRepo moduleRepo, IPermissionRepo permissionRepo, IRoleRepo roleRepo, IUserRepo userRepo, IUserCompanyRepo userCompanyRepo, IUserRoleRepo userRoleRepo)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        
			BranchRepo = branchRepo ?? throw new ArgumentNullException(nameof(branchRepo));
			BranchUserRepo = branchUserRepo ?? throw new ArgumentNullException(nameof(branchUserRepo));
			CompanyRepo = companyRepo ?? throw new ArgumentNullException(nameof(companyRepo));
			ModuleRepo = moduleRepo ?? throw new ArgumentNullException(nameof(moduleRepo));
			PermissionRepo = permissionRepo ?? throw new ArgumentNullException(nameof(permissionRepo));
			RoleRepo = roleRepo ?? throw new ArgumentNullException(nameof(roleRepo));
			UserRepo = userRepo ?? throw new ArgumentNullException(nameof(userRepo));
			UserCompanyRepo = userCompanyRepo ?? throw new ArgumentNullException(nameof(userCompanyRepo));
			UserRoleRepo = userRoleRepo ?? throw new ArgumentNullException(nameof(userRoleRepo));
    }

    public async Task<int> SaveChangesAsync()
    {
        return await _dbContext.SaveChangesAsync();
    }

    public void Dispose()
    {
        _dbContext?.Dispose();
    }
}