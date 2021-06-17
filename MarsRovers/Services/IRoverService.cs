using MarsRovers.Models;

namespace MarsRovers.Services
{
    public interface IRoverService
    {
        Pose ExecuteCommands(RoverInstructions instructions, Coordinates maxPosition);
    }
}