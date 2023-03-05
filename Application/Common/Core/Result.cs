using Application.Common.Enums;

namespace Application.Common.Core;

public class Result<T>
{
    public T Data { get; set; }
    public string Message { get; set; }
    public ReturnTypes ReturnType { get; set; }

    public static Result<T> Return(ReturnTypes returnType, T data = default, string message = null)
        => new Result<T>
        {
            Data = data,
            Message = message,
            ReturnType = returnType
        };
}