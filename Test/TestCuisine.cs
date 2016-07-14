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
    public void Test_Save_SavesToDatabase()
    {
      Cuisine testCuisine = new Cuisine("Burgers");
      testCuisine.Save();

      List<Cuisine> testList = new List<Cuisine>{testCuisine};
      List<Cuisine> result = Cuisine.GetAll();
      //
      // System.Console.WriteLine("***TestList value is: " + testList);
      // System.Console.WriteLine("***Result value is: " + result);

      Assert.Equal(testList, result);
    }
    [Fact]
    public void Test_Update_UpdatesCuisineInDatabase()
    {
      //Arrange
      string cuisineName = "Burgurs";
      Cuisine testCuisine = new Cuisine(cuisineName);
      testCuisine.Save();
      string newCuisineName = "Burgers";

      //Act
      testCuisine.Update(newCuisineName);

      string result = testCuisine.GetCuisineName();

      //Assert
      Assert.Equal(newCuisineName, result);
    }
    [Fact]
    public void Test_Delete_RemovesCuisineFromDatabase()
    {
      List<Cuisine> TestCuisines = new List<Cuisine>{};

      Cuisine testCuisine1 = new Cuisine("Greek");
      testCuisine1.Save();
      Cuisine testCuisine2 = new Cuisine("Japanese");
      testCuisine2.Save();

      Restaurant TestRestaurant1 = new Restaurant("Fred's", "123 Main St", "Delicious!", testCuisine1.GetId());
      TestRestaurant1.Save();
      Restaurant TestRestaurant2 = new Restaurant("Bill's", "123 Main St", "Delicious!", testCuisine2.GetId());
      TestRestaurant2.Save();

      testCuisine1.Delete();

      List<Cuisine> resultCuisines = Cuisine.GetAll();
      List<Cuisine> testCuisines = new List<Cuisine> {testCuisine2};

      List<Restaurant> resultRestaurants = Restaurant.GetAll();
      List<Restaurant> testRestaurants = new List<Restaurant> {TestRestaurant2};

      Assert.Equal(resultCuisines, testCuisines);
      Assert.Equal(resultRestaurants, testRestaurants);
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
