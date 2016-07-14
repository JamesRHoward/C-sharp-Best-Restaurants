using System.Collections.Generic;
using Nancy;
using Nancy.ViewEngines.Razor;

namespace BestRestaurants
{
  public class HomeModule : NancyModule
  {
    public HomeModule()
    {
      Get["/"] = _ => {
        List<Cuisine> allCuisines = Cuisine.GetAll();
        return View["index.cshtml", allCuisines];
      };
      Get["/cuisine/new"] = _ => {
        return View["cuisine_form.cshtml"];
      };
      Post["/cuisine/new"] = _ => {
        Cuisine newCuisine = new Cuisine(Request.Form["cuisine_name"]);
        newCuisine.SaveCuisineName();
        List<Cuisine> allCuisines = Cuisine.GetAll();
        return View["index.cshtml", allCuisines];
      };
      Get["/restaurant/new"] = _ => {
        List<Cuisine> allCuisines = Cuisine.GetAll();
        return View["restaurant_form.cshtml", allCuisines];
      };
      Post["/restaurant/new"] = _ => {
        Restaurant newRestaurant = new Restaurant(Request.Form["restaurant_name"], Request.Form["restaurant_location"], Request.Form["restaurant_description"], Request.Form["cuisine_id"]);
        newRestaurant.Save();
        return View["success.cshtml"];
      };
      Get["/restaurant/all"] = _ => {
        List<Restaurant> allRestaurants = Restaurant.GetAll();
        return View["restaurants.cshtml", allRestaurants];
      };
      Get["restaurant/{id}"] = Parameters => {
        Dictionary<string, object> Model = new Dictionary<string, object>();
        var foundRestaurant = Restaurant.Find(Parameters.id);
        var foundCuisine = Cuisine.Find(foundRestaurant.GetCuisineId());
        Model.Add("restaurant", foundRestaurant);
        Model.Add("cuisine", foundCuisine);
        return View["restaurant.cshtml", Model];
      };
      Get["cuisine/{id}"] = Parameters => {
        Dictionary<string, object> Model = new Dictionary<string, object>();
        var foundCuisine = Cuisine.Find(Parameters.id);
        var foundRestaurants = foundCuisine.GetRestaurant();
        Model.Add("cuisine", foundCuisine);
        Model.Add("restaurants", foundRestaurants);
        return View["cuisine.cshtml", Model];
      };
    }
  }
}
