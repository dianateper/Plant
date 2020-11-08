﻿using Server.Model;
using Server.Repository;
using System.Collections.Generic;
using System.ServiceModel;

namespace Server.Contracts
{
    [ServiceContract]
    interface IContractWeb
    {

        [OperationContract]
        string Greeting();

        [OperationContract]
        [FaultContract(typeof(FaultException))]
        [ServiceKnownType(typeof(Plant))]
        List<Plant> GetListPlants();

        [OperationContract]
        [ServiceKnownType(typeof(Plant))]
        [FaultContract(typeof(FaultException))]
        void SetPlants(List<Plant> plants);

        [OperationContract]
        [ServiceKnownType(typeof(Plant))]
        [FaultContract(typeof(FaultException))]
        List<Plant> GetPlantedSeeds();

    }
}
