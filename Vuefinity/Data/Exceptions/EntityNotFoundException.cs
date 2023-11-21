namespace Vuefinity.Data.Exceptions
{
    public class EntityNotFoundException : Exception
    {
        public EntityNotFoundException(string type, string email)
            : base($"{type} with email: {email} could not be found.")
        {
        }
    }
}
