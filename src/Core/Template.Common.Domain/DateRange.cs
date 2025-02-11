using Template.Common.Domain.Extensions;

namespace Template.Common.Domain;

public sealed class DateRange : ValueObject
{
    private DateRange(DateTimeOffset startDate, DateTimeOffset endDate)
    {
        StartDate = startDate;
        EndDate = endDate.EnsureGreaterThan(startDate);
    }

    public static DateRange FromString(string startDate, string endDate)
        => new DateRange(DateTimeOffset.Parse(startDate), DateTimeOffset.Parse(endDate));

    public static DateRange From(DateTimeOffset startDate, DateTimeOffset endDate)
        => new DateRange(startDate, endDate);

    public DateTimeOffset StartDate { get; }
    public DateTimeOffset EndDate { get; }
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return StartDate;
        yield return EndDate;
    }

    public void InRange(DateTimeOffset utcNow)
    {
        utcNow.EnsureWithinRange(StartDate, EndDate);
    }
}