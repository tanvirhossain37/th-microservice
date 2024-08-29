using AutoMapper;
using MassTransit;
using TH.AuthMS.App;
using TH.Common.Model;
using TH.EventBus.Messages;

namespace TH.AuthMS.API;

public class UserCreateEventConsumer: IConsumer<UserCreateEvent>, IDisposable
{
    private readonly IAuthService _authService;
    private readonly IMapper _mapper;

    public UserCreateEventConsumer(IAuthService authService, IMapper mapper)
    {
        _authService = authService ?? throw new ArgumentNullException(nameof(authService));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public async Task Consume(ConsumeContext<UserCreateEvent> context)
    {
        if (context == null) throw new ArgumentNullException(nameof(context));

        var userCreateEvent = context.Message;
        await _authService.SignUpAsync(_mapper.Map<UserCreateEvent, SignUpInputModel>(userCreateEvent), new DataFilter());
    }

    public void Dispose()
    {
        _authService?.Dispose();
    }
}