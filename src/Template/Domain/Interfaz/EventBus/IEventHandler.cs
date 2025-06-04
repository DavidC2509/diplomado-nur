namespace Template.Domain.Interfaz.EventBus
{
    public interface IEventHandler
    {
        Task MessageHandler(string messageEvent);

        Task ErrorHandler(string messageEvent);
    }
}