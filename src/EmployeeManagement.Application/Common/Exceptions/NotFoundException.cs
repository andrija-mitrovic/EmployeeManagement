namespace EmployeeManagement.Application.Common.Exceptions
{
    public abstract class NotFoundException : Exception
    {
        protected NotFoundException()
        : base()
        {
        }

        protected NotFoundException(string message)
            : base(message)
        {
        }

        protected NotFoundException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        protected NotFoundException(string name, object key)
            : base($"Entity \"{name}\" ({key}) was not found.")
        {
        }
    }
}
