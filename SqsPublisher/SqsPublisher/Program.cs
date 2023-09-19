// See https://aka.ms/new-console-template for more information

using System.Text.Json;
using Amazon.SQS;
using Amazon.SQS.Model;
using SqsPublisher;

var sqsClient = new AmazonSQSClient();

CustomerCreated customer = new CustomerCreated()
{
    Id = Guid.NewGuid(),
    Email = "nick@nickchapsas.com",
    FullName = "Nick Chapsas",
    DateOfBirth = new DateTime(1993, 1, 1),
    GitHubUsername = "nickchapsas"
};


var queueUrlResponse = await sqsClient.GetQueueUrlAsync("customers");

var sendMessageRequest = new SendMessageRequest
{
        QueueUrl = queueUrlResponse.QueueUrl,
        MessageBody = JsonSerializer.Serialize(customer)
};

var response = await sqsClient.SendMessageAsync(sendMessageRequest);

Console.WriteLine("Hello, World!");
