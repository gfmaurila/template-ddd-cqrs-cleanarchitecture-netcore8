namespace Template.Application.Feature.Users.Commands.Create.Messaging.Consumer;

public interface IUserCreatedMessageProcessor
{
    Task ProcessMessageAsync(string message);
}
