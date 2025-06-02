using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TaskMate.Data;
using TaskMate.Domain.Entities;
using TaskMate.Domain.Services;

namespace TaskMate.Web.Controllers
{
    [Authorize]
    public class DeviceController : Controller
    {
        private readonly DeviceService _deviceService;
        private readonly UserManager<ApplicationUser> _userManager;

        public DeviceController(DeviceService deviceService, UserManager<ApplicationUser> userManager)
        {
            _deviceService = deviceService;
            _userManager = userManager;
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(string deviceId)
        {
            var user = await _userManager.GetUserAsync(User);
            var (success, error) = await _deviceService.AddDeviceAsync(deviceId, user);

            if (!success)
            {
                ModelState.AddModelError("", error);
                return View();
            }

            return RedirectToAction("Index", "Planner", new { deviceId = deviceId});
        }
    }
}
