using System;
using System.Collections.Generic;
using MarsRovers.Models;
using MarsRovers.Services;

namespace MarsRovers.Services
{

    public class RoverService : IRoverService
    {
        private Pose pose;
        private readonly IMovementService movementService;
        public RoverService(IMovementService movementService)
        {
            this.movementService = movementService;
        }

        public Pose ExecuteCommands(RoverInstructions instructions, Coordinates maxPosition)
        {
            this.pose = instructions.Pose;
            foreach (CommandTypes command in instructions.Commands)
            {
                ExecuteCommand(command, maxPosition);
            }
            return this.pose;
        }

        private void ExecuteCommand(CommandTypes command, Coordinates maxPosition)
        {
            switch (command)
            {
                case CommandTypes.L:
                    this.pose.Heading = movementService.TurnLeft(this.pose.Heading);
                    break;
                case CommandTypes.R:
                    this.pose.Heading = movementService.TurnRight(this.pose.Heading);
                    break;
                case CommandTypes.M:
                    this.pose.Coordinates = movementService.MoveForward(this.pose, maxPosition);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(command), $"Unable to execute command: {command}");
            }
        }
    }
}