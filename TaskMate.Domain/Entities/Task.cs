using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TaskMate.Domain.Entities
{
    public class Task
    {
        public int Id {get; set; }
        public string Name { get; set; }
        public DayOfWeek DayOfWeek { get; set; }
        public int DeviceId { get; set; } // FK -> Device

        [JsonIgnore]
        public Device Device { get; set; } // navigation property to Device
    }
}
