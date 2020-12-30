using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TiendaServices.RabbitMQ.Bus.Events;

namespace TiendaServices.RabbitMQ.Bus.RabbitBus
{
    public interface IEventHandler<in TEvent> : IEventHandler where TEvent : Event
    {
        Task Handle(TEvent @event);
    }

    public interface IEventHandler { }
}
