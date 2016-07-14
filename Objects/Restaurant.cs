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

    public override bool Equals(System.Object otherRestaurant)
    {
      if (!(otherRestaurant is Restaurant))
      {
        return false;
      }
      else
      {
        Restaurant newRestaurant = (Restaurant) otherRestaurant;
        bool idEquality = this.GetId() == newRestaurant.GetId();
        bool nameEquality = this.GetRestaurantName() == newRestaurant.GetRestaurantName();
        bool locationEquality = this.GetLocation() == newRestaurant.GetLocation();
        bool descriptionEquality = this.GetDescription() == newRestaurant.GetDescription();
        bool cuisineIdEquality = this.GetCuisineId() == newRestaurant.GetCuisineId();
        return (idEquality && nameEquality && locationEquality && descriptionEquality && cuisineIdEquality);
      }
    }

    public int GetId()
    {
      return _id;
    }

    public string GetRestaurantName()
    {
      return _restaurantName;
    }
    public string GetLocation()
    {
      return _location;
    }

    public string GetDescription()
    {
      return _description;
    }

    public int GetCuisineId()
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
      List<Restaurant> allRestaurants = new List<Restaurant>{};
      SqlConnection conn = DB.Connection();
      SqlDataReader rdr;
      conn.Open();

      SqlCommand cmd = new SqlCommand ("SELECT * FROM restaurants;", conn);
      rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        int restaurantId = rdr.GetInt32(0);
        string restaurantName = rdr.GetString(1);
        string location = rdr.GetString(2);
        string description = rdr.GetString(3);
        int cuisineId = rdr.GetInt32(4);
        Restaurant newRestaurant = new Restaurant (restaurantName, location, description, cuisineId, restaurantId);
        allRestaurants.Add(newRestaurant);
      }
      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }
      return allRestaurants;
    }
    public void Save()
    {
      SqlConnection conn = DB.Connection();
      SqlDataReader rdr;
      conn.Open();

      SqlCommand cmd = new SqlCommand ("INSERT INTO restaurants (restaurant, location, description, cuisine_id) OUTPUT INSERTED.id VALUES (@restaurantRestaurant, @restaurantLocation, @restaurantDescription, @restaurantCuisineId);", conn);

      SqlParameter restaurantNameParameter = new SqlParameter();
      restaurantNameParameter.ParameterName = "@restaurantRestaurant";
      restaurantNameParameter.Value = this.GetRestaurantName();

      SqlParameter locationParameter = new SqlParameter();
      locationParameter.ParameterName = "@restaurantLocation";
      locationParameter.Value = this.GetLocation();

      SqlParameter descriptionParameter = new SqlParameter();
      descriptionParameter.ParameterName = "@restaurantDescription";
      descriptionParameter.Value = this.GetDescription();

      SqlParameter cuisineIdParameter = new SqlParameter();
      cuisineIdParameter.ParameterName = "@restaurantCuisineId";
      cuisineIdParameter.Value = this.GetCuisineId();

      cmd.Parameters.Add(restaurantNameParameter);
      cmd.Parameters.Add(locationParameter);
      cmd.Parameters.Add(descriptionParameter);
      cmd.Parameters.Add(cuisineIdParameter);

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

    public static Restaurant Find(int id)
    {
      SqlConnection conn = DB.Connection();
      SqlDataReader rdr = null;
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM restaurants WHERE id = @RestaurantId;", conn);

      SqlParameter restaurantIdParameter = new SqlParameter();
      restaurantIdParameter.ParameterName = "@RestaurantId";
      restaurantIdParameter.Value = id.ToString();

      cmd.Parameters.Add(restaurantIdParameter);
      rdr = cmd.ExecuteReader();

      int foundRestaurantId = 0;
      string foundRestaurantName = null;
      string foundRestaurantLocation = null;
      string foundRestaurantDescription = null;
      int foundRestaurantCuisineId = 0;

      while(rdr.Read())
      {
        foundRestaurantId = rdr.GetInt32(0);
        foundRestaurantName = rdr.GetString(1);
        foundRestaurantLocation = rdr.GetString(2);
        foundRestaurantDescription = rdr.GetString(3);
        foundRestaurantCuisineId = rdr.GetInt32(4);
      }
      Restaurant foundRestaurant = new Restaurant(foundRestaurantName, foundRestaurantLocation, foundRestaurantDescription, foundRestaurantCuisineId, foundRestaurantId);

      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }
      return foundRestaurant;
    }

    public void Delete()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("DELETE FROM restaurants WHERE id = @RestaurantId", conn);

      SqlParameter restaurantIdParameter = new SqlParameter();
      restaurantIdParameter.ParameterName = "@RestaurantId";
      restaurantIdParameter.Value = this.GetId();

      cmd.Parameters.Add(restaurantIdParameter);
      cmd.ExecuteNonQuery();

      if (conn != null)
      {
        conn.Close();
      }
    }

    public static void DeleteAll()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();
      SqlCommand cmd = new SqlCommand ("DELETE FROM restaurants;", conn);
      cmd.ExecuteNonQuery();
    }

    public void Update(string newRestaurantName, string newLocation, string newDescription, int newCuisineId)
    {
      SqlConnection conn = DB.Connection();
      SqlDataReader rdr;
      conn.Open();

      SqlCommand cmd = new SqlCommand("UPDATE restaurants SET restaurant = @newRestaurantName, location = @newLocation, description = @newDescription, cuisine_id = @newCuisineId OUTPUT INSERTED.restaurant, INSERTED.location, INSERTED.description, INSERTED.cuisine_id WHERE id = @restaurantId;", conn);

      SqlParameter newRestaurantNameParameter = new SqlParameter();
      newRestaurantNameParameter.ParameterName = "@newRestaurantName";
      newRestaurantNameParameter.Value = newRestaurantName;

      SqlParameter newLocationParameter = new SqlParameter();
      newLocationParameter.ParameterName = "@newLocation";
      newLocationParameter.Value = newLocation;

      SqlParameter newDescriptionParameter = new SqlParameter();
      newDescriptionParameter.ParameterName = "@newDescription";
      newDescriptionParameter.Value = newDescription;

      SqlParameter newCuisineIdParameter = new SqlParameter();
      newCuisineIdParameter.ParameterName = "@newCuisineId";
      newCuisineIdParameter.Value = newCuisineId;

      SqlParameter restaurantIdParameter = new SqlParameter();
      restaurantIdParameter.ParameterName = "@restaurantId";
      restaurantIdParameter.Value = this.GetId();

      cmd.Parameters.Add(newRestaurantNameParameter);
      cmd.Parameters.Add(newLocationParameter);
      cmd.Parameters.Add(newDescriptionParameter);
      cmd.Parameters.Add(newCuisineIdParameter);
      cmd.Parameters.Add(restaurantIdParameter);

      rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        this._restaurantName = rdr.GetString(0);
        this._location = rdr.GetString(1);
        this._description = rdr.GetString(2);
        this._cuisineId = rdr.GetInt32(3);
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
  }
}
