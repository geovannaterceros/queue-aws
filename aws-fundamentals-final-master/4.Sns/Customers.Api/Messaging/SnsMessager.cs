using System.Text.Json;
using Amazon.SimpleNotificationService;
using Microsoft.Extensions.Options;
using Amazon.SimpleNotificationService.Model;

namespace Customers.Api.Messaging;

public class SnsMessager : ISnsMessager
{
    private readonly IAmazonSimpleNotificationService _sns;
    private readonly IOptions<TopicSettings> _topicSettings;
    private string? _topicArn;

    public SnsMessager(IAmazonSimpleNotificationService sns, IOptions<TopicSettings> topicSettings)
    {
        _sns = sns;
        _topicSettings = topicSettings;
    }

    private async ValueTask<string> GetTopicArnAsync<T>()
    {
        if (_topicArn is not null)
        {
            return _topicArn;
        }

        var queueUrlResponse = await _sns.FindTopicAsync(_topicSettings.Value.Name);
        _topicArn = queueUrlResponse.TopicArn;
        return _topicArn;
    }

    public async Task<PublishResponse> PublishMessageAsync<T>(T message)
    {
        var topicArn = await GetTopicArnAsync<string>();
        
        var sendMessageRequest = new PublishRequest
        {
            TopicArn = topicArn,
            Message = JsonSerializer.Serialize(message),
            MessageAttributes = new Dictionary<string, MessageAttributeValue>
            {
                {
                    "MessageType", new MessageAttributeValue
                    {
                        DataType = "String",
                        StringValue = typeof(T).Name
                    }
                }
            }
        };

        return await _sns.PublishAsync(sendMessageRequest);
    }
}