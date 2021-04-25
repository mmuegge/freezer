using Freezer_MVC.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DevExtreme.AspNet.Mvc;
using DevExtreme.AspNet.Data;

namespace Freezer.Controllers.Api
{
    public class FoodApiController : Controller
    {
        private Food[] _testData;

        public FoodApiController():base()
        {
            this._testData = new Food[]
            {
                new Food {Id =0, Name ="Fisch"},
                new Food {Id =1, Name ="Gemüse"},
            };
        }

        [HttpGet]
        public object Get(DataSourceLoadOptions loadOptions)
        {
            return DataSourceLoader.Load(this._testData, loadOptions);
        }

       [HttpPost]
       public object Update(object data)
       {
                return true;
       }
    }
}
