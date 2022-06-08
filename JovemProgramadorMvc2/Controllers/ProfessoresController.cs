using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using JovemProgramadorMvc2.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Net.Http;
using System.Text.Json;

namespace JovemProgramadorMvc2.Controllers
{
    public class ProfessoresController : Controller
    {

        private readonly IConfiguration _configuration;
        public ProfessoresController()
        {

        }


        public IActionResult Index()
        {
            return View();
        }

    }
}
