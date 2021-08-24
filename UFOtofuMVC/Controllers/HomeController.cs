using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using UFOtofuMVC.Models;
using System.Text.RegularExpressions;

namespace UFOtofuMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        //[HttpGet]
        public IActionResult Index()
        {
            Palindrome palindrome = new();
            return View(palindrome);
            return View();
        }

        [HttpPost]
        //[ValidateAntiForgeryToken] not needed at the moment
        public IActionResult Index(Palindrome palindrome)
        {
            string inputString = palindrome.InputString;
            string revString = "";
            inputString = inputString.ToLower(); //converts to lowercase
            inputString = Regex.Replace(inputString, @"[^a-z0-9]+", ""); // removes everything that is not a letter
            for (int i = inputString.Length-1 ; i >= 0 ; i--) //reverses the string 
            {
                revString += inputString[i];
            }
            palindrome.RevString = revString;
            if (revString == inputString)
            {
                palindrome.IsPalindrome = true;
            }
            
            if (palindrome.IsPalindrome)
            {
                palindrome.Message = $"{palindrome.InputString} when reversed is {revString}, the string is a palindrome!";
            }
            else
            {
                palindrome.Message = $"{palindrome.InputString} when reversed is {revString}, the string is NOT a palindrome...";
            }

            return View(palindrome);
        }



        public IActionResult About()
        {
            return View();
        }

        

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
