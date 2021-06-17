using MarsRovers.Models;

namespace MarsRovers.Services
{
    public interface IMovementService
    {
        Coordinates MoveForward(Pose pose, Coordinates maxPosition);
        HeadingType TurnLeft(HeadingType heading);
        HeadingType TurnRight(HeadingType heading);
    }
}