using AutoMapper;
using MassTransit;
using Microsoft.Extensions.Configuration;
using TH.Common.Model;

namespace TH.Common.Model;

public class BaseService : IBaseService
{
    public IMapper Mapper { get; set; }
    public readonly IConfiguration Config;
    public IPublishEndpoint PublishEndpoint { get; set; }
    public UserResolver UserResolver { get; set; }

    public void SetUserResolver(UserResolver userResolver)
    {
        if (userResolver == null) throw new ArgumentNullException(nameof(userResolver));
        UserResolver = userResolver;
    }

    public BaseService(IMapper mapper, IPublishEndpoint publishEndpoint, IConfiguration config)
    {
        Mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        PublishEndpoint = publishEndpoint ?? throw new ArgumentNullException(nameof(publishEndpoint));
        Config = config ?? throw new ArgumentNullException(nameof(config));
    }

    public virtual void Dispose()
    {
    }
}