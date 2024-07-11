using TH.Repo;
using TH.ShadowMS;
using TH.ShadowMS.Core;

namespace TH.ShadowMS.App;

public interface IShadowRepo : IRepoMongoDB<Shadow>, IDisposable
{
}