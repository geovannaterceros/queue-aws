using Customers.Consumer.Messages;
using MediatR;

namespace Customers.Consumer.Handlers;

public class CustomerDeleteHandler
{
    private readonly ILogger<CustomerDeleteHandler> _logger;
    
    public CustomerDeleteHandler(ILogger<CustomerDeleteHandler> logger)
    {
        _logger = logger;
    }
    
    public Task<Unit> Handle(CustomerDeleted request, CancellationToken cancellationToken)
    {
        _logger.LogInformation(request.Id.ToString());
        return Unit.Task;
    }
}