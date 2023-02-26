using System;
using LiteNetLib;
using System.Threading;

namespace Exploration_RPG.server
{
	public class Connection
	{
		public void connect()
		{
            EventBasedNetListener listener = new EventBasedNetListener();
            NetManager client = new NetManager(listener);
            client.Start();
            client.Connect(
                "localhost" /* host ip or name */,
                9050 /* port */,
                "SomeConnectionKey" /* text key or NetDataWriter */
                );
            listener.NetworkReceiveEvent += (fromPeer, dataReader, deliveryMethod) =>
            {
                Console.WriteLine("We got: {0}", dataReader.GetString(100));
                dataReader.Recycle();
            };

            while (!Console.KeyAvailable)
            {
                client.PollEvents();
                Thread.Sleep(15);
            }

            client.Stop();
        }
	}
}

