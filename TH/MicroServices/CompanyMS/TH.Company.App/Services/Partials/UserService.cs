using System.Linq.Expressions;
using TH.AuthMS.Grpc;
using TH.Common.Lang;
using TH.Common.Model;
using TH.Common.Util;
using TH.CompanyMS.Core;
using TH.EventBus.Messages;

namespace TH.CompanyMS.App;

public partial class UserService
{
    //Add additional services if any

    //private UserService(IUow repo, IPublishEndpoint publishEndpoint, IMapper mapper, IBranchUserService branchUserService, IUserRoleService userRoleService) : this(repo, publishEndpoint, mapper, branchUserService, userRoleService)
    //{
    //}

    private async Task ApplyOnSavingBlAsync(User entity, bool invitation, DataFilter dataFilter)
    {
        if (entity == null) throw new ArgumentNullException(nameof(entity));

        //todo
        //validation
        if (entity.UserRoles.Count <= 0) throw new CustomException($"{Lang.Find("validation_error")}: UserRoles");
        foreach (var userRole in entity.UserRoles)
        {
            userRole.SpaceId = entity.SpaceId;
            userRole.CompanyId = entity.CompanyId;
            userRole.UserId = entity.Id;
        }


        if (invitation)
        {
            //call auth
            var company = await Repo.CompanyRepo.SingleOrDefaultQueryableAsync(x => x.Id == entity.CompanyId);
            if (company == null) throw new CustomException(Lang.Find("error_notfound"));

            var signUpInputRequest = new SignUpInputRequest
            {
                Name = entity.Name,
                Provider = TS.Providers.LOCAL,
                UserName = Util.TryGenerateUserName(entity.UserName),
                Email = entity.UserName,
                Password = Util.TryGenerateCode(),
                ReferralId = UserResolver.UserName,
                CompanyName = company.Name,
                IsAutoUserName = false,
                EmailConfirmed = true
            };
            //grpc
            var applicationUser = await _authGrpcClientService.SignUpAsync(signUpInputRequest);

            //override
            entity.UserName = applicationUser?.UserName;

            var userCompany = new UserCompany
            {
                Id = Util.TryGenerateGuid(),
                SpaceId = entity.SpaceId,
                CompanyId = entity.CompanyId,
                TypeId = (int)CompanyTypeEnum.Guest,
                StatusId = (int)InvitationStatusEnum.Pending,
                UserId = entity.Id
            };

            entity.UserCompanies.Add(userCompany);
        }
    }

    private async Task ApplyOnSavedBlAsync(User entity, DataFilter dataFilter)
    {
        if (entity == null) throw new ArgumentNullException(nameof(entity));

        //todo
        //override
        //entity.UserTypeId = (int)UserTypeEnum.TenantUser;

        //var company = await Repo.CompanyRepo.SingleOrDefaultQueryableAsync(x=>x.Id==entity.CompanyId);

        //var userCreateEvent = new UserCreateEvent
        //{
        //    Name = entity.Name,
        //    UserName = Util.TryGenerateUserName(entity.UserName.Split("@")[0]),
        //    Email = entity.UserName,
        //    Password = Util.TryGenerateCode(),
        //    ReferralId = UserResolver.UserName,
        //    CompanyName = company.Name,
        //    IsAutoUserName = false
        //};

        //PublishEndpoint.Publish(userCreateEvent);

        ////auth service call - grpc
        //var request = new SignUpRequest
        //{
        //    Name = entity.Name,
        //    Email = entity.UserName,
        //    Password = Util.TryGenerateCode()
        //};

        //var reply = await _authGrpcClientService.SaveAuthUserAsync(request);

        ////override username
        //entity.UserName = userCreateEvent.UserName;
    }

    private async Task ApplyOnUpdatingBlAsync(User existingEntity, DataFilter dataFilter)
    {
        if (existingEntity == null) throw new ArgumentNullException(nameof(existingEntity));

        //todo
    }

    private async Task ApplyOnUpdatedBlAsync(User existingEntity, DataFilter dataFilter)
    {
        if (existingEntity == null) throw new ArgumentNullException(nameof(existingEntity));

        //todo
    }

    private async Task ApplyOnArchivingBlAsync(User existingEntity, DataFilter dataFilter)
    {
        if (existingEntity == null) throw new ArgumentNullException(nameof(existingEntity));

        //todo
    }

    private async Task ApplyOnArchivedBlAsync(User existingEntity, DataFilter dataFilter)
    {
        if (existingEntity == null) throw new ArgumentNullException(nameof(existingEntity));

        //todo
    }

    private async Task ApplyOnDeletingBlAsync(User existingEntity, DataFilter dataFilter)
    {
        if (existingEntity == null) throw new ArgumentNullException(nameof(existingEntity));

        //todo
    }

    private async Task ApplyOnDeletedBlAsync(User existingEntity, DataFilter dataFilter)
    {
        if (existingEntity == null) throw new ArgumentNullException(nameof(existingEntity));

        //todo
    }

    private async Task ApplyOnFindBlAsync(User entity, DataFilter dataFilter)
    {
        if (entity == null) throw new ArgumentNullException(nameof(entity));

        //todo
    }

    private async Task ApplyOnGetBlAsync(UserFilterModel filter, DataFilter dataFilter)
    {
        if (filter == null) throw new ArgumentNullException(nameof(filter));

        //todo
    }

    private async Task ApplyCustomGetFilterBlAsync(UserFilterModel filter, List<Expression<Func<User, bool>>> predicates, List<Expression<Func<User, object>>> includePredicates, DataFilter dataFilter)
    {
        if (filter == null) throw new ArgumentNullException(nameof(filter));
        if (predicates == null) throw new ArgumentNullException(nameof(predicates));

        //todo
        //additional
        if (filter.StartDate.HasValue && filter.EndDate.HasValue)
        {
            filter.StartDate = Util.TryFloorTime((DateTime)filter.StartDate);
            filter.EndDate = Util.TryCeilTime((DateTime)filter.EndDate);

            predicates.Add(t => (t.CreatedDate >= filter.StartDate) && (t.CreatedDate <= filter.EndDate));
        }
    }

    public async Task<User> SaveAsync(User entity, bool invitation, DataFilter dataFilter, bool commit = true)
    {
        if (entity == null) throw new ArgumentNullException(nameof(entity));

        entity.Id = Util.TryGenerateGuid();
        entity.CreatedDate = DateTime.Now;

        ApplyValidationBl(entity);
        await ApplyDuplicateOnSaveBl(entity, dataFilter);

        //Add your business logic here
        await ApplyOnSavingBlAsync(entity, invitation, dataFilter);

        //Chain effect

        foreach (var child in entity.BranchUsers)
        {
            await BranchUserService.SaveAsync(child, dataFilter, false);
        }

        foreach (var child in entity.UserCompanies)
        {
            await UserCompanyService.SaveAsync(child, dataFilter, false);
        }

        foreach (var child in entity.UserRoles)
        {
            await UserRoleService.SaveAsync(child, dataFilter, false);
        }


        await Repo.UserRepo.SaveAsync(entity);

        if (commit)
        {
            if (await Repo.SaveChangesAsync() <= 0) throw new CustomException(Lang.Find("error_save"));

            //Add your business logic here
            await ApplyOnSavedBlAsync(entity, dataFilter);
        }

        return entity;
    }

    private void DisposeOthers()
    {
        //todo
    }
}