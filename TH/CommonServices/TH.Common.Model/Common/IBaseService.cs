using AutoMapper;
using MassTransit;
using TH.Common.Model;

namespace TH.Common.Model;

public interface IBaseService : IDisposable
{
    public IMapper Mapper { get; set; }
    public IPublishEndpoint PublishEndpoint { get; set; }
    public UserResolver UserResolver { get; set; }

    void SetUserResolver(UserResolver userResolver);
}