using AutoMapper;
using MassTransit;
using TH.Common.Model;

namespace TH.Common.Model;

public class BaseService : IBaseService
{
    public IMapper Mapper { get; set; }
    public IPublishEndpoint PublishEndpoint { get; set; }
    public UserResolver UserResolver { get; set; }

    public void SetUserResolver(UserResolver userResolver)
    {
        if (userResolver == null) throw new ArgumentNullException(nameof(userResolver));
        UserResolver = userResolver;
    }

    public BaseService(IMapper mapper, IPublishEndpoint publishEndpoint)
    {
        Mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        PublishEndpoint = publishEndpoint ?? throw new ArgumentNullException(nameof(publishEndpoint));
    }

    public virtual void Dispose()
    {
    }
}