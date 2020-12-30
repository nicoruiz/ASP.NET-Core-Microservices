using System;
using System.Collections.Generic;
using System.Text;
using TiendaServices.RabbitMQ.Bus.Events;

namespace TiendaServices.RabbitMQ.Bus.Commands
{
    public abstract class Command : Message
    {
        public DateTime TimeStamp { get; protected set; }

        protected Command()
        {
            TimeStamp = DateTime.Now;
        }
    }
}
