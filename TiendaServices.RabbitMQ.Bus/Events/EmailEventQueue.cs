using System;
using System.Collections.Generic;
using System.Text;
using TiendaServices.RabbitMQ.Bus.Events;

namespace TiendaServices.RabbitMQ.Bus.EventQueue
{
    public class EmailEventQueue : Event
    {
        public string Receiver { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public EmailEventQueue(string receiver, string title, string content)
        {
            Receiver = receiver;
            Title = title;
            Content = content;
        }
    }
}
