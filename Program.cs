using EfCoreTutorial.Database;
using EfCoreTutorial.DesignPatterns;
using EfCoreTutorial.Dtos;
using EfCoreTutorial.Entity.ECommerceModels;
using EfCoreTutorial.Entity.SchoolModels;
using EfCoreTutorial.Helpers;
using EfCoreTutorial.Modules;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text.Json;
using System.Threading.Tasks;
using static EfCoreTutorial.Entity.Enums;

class Program
{
    private static async Task Main(string[] args)
    {
        Console.WriteLine("Hello, Wazi!");

        #region Ecommerce Functions

        EcommerceModule ecommerceModule = new EcommerceModule();

        //InsertFirstUser();
        //PopulateProductCategories();
        //PopulateSellers();
        //PopulateProducts();
        //PopulateUserRoles();
        //PopulateCustomers();

        //int maxOrders = 5000;

        //for (int i = 1; i <= maxOrders; i++)
        //{
        //    AddOrders(DateTime.Now);

        //    if (i % 10 == 0)
        //    {
        //        Console.WriteLine($"{i} order(s) have been added");
        //    }

        //}

        //Console.WriteLine("Done adding orders");

        //UpdateNetAmountOfOrders();

        //Console.WriteLine($"Updated all {maxOrders} order totals.");

        //GetOrderInformation();

        //GetAllPaginatedOrders(2, 3158);

        //await UpdateOrderDatesAndCounters();

        //LoadCustomerOrderExpense();
        //GroupJoiningLinqQuery();

        //Random r = new Random();
        //AddCustomerTransaction(r);

        //await ecommerceModule.ProcessDailyTransactions();
        #endregion

        #region Singleton Design Pattern Example
        SingletonCounter singletonCounter = SingletonCounter.GetSingletonCounterInstance();
        singletonCounter.PrintDetails("From Teacher");

        SingletonCounter studentCounter = SingletonCounter.GetSingletonCounterInstance();
        studentCounter.PrintDetails("From Student");

        //Console.ReadLine();
        #endregion

        #region Parallel Functions
        Parallel.Invoke(
            () => ParallelFunction1(),
            () => ParallelFunction2()
        );
        #endregion

        Console.WriteLine("All Tasks completed");
    }

    private static void ParallelFunction1()
    {
        Console.WriteLine("Fucntion numero uno");
    }
    private static void ParallelFunction2()
    {
        Console.WriteLine("Function numero dos");
    }

}








