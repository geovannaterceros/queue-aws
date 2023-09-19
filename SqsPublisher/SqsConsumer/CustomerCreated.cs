namespace SqsConsumer;

public class CustomerCreated
{
    public Guid Id { get; set; }
    public string Email { get; set; }
    public string FullName { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string GitHubUsername { get; set; }
}