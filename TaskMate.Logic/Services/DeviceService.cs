using Microsoft.AspNetCore.Identity;
using TaskMate.Domain.Entities;
using TaskMate.Data;

namespace TaskMate.Domain.Services
{
    public class DeviceService
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public DeviceService(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<(bool Success, string ErrorMessage)> AddDeviceAsync(string deviceId, ApplicationUser user)
        {
            if (string.IsNullOrWhiteSpace(deviceId))
                return (false, "Device ID cannot be empty.");

            if (user == null)
                return (false, "User is required.");

            var newDevice = new Device
            {
                DeviceId = deviceId,
                UserId = user.Id
            };

            _context.Devices.Add(newDevice);
            await _context.SaveChangesAsync();

            return (true, null);
        }
    }
}

