using MegaMinds_Practical.Model;
using MegaMinds_Practical.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MegaMinds_Practical.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DataController : ControllerBase
    {
        private readonly IDataRepository _dataRepository;

        public DataController(IDataRepository dataRepository) 
        {
            _dataRepository = dataRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetJsonData()
        {
            try
            {
                var data = await _dataRepository.GetAllObservationsAsync();
                return Ok(data);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error retrieving data", details = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> SaveJsonData([FromBody] ObservationsModel model)
        {
            try
            {
                var success = await _dataRepository.SaveObservationAsync(model);

                if (success)
                    return Ok(new { message = "Data saved successfully" });
                else
                    return StatusCode(500, new { message = "Error in save." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Internal error", details = ex.Message });
            }
        }
    }
}
