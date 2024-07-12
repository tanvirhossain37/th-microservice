using AutoMapper;
using MassTransit;
using TH.Common.Model;

namespace TH.CompanyMS.App;

public interface IBaseService : IDisposable
{
    public IMapper Mapper { get; set; }
    public IPublishEndpoint PublishEndpoint { get; set; }
    public UserResolver UserResolver { get; set; }
}