namespace APBD_Tutorial11.Application.Results;

public record BadRequestResult(string Message) : CommandResult(Message);