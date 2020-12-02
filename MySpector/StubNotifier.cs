using System;

namespace MySpector
{
    public class StubNotifier: INotifier
    {
        public void Notify(string message)
        {
            Console.WriteLine("Notification: "+message);
        }
    }

}
