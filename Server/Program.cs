using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
	class Program
	{
		static void Main(string[] args)
		{
			using (var serviceHost = new ServiceHost(typeof(Service)))
			{
				serviceHost.Open();
				Console.WriteLine("Press Enter to stop server");
				Console.ReadLine();
			}
		}
	}
}
