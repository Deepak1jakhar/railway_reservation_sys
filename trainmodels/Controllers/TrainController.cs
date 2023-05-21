using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using trainmodels.Models;
using trainmodels.Models.DTO;
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
            try
            {
                var trains = await _tr.GetAllAsync();
                var tDTOs = new List<TrainDTO>();

                foreach (var tr in trains)
                {
                    var tDTO = new TrainDTO
                    {
                        TrainId = tr.TrainId,
                        Source = tr.Source,
                        Destination = tr.Destination,
                        DepartureTime = tr.DepartureTime,
                        TotalSeats = tr.TotalSeats,
                        AvailableSeats = tr.AvailableSeats,
                        TrainName = tr.TrainName,
                        ArrivalTime = tr.ArrivalTime,
                        Fare = tr.Fare
                    };
                    tDTOs.Add(tDTO);
                }
                return Ok(tDTOs);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            } 
        }

        [HttpGet]
        [Route("GetTrainbyid/{trainid}")]
        public async Task<IActionResult> GettrainbyidAsync(int trainid)
        {
            try
            {
                var train = await _tr.GetTrainByIdAsync(trainid);
                var t = new TrainDTO { 
                    TrainId = trainid,
                    Source = train.Source,
                    Destination = train.Destination,
                    ArrivalTime= train.ArrivalTime,
                    DepartureTime= train.DepartureTime,
                    TotalSeats = train.TotalSeats,
                    AvailableSeats = train.AvailableSeats,
                    TrainName = train.TrainName,
                    Fare= train.Fare
                };
                return Ok(t);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }

        [HttpPost]
        [Route("AddTrain")]
        public async Task<IActionResult> AddTrainAsync(TrainDTO t)
        {
            try
            {
                var train = new Train
                {
                    TrainId = t.TrainId,
                    TrainName = t.TrainName,
                    Fare = t.Fare,
                    Source = t.Source,
                    Destination = t.Destination,
                    ArrivalTime = t.ArrivalTime,
                    AvailableSeats = t.AvailableSeats,
                    TotalSeats = t.TotalSeats,
                    DepartureTime = t.DepartureTime
                };
                await _tr.AddTrainAsync(train);
                return Ok(new {Message="Train added"});
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }

        [HttpDelete]
        [Route("DeleteTrain/{trainid}")]
        public async Task<IActionResult> deleteTrainAsync([FromRoute] int trainid)
        {
            try
            {
                _tr.DeleteTrainAsync(trainid);
                return Ok(new {Message="Deleted Successfully"});
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }

        [HttpGet]
        [Route("{source}/{destination}")]
        public async Task<IActionResult> SourceDestinationAsync(string source, string destination)
        {
            try
            {
                source = source.ToLower();
                destination = destination.ToLower();
                var train = _tr.GetTrainBySourceDestinationAsync(source, destination);
                var trainsDTO = new List<TrainDTO>();
                foreach (var t in train)
                {
                    var trainDto = new TrainDTO
                    {
                        TrainId= t.TrainId,
                        Source  = t.Source, 
                        Destination = t.Destination,
                        ArrivalTime= t.ArrivalTime,
                        DepartureTime= t.DepartureTime,
                        Fare = t.Fare,
                        AvailableSeats  = t.AvailableSeats,
                        TotalSeats = t.TotalSeats,
                        TrainName= t.TrainName
                    };
                    trainsDTO.Add(trainDto);
                }
                return Ok(trainsDTO);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }

        [HttpPut]
        [Route("UpdateTrain/{id}")]
        public async Task<IActionResult> UpdatetrainAsync(int id, TrainDTO traindto)
        {
            try
            {
                var t = await _tr.GetTrainByIdAsync(id);
                if (t == null)
                {
                    return BadRequest("Train with id not found");
                }
                t.TrainName=traindto.TrainName;
                t.Source=traindto.Source;
                t.Destination=traindto.Destination;
                t.Fare=traindto.Fare;
                t.TotalSeats=traindto.TotalSeats;
                t.AvailableSeats=traindto.AvailableSeats;
                t.DepartureTime=t.DepartureTime;
                t.ArrivalTime=t.ArrivalTime;


                await _tr.UpdateTrainAsync(t);
                return Ok(traindto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
    }
}
