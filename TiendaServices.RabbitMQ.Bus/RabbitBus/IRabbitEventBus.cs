using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TiendaServices.RabbitMQ.Bus.Commands;
using TiendaServices.RabbitMQ.Bus.Events;

namespace TiendaServices.RabbitMQ.Bus.RabbitBus
{
    public interface IRabbitEventBus
    {
        Task SendCommand<T>(T command) where T : Command;

        void Publish<T>(T command) where T : Event;

        void Subscribe<T, TH>() where T : Event
                                where TH : IEventHandler<T>;
    }
}
