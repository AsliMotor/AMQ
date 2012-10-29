using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NServiceBus;
using AsliMotor.Products.Models;

namespace AsliMotor.Products.Events
{
    [Serializable]
    public class ProductCreated : IMessage
    {
        public Product Payload { get; set; }
        public string UserName { get; set; }
    }
}
