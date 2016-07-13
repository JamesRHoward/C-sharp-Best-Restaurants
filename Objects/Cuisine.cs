using System.Collections.Generic;
using System.Data.SqlClient;
using System;

namespace BestRestaurants
{
  public class Cuisine
  {
    private int _id;
    private string _cuisineName;

    public Cuisine(string cuisineName, int id = 0)
    {
      _id = id;
      _cuisineName = cuisineName;
    }
    public string GetCuisineName()
    {
      return _cuisineName;
    }
    public int GetId()
    {
      return _id;
    }
    public void SetCuisineName(string newCuisineName)
    {
      _cuisineName = newCuisineName;
    }
    public List<Cuisine> GetCuisine()
    {
      
    }

  }
}
