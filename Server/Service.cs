using Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
	[ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
	public class Service : IContract
	{
		List<Tuple<ICallbackContract, string>> _callbacks = new List<Tuple<ICallbackContract, string>>();
		public Service()
		{
			Console.WriteLine("Service Initializing");
		}

		public void Join(string asName)
		{
			Console.WriteLine($"Join Request from {asName}");
			try
			{
				var registeredUser = OperationContext.Current.GetCallbackChannel<ICallbackContract>();
				var foundCallback = _callbacks.FirstOrDefault(c => c.Item1 == registeredUser);
				if (foundCallback == null)
				{
					foundCallback = new Tuple<ICallbackContract, string>(registeredUser, asName);
					_callbacks.Add(foundCallback);
					SendMessageToAll($"{asName} just joined");
				}
			}
			catch (Exception e)
			{
				Console.WriteLine($"Exception trying to get registered user from callback channel: {e.Message}");
				Console.WriteLine(e.StackTrace);
			}
			Console.WriteLine($"total callbacks: {_callbacks.Count}");
		}

		public void Leave()
		{
			var registeredUser = OperationContext.Current.GetCallbackChannel<ICallbackContract>();
			_callbacks = _callbacks.Where(c => c.Item1 != registeredUser).ToList();
			Console.WriteLine($"someone left.  total callbacks: {_callbacks.Count}");
		}

		public void Ping(string value)
		{
			var user = "<Unknown>";
			var registeredUser = OperationContext.Current.GetCallbackChannel<ICallbackContract>();
			var foundCallback = _callbacks.FirstOrDefault(c => c.Item1 == registeredUser);
			if (foundCallback != null)
			{
				user = foundCallback.Item2;
			}

			var msg = $"From {user} Received: {value}";
			Console.WriteLine(msg);
			SendMessageToAll(msg);
		}

		private void SendMessageToAll(string msg)
		{
			_callbacks.ForEach(callback =>
			{
				try
				{
					var commObj = callback.Item1 as ICommunicationObject;
					if (commObj != null && commObj.State != CommunicationState.Faulted && commObj.State != CommunicationState.Closed)
					{
						Console.WriteLine($"Sending to {callback.Item2} State [{commObj.State.ToString()}] : {msg}");
						callback.Item1.PingBack(msg);
					}
				}
				catch(Exception e)
				{
					Console.WriteLine($"Exception attempting to write to user {callback.Item2}: {e.Message}");
					Console.WriteLine(e.StackTrace);
				}
			});
		}
	}
}
