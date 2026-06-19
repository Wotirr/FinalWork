using KnizhnyMir.DataAccess.Models;
using KnizhnyMir.DataAccess.Services;
using Microsoft.AspNetCore.Mvc;

namespace KnizhnyMir.Api.Controllers
{
    /// <summary>API для получения данных о товарах.</summary>
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController(ProductService productService) : ControllerBase
    {
        private readonly ProductService _productService = productService;

        /// <summary>Возвращает список всех товаров.</summary>
        [HttpGet]
        public ActionResult<IReadOnlyList<Product>> GetAll() => Ok(_productService.GetAll());

        /// <summary>Возвращает товар по артикулу.</summary>
        [HttpGet("{article}")]
        public ActionResult<Product> GetByArticle(string article)
        {
            var product = _productService.GetByArticle(article);
            return product is null ? NotFound() : Ok(product);
        }
    }
}
