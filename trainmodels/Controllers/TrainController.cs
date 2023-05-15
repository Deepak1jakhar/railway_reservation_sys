using Microsoft.AspNetCore.Mvc;
using trainmodels.Models;
using trainmodels.Repository;

namespace trainmodels.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TrainController : Controller
    {
        private readonly ITrain _tr;

        public TrainController(ITrain tr)
        {
            _tr = tr;
        }

        [HttpGet]
        [Route("Trains")]
        public async Task<IActionResult> GetAllTrainsAsync()
        {
            var trains = await _tr.GetAllAsync();
            return Ok(trains);
        }

        [HttpGet]
        [Route("GetTrainbyid/{trainid}")]
        public async Task<IActionResult> GettrainbyidAsync(int trainid)
        {
            var train = await _tr.GetTrainByIdAsync(trainid);
            if(train == null)
            {
                return NotFound();
            }
            return Ok(train);
        }

        [HttpPost]
        [Route("AddTrain")]
        public async Task<IActionResult> AddTrainAsync(Train train)
        {
            var trobj = await _tr.AddTrainAsync(train);
            return RedirectToAction("GetAllTrainsAsync");
        }

        [HttpDelete]
        [Route("DeleteTrain")]
        public async Task<IActionResult> deleteTrainAsync(int trainid)
        {
            _tr.DeleteTrainAsync(trainid);
            return Ok();
        }

        [HttpGet]
        [Route("{source}/{destination}")]
        public async Task<IActionResult> SourceDestinationAsync(string source, string destination)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            source = source.ToLower();
            destination = destination.ToLower();
            var train = _tr.GetTrainBySourceDestinationAsync(source, destination);
            return Ok(train);
        }

        [HttpPut]
        [Route("UpdateTrain/{id}")]
        public async Task<IActionResult> UpdatetrainAsync(int id, Train train)
        {
            Train train1 = await _tr.UpdateTrainAsync(id, train);
            if(train1 == null)
            {
                return Ok("Train is not available for Updation");
            }
            return Ok(train1);

        }
    }
}
