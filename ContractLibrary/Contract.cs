using System;
using System.ServiceModel;

namespace Contract
{
	[ServiceContract(Namespace = "http://careevolution.com/sample", SessionMode = SessionMode.Allowed, CallbackContract = typeof(ICallbackContract))]
	public interface IContract
	{
		[OperationContract(IsOneWay = true)]
		void Ping(string value);
		[OperationContract(IsOneWay = true)]
		void Join(string asName);
		[OperationContract(IsOneWay = true)]
		void Leave();
	}

	public interface ICallbackContract
	{
		[OperationContract(IsOneWay = true)]
		void PingBack(string result);
	}
}
