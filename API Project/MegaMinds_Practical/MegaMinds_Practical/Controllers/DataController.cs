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
        private readonly IWebHostEnvironment _env;
        public DataController(IDataRepository dataRepository , IWebHostEnvironment env) 
        {
            _dataRepository = dataRepository;
            _env = env;
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

        [HttpPost("save-data")]
        public IActionResult SaveData([FromBody] object updatedData)
        {
            string filePath = Path.Combine(_env.WebRootPath, "assets", "data.json");

            try
            {
                System.IO.File.WriteAllText(filePath, updatedData.ToString());
                return Ok(new { status = "success", message = "Data saved." });
            }
            catch (Exception ex)
            {   
                return StatusCode(500, new { status = "error", message = ex.Message });
            }
        }

    }
}
