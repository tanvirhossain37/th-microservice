using AutoMapper;
using MassTransit;
using Microsoft.Extensions.Logging;
using TH.Common.Model;
using TH.EventBus.Messages;
using TH.ShadowMS.Core;

namespace TH.ShadowMS.App;

public class ShadowEventConsumer : IConsumer<ShadowEvent>, IDisposable
{
    private readonly IShadowService _shadowService;
    private readonly ILogger<ShadowEventConsumer> _logger;
    private readonly IMapper _mapper;
    private readonly DataFilter _dataFilter;

    public ShadowEventConsumer(IShadowService shadowService, ILogger<ShadowEventConsumer> logger, IMapper mapper)
    {
        _shadowService = shadowService ?? throw new ArgumentNullException(nameof(shadowService));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _dataFilter = new DataFilter();
    }

    public async Task Consume(ConsumeContext<ShadowEvent> context)
    {
        if (context == null) throw new ArgumentNullException(nameof(context));

        var shadowEvent = context.Message;
        var result = await _shadowService.SaveAsync(_mapper.Map<ShadowEvent, Shadow>(shadowEvent), _dataFilter);
    }

    public void Dispose()
    {
        _shadowService?.Dispose();
    }
}