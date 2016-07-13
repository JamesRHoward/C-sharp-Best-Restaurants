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

    [Fact]
    public void Test_Update_UpdatesRestaurantInDatabase()
    {
      //Arrange
      string name = "Dirty Steve's";
      Restaurant TestRestaurant = new Restaurant(name, "123 main str", "blah", 1);
      TestRestaurant.Save();
      string newName = "Filthy Steve's";

      //Act
      TestRestaurant.Update(newName);

      string result = TestRestaurant.GetRestaurantName();

      //Assert
      Assert.Equal(newName, result);
    }

    [Fact]
    public void Test_Find_FindsRestaurantInDatabase()
    {
      Restaurant newRestaurant = new Restaurant("Dirty Steve's", "123 Main St", "It's pretty dirty!", 4);
      newRestaurant.Save();
      Restaurant foundRestaurant = Restaurant.Find(newRestaurant.GetId());
      Assert.Equal(newRestaurant, foundRestaurant);
    }


    public void Dispose()
    {
      Restaurant.DeleteAll();
      Cuisine.DeleteAllCuisines();
    }
  }
}
