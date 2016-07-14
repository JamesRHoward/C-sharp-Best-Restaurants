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

    public override bool Equals(System.Object otherCuisine)
    {
      if (!(otherCuisine is Cuisine))
      {
        return false;
      }
      else
      {
        Cuisine newCuisine = (Cuisine) otherCuisine;
        bool idEquality = this.GetId() == newCuisine.GetId();
        bool cuisineNameEquality = this.GetCuisineName() == newCuisine.GetCuisineName();
        return (idEquality && cuisineNameEquality);
      }
    }

    public static Cuisine Find(int id)
    {
      SqlConnection conn = DB.Connection();
      SqlDataReader rdr = null;
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM cuisines WHERE id = @cuisineId;", conn);

      SqlParameter cuisineIdParameter = new SqlParameter();
      cuisineIdParameter.ParameterName = "@CuisineId";
      cuisineIdParameter.Value = id.ToString();

      cmd.Parameters.Add(cuisineIdParameter);
      rdr = cmd.ExecuteReader();

      int foundCuisineId = 0;
      string foundCuisineName = null;

      while(rdr.Read())
      {
        foundCuisineId = rdr.GetInt32(1);
        foundCuisineName = rdr.GetString(0);
      }
      Cuisine foundCuisine = new Cuisine(foundCuisineName, foundCuisineId);

      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }
      return foundCuisine;
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
    public static List<Cuisine> GetAll()
    {
      List<Cuisine> allCuisines = new List<Cuisine> {};

      SqlConnection conn = DB.Connection();
      SqlDataReader rdr = null;
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM cuisines;", conn);
      rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        string cuisineName = rdr.GetString(0);
        int cuisineId = rdr.GetInt32(1);
        Cuisine newCuisine = new Cuisine(cuisineName, cuisineId);
        allCuisines.Add(newCuisine);
      }
      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }
      return allCuisines;
    }

    public void SaveCuisineName()
    {
      SqlConnection conn = DB.Connection();
      SqlDataReader rdr;
      conn.Open();

      SqlCommand cmd = new SqlCommand("INSERT INTO cuisines (cuisine) OUTPUT INSERTED.id VALUES (@CuisineName);", conn);
      SqlParameter cuisineParameter = new SqlParameter();
      cuisineParameter.ParameterName = "@CuisineName";
      cuisineParameter.Value = this.GetCuisineName();
      cmd.Parameters.Add(cuisineParameter);
      rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        this._id = rdr.GetInt32(0);
      }
      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }
    }

    public void Update(string newCuisine)
    {
      SqlConnection conn = DB.Connection();
      SqlDataReader rdr;
      conn.Open();

      SqlCommand cmd = new SqlCommand("UPDATE cuisines SET cuisine = @NewCuisine OUTPUT INSERTED.cuisine WHERE id = @CuisineId;", conn);

      SqlParameter newCuisineParameter = new SqlParameter();
      newCuisineParameter.ParameterName = "@NewCuisine";
      newCuisineParameter.Value = newCuisine;
      cmd.Parameters.Add(newCuisineParameter);

      SqlParameter categoryIdParameter = new SqlParameter();
      categoryIdParameter.ParameterName = "@CuisineId";
      categoryIdParameter.Value = this.GetId();
      cmd.Parameters.Add(categoryIdParameter);
      rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        this._cuisineName = rdr.GetString(0);
      }

      if (rdr != null)
      {
        rdr.Close();
      }

      if (conn != null)
      {
        conn.Close();
      }
    }

    public List<Restaurant> GetRestaurant()
    {
      SqlConnection conn = DB.Connection();
      SqlDataReader rdr = null;
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM restaurants WHERE cuisine_id = @CuisineId;", conn);
      SqlParameter cuisineIdParameter = new SqlParameter();
      cuisineIdParameter.ParameterName = "@CuisineId";
      cuisineIdParameter.Value = this.GetId();
      cmd.Parameters.Add(cuisineIdParameter);
      rdr = cmd.ExecuteReader();

      List<Restaurant> restaurants = new List<Restaurant> {};
      while(rdr.Read())
      {
        int foundRestaurantId = rdr.GetInt32(0);
        string foundRestaurantName = rdr.GetString(1);
        string foundRestaurantLocation = rdr.GetString(2);
        string foundRestaurantDescription = rdr.GetString(3);
        Restaurant newRestaurant = new Restaurant(foundRestaurantName, foundRestaurantLocation, foundRestaurantDescription, this.GetId(), foundRestaurantId);
        restaurants.Add(newRestaurant);
        System.Console.WriteLine("Name: " + foundRestaurantName);
      }
      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }
      return restaurants;
    }
    public static void DeleteAllCuisines()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();
      SqlCommand cmd = new SqlCommand("DELETE FROM cuisines;", conn);
      cmd.ExecuteNonQuery();
    }
  }
}
