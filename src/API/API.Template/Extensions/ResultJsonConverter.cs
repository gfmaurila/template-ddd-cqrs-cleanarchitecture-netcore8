using CSharpFunctionalExtensions;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace API.Template.Extensions;

public class ResultJsonConverter<TValue, TError> : JsonConverter<Result<TValue, TError>>
{
    public override Result<TValue, TError> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        throw new NotSupportedException("Deserialization is not supported.");
    }

    public override void Write(Utf8JsonWriter writer, Result<TValue, TError> value, JsonSerializerOptions options)
    {
        if (value.IsSuccess)
        {
            JsonSerializer.Serialize(writer, new
            {
                isSuccess = value.IsSuccess,
                isFailure = value.IsFailure,
                Data = value.Value
            }, options);
        }
        else
        {
            JsonSerializer.Serialize(writer, new
            {
                isSuccess = value.IsSuccess,
                isFailure = value.IsFailure,
                error = value.Error
            }, options);
        }
    }
}



