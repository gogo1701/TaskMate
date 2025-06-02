using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskMate.Data;
using TaskMate.Domain.Entities;

namespace TaskMate.Logic.Services
{
    public class PlannerService
    {
        private readonly ApplicationDbContext _context;

        public PlannerService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<TaskMate.Domain.Entities.Task>> GetTasksForDeviceAsync(string deviceId)
        {
            return await _context.Tasks
                .Include(t => t.Device)
                .Where(t => t.Device.DeviceId == deviceId)
                .ToListAsync();
        }

        public async Task<Device> GetDeviceFromTask(Domain.Entities.Task task)
        {
            return _context.Devices.Where(d => d.Tasks.Contains(task)).FirstOrDefault();
        }

        public async Task<(bool Success, string ErrorMessage, Domain.Entities.Task Task)> AddOrUpdateTaskAsync(
         int? id, string name, DayOfWeek dayOfWeek, string userId)
        {
            Device device = await _context.Devices.FirstOrDefaultAsync(d => d.UserId == userId);
            if (device == null)
                return (false, "No device found for this user.", null);

            Domain.Entities.Task task;
            if (id == null || id == 0)
            {
                task = new Domain.Entities.Task
                {
                    Name = name,
                    DayOfWeek = dayOfWeek,
                    DeviceId = device.Id
                };
                _context.Tasks.Add(task);
            }
            else
            {
                task = await _context.Tasks.FindAsync(id);
                if (task == null || task.Device.UserId != userId)
                    return (false, "Task not found or not owned by user.", null);

                task.Name = name;
                task.DayOfWeek = dayOfWeek;
            }

            await _context.SaveChangesAsync();
            return (true, null, task);
        }

        public async Task<(bool Success, string Message, string DeviceId)> DeleteTaskAsync(int id, string userId)
        {
            var task = await _context.Tasks
                .Include(t => t.Device)
                .FirstOrDefaultAsync(t => t.Id == id);

            if (task == null || task.Device.UserId != userId)
                return (false, "Task not found or unauthorized.", null);

            var deviceId = task.Device.DeviceId;

            _context.Tasks.Remove(task);
            await _context.SaveChangesAsync();

            return (true, null, deviceId);
        }
    }
}
