namespace Template.Common.Domain.Services;

public interface IDateTimeProvider
{
    DateTimeOffset UtcNow();
}
