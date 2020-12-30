using System;
using System.Collections.Generic;
using System.Text;
using TiendaServices.RabbitMQ.Bus.Events;

namespace TiendaServices.RabbitMQ.Bus.RabbitBus
{
    public interface IRabbitEventBus
    {
        void Publish<T>(T e) where T : Event;

        void Consume<T>() where T : Event;
    }
}
