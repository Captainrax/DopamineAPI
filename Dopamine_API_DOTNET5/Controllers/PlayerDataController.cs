
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Dopamine_API_Data_DOTNET5;
using Dopamine_API_Data_DOTNET5.Interfaces;
using Microsoft.Extensions.Logging;

namespace Dopamine_API_DOTNET5.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlayerDataController : ControllerBase
    {
        private IRepositoryWrapper _repositoryWrapper;

        private const int PlayerDataControllerLoggingEventID = 20;
        private readonly ILogger<PlayerDataController> _logger;

        public PlayerDataController(IRepositoryWrapper repositoryWrapper, ILogger<PlayerDataController> logger)
        {
            this._repositoryWrapper = repositoryWrapper;
            this._logger = logger;
        }

        // GET: api/PlayerData
        [HttpGet]
        public async Task<ActionResult> GetPlayerData()
        {
            var playerData = await _repositoryWrapper.PlayerDataRepositoryWrapper.GetAllPlayerData(); // TODO make DTOS

           
            //Serilog.Context.LogContext.PushProperty("UserName", "get request"); // ??????
            this._logger.LogWarning(PlayerDataControllerLoggingEventID, "GET Request Made.");


            return Ok(playerData);
        }

        // GET: api/PlayerData/5
        [HttpGet("{Id}/{IncludeRelations}")]
        public async Task<ActionResult<PlayerData>> GetPlayerData(int Id, bool IncludeRelations)
        {
            PlayerData playerData = await _repositoryWrapper.PlayerDataRepositoryWrapper.GetPlayerData(Id, IncludeRelations);

            if (playerData == null)
            {
                return NotFound();
            }

            this._logger.LogWarning(PlayerDataControllerLoggingEventID, "PlayerData.id : " + Id.ToString() + " Requested");

            return Ok(playerData);
        }

        // GET: api/PlayerData/5
        [HttpGet("{Id}")]
        public async Task<ActionResult<int>> GetPointsToAdd(int Id)
        {
            var points = await _repositoryWrapper.PlayerDataRepositoryWrapper.GetPointsToAdd(Id);

            this._logger.LogWarning(PlayerDataControllerLoggingEventID, "PlayerData.id : " + Id.ToString() + " Points added: " + points);

            return Ok(points);
        }

        // PUT: api/PlayerData/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPlayerData(int id, [FromBody] PlayerData playerData)
        {
            await _repositoryWrapper.PlayerDataRepositoryWrapper.Update(playerData);

            return Ok();
        }

        // POST: api/PlayerData
        // Creates new player "account", uses new PlayerData instead of arguments from sender, prevents players making new accounts with custom data.
        // probably could just be a GET or something, but this way i dont have to make overloads :L
        [HttpPost]
        public async Task<ActionResult<PlayerData>> PostNewPlayerData()
        {
            PlayerData playerData = new PlayerData();
            playerData.Upgrades = new Upgrades();
            await _repositoryWrapper.PlayerDataRepositoryWrapper.Create(playerData);

            //Serilog.Context.LogContext.PushProperty("UserName", UserName); //Push user in LogContext;
            this._logger.LogWarning(PlayerDataControllerLoggingEventID, "PlayerData.id : " + playerData.Id.ToString() + " og points : " + playerData.points);

            return Ok(playerData);
        }

        // DELETE: api/PlayerData/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePlayerData(PlayerData playerData)
        {
            await _repositoryWrapper.PlayerDataRepositoryWrapper.Delete(playerData);

            //var todoItem = await _context.TodoItems.FindAsync(id);
            //if (todoItem == null)
            //{
            //    return NotFound();
            //}

            //_context.TodoItems.Remove(todoItem);
            //await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PlayerDataExists(long id)
        {
            //return _context.TodoItems.Any(e => e.Id == id);
            return false;
        }
    }
}
