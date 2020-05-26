using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Lab25_Shopping_Cart.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Lab25_Shopping_Cart.Controllers
{
    public class MovieController : Controller
    {
        public static bool isCartEmpty = true;

        public static List<RentMovie> Movies = new List<RentMovie>()
        {
            new RentMovie(0, "Dude Wheres My Car?", "Comedy", 2000, 120, 6.99),
            new RentMovie(1, "Dude Wheres My Mom?", "Comedy", 2000, 120, 6.99),
            new RentMovie(2, "Dude Wheres My Girlfriend?", "Comedy", 2000, 120, 6.99),
            new RentMovie(3, "Dude Wheres My Dog?", "Comedy", 2000, 120, 6.99),
            new RentMovie(4, "Dude Wheres My House?", "Comedy", 2000, 120, 6.99),
            new RentMovie(5, "Dude Wheres My Clothes?", "Comedy", 2000, 120, 6.99)
        };
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ShowTable()
        {
            return View(Movies);
        }

        public IActionResult AddToCart(RentMovie movie)
        {
            List<RentMovie> cartList;
            string movieCartJSON = HttpContext.Session.GetString("Cart") ?? "FirstTimeDoingThis";
            if (movieCartJSON != "FirstTimeDoingThis")
            {
                cartList = JsonSerializer.Deserialize<List<RentMovie>>(movieCartJSON);
            }
            else
            {
                cartList = new List<RentMovie>();
            }

            bool isInCart = false;
            for (int i = 0; i < cartList.Count; i++)
            {
                if (cartList[i].ID == movie.ID)
                {
                    isInCart = true;
                    ViewData["Message result2"] = "Already in cart!";
                    break;
                }
            }

            if (isInCart == false)
            {
                isCartEmpty = false;
            cartList.Add(movie);
               
                ViewData["Receipt View"] = "Here is your receipt";
            }    



            movieCartJSON = JsonSerializer.Serialize(cartList);

            HttpContext.Session.SetString("Cart", movieCartJSON);

            return View(cartList);
        }

        public IActionResult ViewCart()
        {

            List<RentMovie> cartList;
            string movieCartJSON = HttpContext.Session.GetString("Cart") ?? "FirstTimeDoingThis";
            if (movieCartJSON != "FirstTimeDoingThis")
            {
                cartList = JsonSerializer.Deserialize<List<RentMovie>>(movieCartJSON);
            }
            else
            {
                cartList = new List<RentMovie>();
            }
            movieCartJSON = JsonSerializer.Serialize(cartList);

            HttpContext.Session.SetString("Cart", movieCartJSON);

            return View(cartList);
        }

        public IActionResult ClearCart()
        {

            string movieCartJSON = HttpContext.Session.GetString("Cart") ?? "FirstTimeDoingThis";

            HttpContext.Session.Clear();

            List<RentMovie> cartList;
            cartList = JsonSerializer.Deserialize<List<RentMovie>>(movieCartJSON);

            Lab25_Shopping_Cart.Controllers.MovieController.isCartEmpty = true;

            return View("Receipt", cartList);
        }
       
    }
}