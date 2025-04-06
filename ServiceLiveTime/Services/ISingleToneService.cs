namespace ServiceLiveTime.Services
{
    public interface ISingleToneService
    {
        string GetGuid();
    }

    public class SingleToneService : ISingleToneService
    {
        private Guid _guid;
        public SingleToneService()
        {
            _guid = Guid.NewGuid(); //Unique Id
        }
        public string GetGuid()
        {
            return _guid.ToString();
        }
    }
}
