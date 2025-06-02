using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TaskMate.Logic.Services;
using System.Threading.Tasks;

namespace TaskMate.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlannerController : ControllerBase
    {
        private readonly PlannerService _plannerService;

        public PlannerController(PlannerService plannerService)
        {
            _plannerService = plannerService;
        }

        [HttpGet]
        public async Task<ICollection<TaskMate.Domain.Entities.Task>> GetTasks([FromQuery] string deviceId)
        { 
            var tasks = await _plannerService.GetTasksForDeviceAsync(deviceId);
            return tasks;
        }
    }
}
