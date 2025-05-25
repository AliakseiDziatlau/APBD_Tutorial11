namespace APBD_Tutorial11.Application.Results;

public record NotFoundResult(string Message) : CommandResult(Message);