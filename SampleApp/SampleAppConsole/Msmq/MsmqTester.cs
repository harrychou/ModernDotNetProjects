using System;
using System.Messaging;

namespace SampleAppConsole.Msmq
{
    public class MsmqTester
    {
        public void SendMessage(MsmqMessage msmqMessage)
        {
            var msg = new Message {Body = msmqMessage};

            var msgQ = new MessageQueue(".\\Private$\\billpay");
            msgQ.Send(msg);
        }

        public MsmqMessage ReceiveMessage()
        {
            var msgQ = new MessageQueue(".\\Private$\\billpay");

            MsmqMessage myPayment = new MsmqMessage();
            Object o = new Object();
            System.Type[] arrTypes = new System.Type[2];
            arrTypes[0] = myPayment.GetType();
            arrTypes[1] = o.GetType();
            msgQ.Formatter = new XmlMessageFormatter(arrTypes);

            var receive = msgQ.Receive();
            if (receive != null) return (MsmqMessage) receive.Body;
            return null;
        }
    }
}