// See https://aka.ms/new-console-template for more information

using Amazon.SecretsManager;
using Amazon.SecretsManager.Model;

var secretsManagerClient = new AmazonSecretsManagerClient();

var listSecretVersionRequest = new ListSecretVersionIdsRequest
{
    SecretId    = "ApiKey",
    IncludeDeprecated = true
};

var versionResponse = await secretsManagerClient.ListSecretVersionIdsAsync(listSecretVersionRequest);

var request = new GetSecretValueRequest
{
    SecretId = "ApiKey",
    VersionStage = "AWSCURRENT"
};

var response = await secretsManagerClient.GetSecretValueAsync(request);


var describeSecretRequest = new DescribeSecretRequest
{
    SecretId = "ApiKey"
};

var describeResponse = await secretsManagerClient.DescribeSecretAsync(describeSecretRequest);

Console.WriteLine(describeResponse);
Console.WriteLine(response.SecretString);