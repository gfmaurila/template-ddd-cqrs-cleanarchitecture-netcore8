using CSharpFunctionalExtensions;

namespace API.Template.Extensions;

public static class ResultExtensions
{
    public static object ToSerializableObject<TValue, TError>(this Result<TValue, TError> result)
    {
        if (result.IsSuccess)
        {
            return new
            {
                IsSuccess = true,
                IsFailure = false,
                Data = result.Value
            };
        }

        return new
        {
            IsSuccess = false,
            IsFailure = true,
            error = result.Error?.ToString()
        };
    }
}
