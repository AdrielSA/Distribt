namespace Distribt.Shared.Communication.Messages.Interfaces
{
    public interface IMessage
    {
        public string MessageIdentifier { get; }
        public string Name { get; }
    }
}
