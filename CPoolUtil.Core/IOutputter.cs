namespace CPoolUtil.Core
{
    public interface IOutputter
    {
        public void Write(string message);
        public void WriteLine(string message = null);
    }
}
