using AutoMapper;
using MassTransit;
using TH.EventBus.Messages;

namespace TH.EmailMS.API;

public class SignInConsumer : IConsumer<SignInEvent>, IDisposable
{
    private readonly IEmailService _emailService;
    private readonly ILogger<SignInConsumer> _logger;
    private readonly IMapper _mapper;

    public SignInConsumer(IEmailService emailService, ILogger<SignInConsumer> logger, IMapper mapper)
    {
        if (emailService == null) throw new ArgumentNullException(nameof(emailService));
        _emailService = emailService ?? throw new ArgumentNullException(nameof(emailService));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public async Task Consume(ConsumeContext<SignInEvent> context)
    {
        if (context == null) throw new ArgumentNullException(nameof(context));

        var signInEvent = context.Message;
        var result = await _emailService.SendEmailAsync(_mapper.Map<SignInEvent, EmailInputModel>(signInEvent));
    }

    public void Dispose()
    {
        _emailService?.Dispose();
    }
}