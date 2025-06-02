using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using TaskMate.Data;
using TaskMate.Domain.Entities;
using TaskMate.Logic.Services;

namespace TaskMate.Web.Controllers
{
    public class PlannerController : Controller
    {
        private readonly PlannerService _plannerService;
        private readonly UserManager<ApplicationUser> _userManager;

        public PlannerController(PlannerService plannerService, UserManager<ApplicationUser> userManager)
        {
            _plannerService = plannerService;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index(string deviceId)
        {
            var tasks = await _plannerService.GetTasksForDeviceAsync(deviceId);
            return View(tasks);
        }

        [HttpPost]
        public async Task<IActionResult> AddOrEditTask(int? id, string name, DayOfWeek dayOfWeek)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var (success, message, task) = await _plannerService.AddOrUpdateTaskAsync(id, name, dayOfWeek, userId);

            if (!success)
                return BadRequest(message);

            var device = await _plannerService.GetDeviceFromTask(task);
            var deviceId = device.DeviceId;
            return RedirectToAction("Index", new { deviceId = deviceId });
        }

        [HttpPost]
        public async Task<IActionResult> DeleteTask(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var (success, message, deviceId) = await _plannerService.DeleteTaskAsync(id, userId);

            if (!success)
                return NotFound(message);

            return RedirectToAction("Index", new { deviceId = deviceId });
        }
    }

}
