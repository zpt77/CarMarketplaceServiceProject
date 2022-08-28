using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Xml.Serialization;

namespace CarMarketplaceServiceProject
{
    [ServiceContract]
    public interface ICarMarketplaceAPI
    {
        [OperationContract]
        List<Car> GetAllCars();

        [OperationContract]
        List<Car> GetCars(string make, string model, int minEngineSize, int maxEngineSize, int minProductionYear, int maxProductionYear, int minPrice, int maxPrice);

        [OperationContract]
        List<Car> AddCars(string make, string model, string engineSize, int productionYear, int price);
    }

    [DataContract]
    public class Car
    {
        public int Id { get; set; }

        public int engineSize;

        [DataMember]
        public string Make { get; set; }

        [DataMember]
        public string Model { get; set; }

        [DataMember]
        public int Price { get; set; }

        [DataMember]
        public int ProductionYear { get; set; }

        [DataMember]
        [XmlElement(DataType="string")]
        public string EngineSize 
        { 
            get { return engineSize.ToString().Insert(1,"."); }
            set { engineSize = Convert.ToInt32(value.Remove(1,1)); }
        }

        public override string ToString()
        {
            return String.Format("{0},{1},{2},{3},{4},{5}", this.Id, this.Make, this.Model, this.EngineSize, this.ProductionYear, this.Price);
        }
    }
}
