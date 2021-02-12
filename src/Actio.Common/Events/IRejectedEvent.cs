namespace Actio.Common.Events
{
    public interface IRejectedEvent : IEvent
    {
        public string Reason { get; }
        public string Code { get; }
    }
}
