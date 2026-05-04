using System.Diagnostics;
using Metodos;
using Microsoft.AspNetCore.Mvc;
using TabajoAnalisisMatematico.Models;

namespace TabajoAnalisisMatematico.Controllers
{
    public class GaussJordanController : Controller
    {
        [HttpGet]
        public IActionResult GJ()
        {

            return View(new GaussJordanModel());
        }


        [HttpPost]
        public IActionResult GJ(GaussJordanModel modelo)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    modelo.Resultado = GaussJordan.Gauss_Jordan(modelo.Matriz);
                }
                catch (Exception ex)
                {
                    ViewBag.ErrorMessage = ex.Message;
                }

            }
                return View(modelo);
        }
    }
}