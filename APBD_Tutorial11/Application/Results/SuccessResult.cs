namespace APBD_Tutorial11.Application.Results;

public record SuccessResult(string Message) : CommandResult(Message);