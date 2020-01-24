using System;
using System.ServiceModel;

namespace Contract
{
	[ServiceContract(Namespace = "http://careevolution.com/sample", ConfigurationName="Contract.IContract", SessionMode = SessionMode.Allowed, CallbackContract = typeof(ICallbackContract))]
	public interface IContract
	{
		[OperationContract]
		void Ping(string value);
		[OperationContract]
		void Join(string asName);
		[OperationContract]
		void Leave();
	}

	public interface ICallbackContract
	{
		[OperationContract(IsOneWay = true)]
		void PingBack(string result);
	}
}
