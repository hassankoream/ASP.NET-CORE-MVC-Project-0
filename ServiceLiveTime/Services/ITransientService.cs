namespace ServiceLiveTime.Services
{
    public interface ITransientService
    {
        string GetGuid();
    }

    public class TransientService : ITransientService
    {
        private Guid _guid;
        public TransientService()
        {
            _guid = Guid.NewGuid(); //Unique Id
        }
        public string GetGuid()
        {
            return _guid.ToString();
        }
    }
}
