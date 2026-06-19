using KnizhnyMir.DataAccess.Models;
using KnizhnyMir.DataAccess.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace KnizhnyMir.Api.Controllers
{
    /// <summary>API для получения справочных данных.</summary>
    [ApiController]
    [Route("api/[controller]")]
    public class LookupController(LookupRepository lookupRepository) : ControllerBase
    {
        private readonly LookupRepository _lookupRepository = lookupRepository;

        /// <summary>Возвращает список категорий.</summary>
        [HttpGet("categories")]
        public ActionResult<IReadOnlyList<Category>> GetCategories() => Ok(_lookupRepository.GetCategories());

        /// <summary>Возвращает список производителей.</summary>
        [HttpGet("manufacturers")]
        public ActionResult<IReadOnlyList<Manufacturer>> GetManufacturers() => Ok(_lookupRepository.GetManufacturers());

        /// <summary>Возвращает список статусов заказа.</summary>
        [HttpGet("order-statuses")]
        public ActionResult<IReadOnlyList<OrderStatus>> GetOrderStatuses() => Ok(_lookupRepository.GetOrderStatuses());
    }
}
