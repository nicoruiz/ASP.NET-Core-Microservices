using System;
using System.Threading.Tasks;

namespace TiendaServices.RabbitMQ.Bus.Events
{
    public abstract class Event
    {
        public DateTime TimeStamp { get; protected set; }

        protected Event()
        {
            TimeStamp = DateTime.Now;
        }

        public virtual void Handle()
        {
            throw new NotImplementedException();
        }
    }
}
