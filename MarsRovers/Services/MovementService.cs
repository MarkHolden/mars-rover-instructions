using System;
using MarsRovers.Models;

namespace MarsRovers.Services
{
    public class MovementService : IMovementService
    {
        public MovementService()
        {
        }

        public HeadingType TurnLeft(HeadingType heading) => heading switch
        {
            HeadingType.N => HeadingType.W,
            HeadingType.W => HeadingType.S,
            HeadingType.S => HeadingType.E,
            HeadingType.E => HeadingType.N,
            _ => throw new ArgumentOutOfRangeException(nameof(heading), $"Unable to turn left from heading: {heading}"),
        };

        public HeadingType TurnRight(HeadingType heading) => heading switch
        {
            HeadingType.N => HeadingType.E,
            HeadingType.W => HeadingType.N,
            HeadingType.S => HeadingType.W,
            HeadingType.E => HeadingType.S,
            _ => throw new ArgumentOutOfRangeException(nameof(heading), $"Unable to turn left from heading: {heading}"),
        };

        public Coordinates MoveForward(Pose pose, Coordinates maxPosition)
        {
            Coordinates delta = GetPositionDelta(pose.Heading);
            Coordinates newPosition = GetNewPosition(pose.Coordinates, delta);
            VerifyLocation(newPosition, maxPosition);
            return newPosition;
        }

        public Coordinates GetPositionDelta(HeadingType heading) => heading switch
        {
            HeadingType.N => new Coordinates(0, 1),
            HeadingType.W => new Coordinates(-1, 0),
            HeadingType.S => new Coordinates(0, -1),
            HeadingType.E => new Coordinates(1, 0),
            _ => throw new ArgumentOutOfRangeException(nameof(heading), $"Unable to get position delta for heading: {heading}"),
        };

        private Coordinates GetNewPosition(Coordinates position, Coordinates delta)
        {
            position.X += delta.X;
            position.Y += delta.Y;
            return position;
        }

        private void VerifyLocation(Coordinates position, Coordinates maxPosition)
        {
            if (position.X > maxPosition.X || position.X < 0)
                throw new ArgumentOutOfRangeException("The Rover's X-Coordinate would fall outside expected area if that move were completed.");

            if (position.Y > maxPosition.Y || position.Y < 0)
                throw new ArgumentOutOfRangeException("The Rover's Y-Coordinate would fall outside expected area if that move were completed.");
        }
    }
}