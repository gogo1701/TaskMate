using Microsoft.AspNetCore.Identity;


namespace TaskMate.Domain.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public ICollection<Device> Devices { get; set; } = new List<Device>();
    }
}
