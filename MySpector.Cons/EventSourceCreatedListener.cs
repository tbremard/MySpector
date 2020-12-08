using System.Diagnostics.Tracing;
using System.Text;
using NLog;

namespace MySpector.Cons
{
    sealed class EventSourceCreatedListener : EventListener
    {
        static Logger _log = LogManager.GetCurrentClassLogger();
        protected override void OnEventSourceCreated(EventSource eventSource)
        {
            base.OnEventSourceCreated(eventSource);
            _log.Trace($"New event source: {eventSource.Name}");
        }
    }
    
    sealed class EventSourceListener : EventListener
    {
        static Logger _log = LogManager.GetCurrentClassLogger();
        private readonly string _eventSourceName;
        private readonly StringBuilder _messageBuilder = new StringBuilder();

        public EventSourceListener(string name)
        {
            _eventSourceName = name;
        }

        protected override void OnEventSourceCreated(EventSource eventSource)
        {
            base.OnEventSourceCreated(eventSource);

            if (eventSource.Name == _eventSourceName)
            {
                EnableEvents(eventSource, EventLevel.LogAlways, EventKeywords.All);
            }
        }

        protected override void OnEventWritten(EventWrittenEventArgs eventData)
        {
            base.OnEventWritten(eventData);

            string message;
            lock (_messageBuilder)
            {
                _messageBuilder.Append("Event ");
                _messageBuilder.Append(eventData.EventSource.Name);
                _messageBuilder.Append(" - ");
                _messageBuilder.Append(eventData.EventName);
                _messageBuilder.Append(" : ");
                _messageBuilder.AppendJoin(',', eventData.Payload);
                message = _messageBuilder.ToString();
                _messageBuilder.Clear();
            }
            _log.Trace(message);
        }
    }    
}