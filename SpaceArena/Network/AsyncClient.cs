using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Text;

namespace SpaceOnLine
{
	public static class AsyncClient
	{
		private const int port = 11000;
        private static Socket client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        private static byte[] buffer = new byte[256];

	    private static void Connect() {
	        try {
	            client.BeginConnect("127.0.0.1", 11000, ConnectCallback, client);
	        }
	        catch (Exception e) {
	            Console.WriteLine(e.Message);

	        }

	        Console.WriteLine("Connected");
	    }

	    public static void StartClient()
		{
			Connect();
    		string text = String.Empty;
            try { 	
                /*
				while (true) {
					text = Console.ReadLine();
					Send(client, text);
				}
                */
			} catch (Exception e) {
				Console.WriteLine(e.ToString());
				client.Shutdown(SocketShutdown.Both);
				client.Close();
				Console.WriteLine("Disconnected...");
			}
		}

		private static void ConnectCallback(IAsyncResult ar)
		{
			try {
				Socket socket = (Socket)ar.AsyncState;
			    socket.EndConnect(ar);

				Console.WriteLine("Socket connected to {0}",socket.RemoteEndPoint);
			    socket.BeginReceive(buffer, 0, buffer.Length, 0, ReceiveCallback, socket);
			} catch (Exception e) {
				Console.WriteLine(e.ToString());
			}
		}

		private static void ReceiveCallback(IAsyncResult AR)
		{
			String content = String.Empty;
			Socket socket = (Socket)AR.AsyncState;

			try {
				int received = socket.EndReceive(AR);
			

				if (received > 0) {
					// There  might be more data, so store the data received so far.
					content = Encoding.ASCII.GetString(buffer, 0, received);

				    Console.WriteLine("Read {0} bytes from socket. \n Data : {1}",	content.Length, content);
				    socket.BeginReceive(buffer, 0, buffer.Length, 0, ReceiveCallback, socket);
						
				}
			} catch (Exception) {
				Console.WriteLine("Disconnected...");
			    socket.Shutdown(SocketShutdown.Both);
			    socket.Close();
			}
		}

		private static void Send(Socket client, String data)
		{
			byte[] byteData = Encoding.ASCII.GetBytes(data);
			client.BeginSend(byteData, 0, byteData.Length, 0, SendCallback, client);
		}

	    public static void SendPlayerData() {
            Send(client, "myData: 1");
	    }

		private static void SendCallback(IAsyncResult AR)
		{
			try {
				Socket client = (Socket)AR.AsyncState;
				int bytesSent = client.EndSend(AR);
				Console.WriteLine("Sent {0} bytes to server.", bytesSent);
			} catch (Exception e) {
				Console.WriteLine(e.ToString());
			}
		}
    
	
	}
}