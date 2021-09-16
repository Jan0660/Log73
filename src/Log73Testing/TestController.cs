using System;
using Microsoft.AspNetCore.Mvc;

namespace Log73Testing
{
    public class TestController : Controller
    {
        [HttpGet("/success")]
        public void Index()
        {
        }

        [HttpGet("/exception")]
        public object Exception()
        {
            throw new Exception();
        }
    }
}