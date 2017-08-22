using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Messaging;

namespace MSMQTest
{
    public class Program
    {
        static void Main(string[] args)
        {
            Person p = new Person();
            p.FirstName = "Abhi";
            p.LastName = "Kulkarni";

            MessageQueue msMq = null;
            CreateQueue(ref msMq, @"LPCL7X64-077\private$\MSMQTest");

            if (msMq != null)
                msMq.Send(p);

            msMq.Formatter = new XmlMessageFormatter(new Type[] { typeof(Person) });

            var message = (Person)msMq.Receive().Body;

            Console.WriteLine("FirstName: " + message.FirstName + ", LastName: " + message.LastName);
            msMq.Close();
        }
        private static void CreateQueue(ref MessageQueue msMq, string queueName)
        {
            // check if queue already exists, if not create it
            if (!MessageQueue.Exists(queueName))
            {
                msMq = MessageQueue.Create(queueName);
            }
            else
            {
                msMq = new MessageQueue(queueName);
            }
        }
    }
}
