namespace Bank.Application.Common.Exceptions;

public class NotFoundException : Exception
{
    public NotFoundException() : base("Объект не найден")
    {
    }

    public NotFoundException(string objectName) : base($"{objectName} не найден")
    {
    }

    public NotFoundException(string objectName, int objectId) : base($"{objectName} ({objectId}) не найден")
    {
    }
}