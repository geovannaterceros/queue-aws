
using Amazon.S3;
using Amazon.S3.Model;


var s3Client = new AmazonS3Client();

await using var inputstream = new FileStream("./movies.csv", FileMode.Open, FileAccess.Read);

var putObjectRequest = new PutObjectRequest
{
    BucketName = "geoawscourse",
    Key = "file/csv",
    ContentType = "text/csv",
    InputStream = inputstream
};

await s3Client.PutObjectAsync(putObjectRequest);

Console.WriteLine();
