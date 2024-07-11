using TH.Repo;
using TH.ShadowMS.App;
using TH.ShadowMS.Core;

namespace TH.ShadowMS.Infra;

public class ShadowRepo:RepoMongoDb<Shadow>, IShadowRepo
{
    public ShadowRepo(IDatabase database) : base(database)
    {
    }

    public void Dispose()
    {
        //
    }
}