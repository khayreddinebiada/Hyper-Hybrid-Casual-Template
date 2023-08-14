
namespace HCEngine.Exceptions
{
    public class ObjectNotFoundException : System.Exception
    {
        public ObjectNotFoundException(string message = "The object is not found...") : base(message)
        {

        }
    }
}
