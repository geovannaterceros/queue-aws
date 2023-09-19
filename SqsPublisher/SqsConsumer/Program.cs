// See https://aka.ms/new-console-template for more information

using Amazon.SQS;
using Amazon.SQS.Model;

var cts = new CancellationTokenSource();
var sqsClient = new AmazonSQSClient();

var queueUrlResponse = await sqsClient.GetQueueUrlAsync("customers");

var receiveMessageRequest = new ReceiveMessageRequest
{
    QueueUrl = queueUrlResponse.QueueUrl,
    AttributeNames = new List<string>{ "All"},
    MessageAttributeNames = new List<string>{ "All"}
};

while(!cts.IsCancellationRequested)
{
    var response = await sqsClient.ReceiveMessageAsync(receiveMessageRequest, cts.Token);

    foreach (var messsage in response.Messages)
    {
        Console.WriteLine($"Message Id: {messsage.MessageId}");
        Console.WriteLine($"Message body: {messsage.Body}");
        await sqsClient.DeleteMessageAsync(queueUrlResponse.QueueUrl, messsage.ReceiptHandle);
    }

    await Task.Delay(1000);
}


Console.WriteLine("Hello, World!");