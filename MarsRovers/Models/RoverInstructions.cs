using System.Collections.Generic;

namespace MarsRovers.Models
{
    public class RoverInstructions
    {
        public Pose Pose { get; set; }
        public List<CommandTypes> Commands { get; set; }
    }
}