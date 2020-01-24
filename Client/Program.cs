using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
	class Program
	{
		static void Main(string[] args)
		{
			var instanceContext = new InstanceContext(new ClientCallbackHandler());
			var contractClient = new ContractClient(instanceContext);

			var name = string.Empty;
			var done = false;
			try
			{
				contractClient.Open();
				Console.WriteLine(contractClient.State.ToString());
				contractClient.Join("User");
				name = "User";
				Console.WriteLine(contractClient.State.ToString());
				Console.WriteLine("Connected as User");
			}
			catch(Exception e)
			{
				Console.WriteLine($"Exception while attempting to open connection and join as {name}: ", e.Message);
				Console.WriteLine(e.StackTrace);
				done = true;
			}

			while (!done)
			{
				var joinMsg = string.Empty;
				if (!string.IsNullOrEmpty(name))
				{
					joinMsg = $"[Joined as {name}]";
				}
				Console.WriteLine();
				Console.WriteLine($"Menu {joinMsg} Connection State: {contractClient.State.ToString()}");
				Console.WriteLine("     Enter 'x' to exit client");
				Console.WriteLine("     Enter 'j:<name>' to join");
				Console.WriteLine("     Enter 'l' to leave");
				Console.WriteLine("     Enter 'p:<message>' to ping the server with message");
				Console.WriteLine();
				Console.Write("Enter a command: ");
				var cmd = Console.ReadLine();
				cmd = cmd.ToLower().Trim();
				var splitCmd = cmd.Split(':');
				if (splitCmd[0] == "x")
				{
					done = true;
				} 
				else if (splitCmd[0] == "l")
				{
					try
					{
						Console.WriteLine(contractClient.State.ToString());
						contractClient.Leave();
						name = null;
					}
					catch (Exception e)
					{
						Console.WriteLine("Exception while attempting to leave: ", e.Message);
						Console.WriteLine(e.StackTrace);
					}
				} 
				else if (splitCmd[0] == "j")
				{
					var newname = (splitCmd.Length < 2 || string.IsNullOrEmpty(splitCmd[1])) ? null : splitCmd[1];
					if (! string.IsNullOrEmpty(newname))
					{
						try
						{
							Console.WriteLine(contractClient.State.ToString());
							contractClient.Join(newname);
							name = newname;
						}
						catch(Exception e)
						{
							Console.WriteLine($"Exception while attempting to join as {newname}: ", e.Message);
							Console.WriteLine(e.StackTrace);
						}
					}
					else
					{
						Console.WriteLine("Name should not be left empty on a join request");
					}
				}
				else if (splitCmd[0] == "p")
				{
					if (!string.IsNullOrEmpty(name)) {
						var msg = (splitCmd.Length < 2 || string.IsNullOrEmpty(splitCmd[1])) ? null : splitCmd[1];
						if (!string.IsNullOrEmpty(msg))
						{
							try
							{
								Console.WriteLine(contractClient.State.ToString());
								contractClient.Ping(msg);
							}
							catch (Exception e)
							{
								Console.WriteLine($"Exception attempting to ping with msg: {msg}: ", e.Message);
								Console.WriteLine(e.StackTrace);
							}
						}
						else
						{
							Console.WriteLine("Message cannot be left empty on a ping request");
						}
					}
					else
					{
						Console.WriteLine("Must join the server before sending a ping request");
					}
				} 
				else
				{
					Console.WriteLine("Unrecognized command");
				}
			}
			try
			{
				Console.WriteLine(contractClient.State.ToString());
				contractClient.Close();
			}
			catch (Exception e)
			{
				Console.WriteLine("Exception attempt to close out the client: ", e.Message);
				Console.WriteLine(e.StackTrace);
			}
		}
	}
}
