namespace TaskMate.Domain.Entities
{
    public class Device
    {
        public int Id { get; set; }
        public string UserId { get; set; } //FK -> ApplicationUser
        public string DeviceId { get; set; }
        public ApplicationUser User { get; set; } // navigation property to ApplicationUser
        public ICollection<Task> Tasks { get; set; } // nav property

    }
}
