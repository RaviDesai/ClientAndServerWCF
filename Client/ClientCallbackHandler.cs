using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
	public class ClientCallbackHandler : IContractCallback
	{
		public void PingBack(string result)
		{
			Console.WriteLine(result);
		}
	}
}
