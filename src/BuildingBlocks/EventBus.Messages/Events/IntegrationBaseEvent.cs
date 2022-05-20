using System;

namespace EventBus.Messages.Events
{
    public class IntegrationBaseEvent
    {
        public IntegrationBaseEvent()
        {
            Id = Guid.NewGuid();
            CreationName = DateTime.UtcNow;
        }

        public IntegrationBaseEvent(Guid id, DateTime createDate)
        {
            Id = id;
            CreationName = createDate;
        }
        public Guid Id { get; set; }
        public DateTime CreationName { get; private set; }
    }
}
