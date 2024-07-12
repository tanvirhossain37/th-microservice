using AutoMapper;
using MassTransit;
using TH.Common.Model;

namespace TH.CompanyMS.App;

public abstract class BaseService : IBaseService
{
    public IMapper Mapper { get; set; }
    public IPublishEndpoint PublishEndpoint { get; set; }
    public UserResolver UserResolver { get; set; }

    protected BaseService(IMapper mapper, IPublishEndpoint publishEndpoint)
    {
        Mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        PublishEndpoint = publishEndpoint ?? throw new ArgumentNullException(nameof(publishEndpoint));
        //UserResolver = userResolver ?? throw new ArgumentNullException(nameof(userResolver));
    }

    public abstract void Dispose();
}