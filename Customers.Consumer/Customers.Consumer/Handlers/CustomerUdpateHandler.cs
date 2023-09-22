using Customers.Consumer.Messages;
using MediatR;

namespace Customers.Consumer.Handlers;

public class CustomerUdpateHandler : IRequestHandler<CustomerUpdated>
{
    private readonly ILogger<CustomerUdpateHandler> _logger;

    public CustomerUdpateHandler(ILogger<CustomerUdpateHandler> logger)
    {
        _logger = logger;
    }
    
    public Task<Unit> Handle(CustomerUpdated request, CancellationToken cancellationToken)
    {
        _logger.LogInformation(request.FullName);
        return Unit.Task;
    }
}