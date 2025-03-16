using HotelAdminApp.Data;

namespace HotelAdminApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Creating an instance of ApplicationDbContext using DataInitializer.Build() method
            using (var dbContext = DataInitializer.Build())
            {
                DataInitializer.SeedData(dbContext);    

            }
        }
    }
}
