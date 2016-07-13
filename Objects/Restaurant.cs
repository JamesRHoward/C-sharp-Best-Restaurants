using System.Collections.Generic;
using System.Data.SqlClient;
using System;

namespace BestRestaurants
{
  public class Restaurant
  {
    private int _id;
    private string _restaurantName;
    private string _location;
    private string _description;
    private int _cuisineId;

    public Restaurant(string restaurantName, string location, string description, int cuisineId = 0, int id = 0)
    {
      _id = id;
      _restaurantName = restaurantName;
      _location = location;
      _description = description;
      _cuisineId = cuisineId;
    }

    public int GetId()
    {
      return _id;
    }

    public string GetRestaurantName()
    {
      return _restaurantName;
    }

    public string GetDescription()
    {
      return _description;
    }

    public int GetCuisine()
    {
      return _cuisineId;
    }

    public void SetRestaurant(string newRestaurantName)
    {
      _restaurantName = newRestaurantName;
    }

    public void SetLocation(string newLocation)
    {
      _location = newLocation;
    }

    public void SetDescription(string newDescription)
    {
      _description = newDescription;
    }

    public void SetCuisineId(int newCuisineId)
    {
      _cuisineId = newCuisineId;
    }

    public static List<Restaurant> GetAll()
    {
    }
  }
}
