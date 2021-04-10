using Freezer_MVC.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Freezer.Controllers.Api
{
    public class FoodApiController : Controller
    {
        public FoodApiController():base()
        {
        }

        public async Task<IEnumerable<Food>> Get()
        {
            var food = new Food[]
             {
                new Food {Id =0, Name ="Fisch"},
                new Food {Id =1, Name ="Gemüse"},
             };
            return food;
        }

       [HttpPost]
       public object Update(object data)
       {
                return true;
       }
    }
}
