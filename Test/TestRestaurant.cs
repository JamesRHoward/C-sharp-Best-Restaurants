using Xunit;
using System.Collections.Generic;
using System;
using System.Data;
using System.Data.SqlClient;

namespace BestRestaurants
{
  public class TestRestaurant : IDisposable
  {
    public TestRestaurant()
    {
      DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=best_restaurants_test;Integrated Security=SSPI;";
    }

    [Fact]
    public void Test_RestaurantsAsEmpty()
    {
      int result = Restaurant.GetAll().Count;

      Assert.Equal(0, result);
    }

    [Fact]
    public void Test_SaveRestaurantToDatabase()
    {
      Restaurant newRestaurant = new Restaurant("Dirty Steve's", "123 Main St", "It's prettt dirty!", 4);
      newRestaurant.Save();

      List<Restaurant> testList = new List<Restaurant>{newRestaurant};
      List<Restaurant> result = Restaurant.GetAll();

      Assert.Equal(testList, result);
    }


    public void Dispose()
    {
      Restaurant.DeleteAll();
      Cuisine.DeleteAllCuisines();
    }
  }
}
