using AutoMapper;
using MassTransit;
using TH.EventBus.Messages;

namespace TH.EmailMS.API;

public class EmailEventConsumer : IConsumer<EmailEvent>, IDisposable
{
    private readonly IEmailService _emailService;
    private readonly ILogger<EmailEventConsumer> _logger;
    private readonly IMapper _mapper;

    public EmailEventConsumer(IEmailService emailService, ILogger<EmailEventConsumer> logger, IMapper mapper)
    {
        if (emailService == null) throw new ArgumentNullException(nameof(emailService));
        _emailService = emailService ?? throw new ArgumentNullException(nameof(emailService));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public async Task Consume(ConsumeContext<EmailEvent> context)
    {
        if (context == null) throw new ArgumentNullException(nameof(context));

        var emailEvent = context.Message;
        var result = await _emailService.SendEmailAsync(_mapper.Map<EmailEvent, EmailInputModel>(emailEvent));
    }

    public void Dispose()
    {
        _emailService?.Dispose();
    }
}