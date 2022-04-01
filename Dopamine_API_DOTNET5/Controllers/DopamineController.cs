using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Dopamine_API_Data_DOTNET5;
using System.Collections.Generic;
using Dopamine_API_Data_DOTNET5.Interfaces;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Dopamine_API_DOTNET5.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DopamineController : ControllerBase
    {

        private IRepositoryWrapper _repositoryWrapper;

        private const int PlayerDataControllerLoggingEventID = 20;
        private readonly ILogger<DopamineController> _logger;

        public DopamineController(IRepositoryWrapper repositoryWrapper, ILogger<DopamineController> logger)
        {
            this._repositoryWrapper = repositoryWrapper;
            this._logger = logger;
        }

        // GET: api/Dopamine
        [HttpGet]
        public async Task<ActionResult> GetPlayerData()
        {
            var playerData = await _repositoryWrapper.PlayerDataRepositoryWrapper.GetAllPlayerData();

            this._logger.LogWarning(PlayerDataControllerLoggingEventID, "GET Request Made. DopamineController");

            return Ok(playerData);
        }

        // POST: api/Dopamine/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("{Id}")]
        public async Task<IActionResult> PostPlayerData(int Id, [FromBody]string pointIncrease)
        {
            PlayerData playerData = await _repositoryWrapper.PlayerDataRepositoryWrapper.GetPlayerData(Id, false);

            if (playerData != null)
            {
                playerData.pointsToBeAdded += Int32.Parse(pointIncrease);
                this._logger.LogWarning(PlayerDataControllerLoggingEventID, "PlayerData.id : " + playerData.Id.ToString() + " og points : " + playerData.points);
            }
            else
            {
                this._logger.LogCritical(PlayerDataControllerLoggingEventID, "PlayerData.id : " + playerData.Id.ToString() + " could not increase points by: " + pointIncrease);
            }
            await _repositoryWrapper.PlayerDataRepositoryWrapper.Update(playerData);
            await _repositoryWrapper.PlayerDataRepositoryWrapper.Save();

            return Ok(playerData.Id);
        }

    }
}
