using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace CarMarketplaceServiceProject
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in both code and config file together.
    public class CarMarketplaceAPI : ICarMarketplaceAPI
    {
        List<Car> cars = new List<Car>();

        public CarMarketplaceAPI()
        {
            string line;

            System.IO.StreamReader file =
                new System.IO.StreamReader("C:\\Users\\tzubr\\Downloads\\MOCK_DATA.csv");
            while ((line = file.ReadLine()) != null)
            {
                string[] words = line.Split(',');
                cars.Add(new Car
                {
                    Id = Convert.ToInt32(words[0]),
                    Make = words[1],
                    Model = words[2],
                    EngineSize = words[3],
                    ProductionYear = Convert.ToInt32(words[4]),
                    Price = Convert.ToInt32(words[5])
                });
            }
            
            file.Close();

        }
           
        public List<Car> GetAllCars()
        {
            return cars;
        }

        public List<Car> GetCars(string make, string model, int minEngineSize, int maxEngineSize, int minProductionYear, int maxProductionYear, int minPrice, int maxPrice)
        {
            List<Car> carsList = new List<Car>();
            
            List<Car> CarsByMake = GetCarsByMake(make);
            List<Car> CarsByModel = GetCarsByModel(model);
            List<Car> CarsByProductionYear = GetCarsByProductionYear(minProductionYear, maxProductionYear);
            List<Car> CarsByEngineSize = GetCarsByEngineSize(minEngineSize, maxEngineSize);
            List<Car> CarsByPrice = GetCarsByPrice(minPrice, maxPrice);

            carsList.AddRange(cars.FindAll(
                c => CarsByMake.Contains(c) 
                && CarsByModel.Contains(c) 
                && CarsByProductionYear.Contains(c)
                && CarsByEngineSize.Contains(c)
                && CarsByPrice.Contains(c)));

            return carsList;

        }

        public List<Car> AddCars(string make, string model, string engineSize, int productionYear, int price)
        {
            string car = "empty";
            
            cars.Add(new Car { Id = GetNextId(cars), Make = make, Model = model, EngineSize = engineSize, ProductionYear = productionYear, Price = price });
            car = cars.Last().ToString();
                
            using (StreamWriter writer = new StreamWriter("C:\\Users\\tzubr\\Downloads\\MOCK_DATA.csv",append:true))
            {
                writer.WriteLine(car);
            }

            return cars;
        }




        private int GetNextId(List<Car> carsList)
        {
            return carsList.Count() + 1;
        }

        private List<Car> Filtered(List<Car> toRemove)
        {
            List<Car> Result = new List<Car>();
            Result.AddRange(cars);
            Result.RemoveAll(c => toRemove.Contains(c));
            
            return Result;
            
        }

        private List<Car> GetCarsByMake(string make)
        {
            if (make != null && make.Length>1)
                return Filtered(cars.Where(c => c.Make != make).ToList());

            else return cars;
        }
        
        private List<Car> GetCarsByModel(string model)
        {
            if (model != null && model.Length>1)
                return Filtered(cars.Where(c => c.Model != model).ToList());

            else return cars;


        }
        
        private List<Car> GetCarsByProductionYear(int MinProductionYear, int MaxProductionYear)
        {
            List<Car> ToFilter = new List<Car>(); 
            if (MinProductionYear != 0 || MaxProductionYear != 0)
            {
                if (MinProductionYear == 0 && MaxProductionYear > 0)
                {
                    ToFilter.AddRange(cars.Where(c => c.ProductionYear > MaxProductionYear).ToList());
                }
                else if (MaxProductionYear == 0 && MinProductionYear > 0)
                {
                    ToFilter.AddRange(cars.Where(c => c.ProductionYear < MinProductionYear).ToList());
                }
                else
                {
                    ToFilter.AddRange(cars.Where(c => c.ProductionYear < MinProductionYear || c.ProductionYear > MaxProductionYear).ToList());
                }
            }
            
            return Filtered(ToFilter);
        }

        private List<Car> GetCarsByEngineSize(int MinEngineSize, int MaxEngineSize)
        {
            List<Car> ToFilter = new List<Car>();
            if(MinEngineSize != 0 || MaxEngineSize != 0)
            {
                if (MinEngineSize == 0 && MaxEngineSize > 0)
                {
                    ToFilter.AddRange(cars.Where(c => c.engineSize > MaxEngineSize).ToList());
                }
                else if (MaxEngineSize == 0 && MinEngineSize > 0)
                {
                    ToFilter.AddRange(cars.Where(c => c.engineSize < MinEngineSize).ToList());
                }
                else
                {
                    ToFilter.AddRange(cars.Where(c => c.engineSize < MinEngineSize || c.engineSize > MaxEngineSize).ToList());
                }
            }
            
            return Filtered(ToFilter);




        }

        private List<Car> GetCarsByPrice(int MinPrice, int MaxPrice)
        {
            List<Car> ToFilter = new List<Car>();
            if(MinPrice != 0 || MaxPrice != 0)
            {
                if (MinPrice == 0 && MaxPrice > 0)
                {
                    ToFilter.AddRange(cars.Where(c => c.Price > MaxPrice).ToList());
                }
                else if (MaxPrice == 0 && MinPrice > 0)
                {
                    ToFilter.AddRange(cars.Where(c => c.Price < MinPrice).ToList());
                }
                else
                {
                    ToFilter.AddRange(cars.Where(c => c.Price < MinPrice || c.Price > MaxPrice).ToList());
                }
            }
            
            return Filtered(ToFilter);
        }

        
    }


}
