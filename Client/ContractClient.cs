using Contract;

[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
public partial class ContractClient : System.ServiceModel.DuplexClientBase<IContract>, IContract
{

    public ContractClient(System.ServiceModel.InstanceContext callbackInstance) :
            base(callbackInstance)
    {
    }

    public ContractClient(System.ServiceModel.InstanceContext callbackInstance, string endpointConfigurationName) :
            base(callbackInstance, endpointConfigurationName)
    {
    }

    public ContractClient(System.ServiceModel.InstanceContext callbackInstance, string endpointConfigurationName, string remoteAddress) :
            base(callbackInstance, endpointConfigurationName, remoteAddress)
    {
    }

    public ContractClient(System.ServiceModel.InstanceContext callbackInstance, string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) :
            base(callbackInstance, endpointConfigurationName, remoteAddress)
    {
    }

    public ContractClient(System.ServiceModel.InstanceContext callbackInstance, System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) :
            base(callbackInstance, binding, remoteAddress)
    {
    }

    public void Ping(string value)
    {
        base.Channel.Ping(value);
    }

    public void Join(string asName)
    {
        base.Channel.Join(asName);
    }

    public void Leave()
    {
        base.Channel.Leave();
    }
}
