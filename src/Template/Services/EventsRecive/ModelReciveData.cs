namespace Template.Services.EventsRecive
{
    public class ModelReciveData<T>
    {
        public string EventType { get; set; }
        public string EventVersion { get; set; }
        public T Body { get; set; }
        public string Source { get; set; }
    }
}