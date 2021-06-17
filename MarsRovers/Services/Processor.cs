using System;
using System.Collections.Generic;
using MarsRovers.Models;

namespace MarsRovers.Services
{
    public class Processor : IProcessor
    {
        private readonly IRoverService roverService;

        public Processor(IRoverService roverService)
        {
            this.roverService = roverService;
        }

        public void Process()
        {
            InputModel input = GetUserInput();
            List<Pose> rovers = new List<Pose>();
            foreach (RoverInstructions roverInstructions in input.RoverInstructions)
            {
                rovers.Add(roverService.ExecuteCommands(roverInstructions, input.MaximumCoordinates));
            }

            Console.WriteLine("Final Rover Positions and Headings:");
            rovers.ForEach(r => Console.WriteLine(r.ToString()));
        }

        private static InputModel GetUserInput()
        {
            InputModel input = new InputModel
            {
                MaximumCoordinates = GetMaxCoordinatesInput(),
                RoverInstructions = GetRoverInstructions(),
            };

            return input;
        }

        private static Coordinates GetMaxCoordinatesInput()
        {
            Console.WriteLine("Enter the maximum X and Y coordinates and press enter.");
            string rawMaxCoords = Console
                .ReadLine()
                .Trim();

            string[] maxCoordArray = rawMaxCoords.Split(' ');

            if (maxCoordArray.Length != 2)
                throw new ArgumentOutOfRangeException("Wrong number of inputs.");

            bool xSuccess = int.TryParse(maxCoordArray[0], out int maxX);
            bool ySuccess = int.TryParse(maxCoordArray[1], out int maxY);

            if (!xSuccess || !ySuccess)
                throw new Exception("Unable to parse input. Please try again.");

            return new Coordinates(maxX, maxY);
        }

        private static List<RoverInstructions> GetRoverInstructions()
        {
            List<RoverInstructions> list = new List<RoverInstructions>();
            for (int i = 0; i < 2; i++)
            {
                list.Add(new RoverInstructions
                {
                    Pose = GetPose(),
                    Commands = GetCommands(),
                });
            }
            return list;
        }

        private static Pose GetPose()
        {
            Console.WriteLine("Enter Rover current X and Y coordinates and heading and press enter.");
            string rawPose = Console
                .ReadLine()
                .Trim();

            string[] poseArray = rawPose.Split(' ');

            if (poseArray.Length != 3)
                throw new ArgumentOutOfRangeException("Wrong number of inputs.");

            bool xSuccess = int.TryParse(poseArray[0], out int x);
            bool ySuccess = int.TryParse(poseArray[1], out int y);
            bool zSuccess = Enum.TryParse(poseArray[2].ToUpper(), out HeadingType heading);

            if (!xSuccess || !ySuccess || !zSuccess)
                throw new Exception("Unable to parse input. Please try again.");

            return new Pose
            {
                Coordinates = new Coordinates(x, y),
                Heading = heading,
            };
        }

        private static List<CommandTypes> GetCommands()
        {
            Console.WriteLine("Enter Rover commands and press enter.");
            char[] rawCommands = Console
                .ReadLine()
                .Trim()
                .ToCharArray();

            List<CommandTypes> list = new List<CommandTypes>();
            foreach (char c in rawCommands)
            {
                bool success = Enum.TryParse(c.ToString().ToUpper(), out CommandTypes cmd);

                if (!success)
                    throw new Exception("Unable to parse input. Please try again.");

                list.Add(cmd);
            }
            return list;
        }
    }
}