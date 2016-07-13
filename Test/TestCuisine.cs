using Xunit;
using System.Collections.Generic;
using System;
using System.Data;
using System.Data.SqlClient;

namespace BestRestaurants
{
  public class CuisineTest : IDisposable
  {
    public CuisineTest()
    {
      DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=best_restaurants_test;Integrated Security=SSPI;";
    }

    [Fact]
    public void Test_CuisineAsEmpty()
    {
      int result = Cuisine.GetAll().Count;

      Assert.Equal(0, result);
    }

    [Fact]
    public void Test_Equal_ForSameCuisineName()
    {
      Cuisine firstCuisine = new Cuisine("Burgers");
      Cuisine secondCuisine = new Cuisine("Burgers");
      Assert.Equal(firstCuisine, secondCuisine);
    }

    [Fact]
    public void Test_SaveCuisineName_SavesToDatabase()
    {
      Cuisine testCuisine = new Cuisine("Burgers");
      testCuisine.SaveCuisineName();

      List<Cuisine> testList = new List<Cuisine>{testCuisine};
      List<Cuisine> result = Cuisine.GetAll();
      //
      // System.Console.WriteLine("***TestList value is: " + testList);
      // System.Console.WriteLine("***Result value is: " + result);

      Assert.Equal(testList, result);
    }

    // [Fact]
    // public void Test_Equal_FindCuisineById()
    // {
    //   Cuisine testCuisine = new Cuisine("Burgers");
    //   int result = testCuisine.GetId();
    //   Assert.Equal(1, result);
    // }

    public void Dispose()
    {
      // Task.DeleteAll();
      Cuisine.DeleteAllCuisines();
    }
  }
}
