using System.Collections.Generic;

namespace Study.Application.Common;

public class OperationResult
{
	public bool Success { get; set; }
	public string? ErrorCode { get; set; }
	public string? ErrorMessage { get; set; }
	public Dictionary<string, string[]>? ValidationErrors { get; set; }

	public static OperationResult Ok() => new() { Success = true };
	public static OperationResult Fail(string code, string message, Dictionary<string, string[]>? validation = null)
		=> new() { Success = false, ErrorCode = code, ErrorMessage = message, ValidationErrors = validation };
}

public class OperationResult<T> : OperationResult
{
	public T? Data { get; set; }
	public static OperationResult<T> Ok(T data) => new() { Success = true, Data = data };
	public static new OperationResult<T> Fail(string code, string message, Dictionary<string, string[]>? validation = null)
		=> new() { Success = false, ErrorCode = code, ErrorMessage = message, ValidationErrors = validation };
}
