using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarMarketplaceServiceProject;
using System.ServiceModel;

namespace CarMarketplaceHost
{
    class Program
    {
        static void Main(string[] args)
        {
            CarMarketplaceAPI carMarketplace = new CarMarketplaceAPI();

            ServiceHost host = new ServiceHost(typeof(CarMarketplaceAPI));

            host.Opened += Host_Opened;
            Console.WriteLine("Starting...");
            host.Open();
            Console.ReadKey();
        }

        private static void Host_Opened(object sender, EventArgs e)
        {
            Console.WriteLine("CarMarketplace service is running...");     
        }
    }
}
