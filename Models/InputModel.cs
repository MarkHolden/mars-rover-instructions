using System.Collections.Generic;

namespace MarsRovers.Models
{
    public class InputModel
    {
        public Coordinates MaximumCoordinates { get; set; }
        public List<RoverInstructions> RoverInstructions { get; set; }
    }
}