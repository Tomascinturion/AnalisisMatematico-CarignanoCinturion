using Microsoft.AspNetCore.Mvc;
using TabajoAnalisisMatematico.Models;
using Metodos;



namespace TabajoAnalisisMatematico.Controllers
{
    public class MetodosCerradosController : Controller
    {
        [HttpGet]
        public IActionResult Cerrados()
        {
            // Le mandamos a la vista un modelo vacío (con los valores por defecto)
            return View(new MetodosCerradoAbierto());
        }

        // 2. POST: Se ejecuta cuando el usuario hace clic en "Calcular"
        [HttpPost]
        public IActionResult Cerrados(MetodosCerradoAbierto modelo)
        {
            // Verificamos que los datos del formulario sean válidos
            if (ModelState.IsValid)
            {
                // ¡AQUÍ SUCEDE LA MAGIA! 
                // Llamamos a tu clase exactamente con los datos del modelo.
                // El resultado lo guardamos en la propiedad "Resultado" del modelo.
                modelo.Resultado = MetodosCerrados.AnalizarRaiz(
                    modelo.FuncionTexto,
                    modelo.IteracionesMaximas,
                    modelo.Tolerancia,
                    modelo.Xi,
                    modelo.Xd,
                    modelo.MetodoElegido
                );
            }

            // Volvemos a mostrar la misma página, pero ahora el modelo tiene el Resultado lleno.
            // La página HTML se dará cuenta y mostrará tu tarjeta.
            return View(modelo);
        }
    }
}
