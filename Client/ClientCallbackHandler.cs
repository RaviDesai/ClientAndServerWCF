using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contract;

namespace Client
{
	public class ClientCallbackHandler : ICallbackContract
	{
		public void PingBack(string result)
		{
			Console.WriteLine(result);
		}
	}
}
