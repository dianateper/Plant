using System.ServiceModel;

namespace app.seed
{
    [ServiceContract]
    public interface IContractXam
    {
        [OperationContract]
        string Greeting();
    }

}
