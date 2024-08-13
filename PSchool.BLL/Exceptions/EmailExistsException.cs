namespace PSchool.BLL.Exceptions;

public class EmailExistsException(string message) : Exception(message);