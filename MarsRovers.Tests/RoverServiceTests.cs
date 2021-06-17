using AutoFixture;
using MarsRovers.Models;
using MarsRovers.Services;
using System;
using Xunit;
using System.Linq;
using Moq;
using AutoFixture.Kernel;

namespace MarsRovers.Tests
{
    public class RoverServiceTests
    {
        private readonly Fixture fixture;
        public RoverServiceTests()
        {
            this.fixture = new Fixture();
        }

        [Theory]
        [InlineData(1, 2, HeadingType.N, "LMLMLMLMM", "1 3 N")]
        [InlineData(3, 3, HeadingType.E, "MMRMMRMRRM", "5 1 E")]
        public void IntegrationTest_ExecuteCommands_ShouldReturn_ExpectedOutput(int x, int y, HeadingType z, string commands, string expected)
        {            
            RoverInstructions instructions = new RoverInstructions
            {
                Pose = new Pose
                {
                    Coordinates = new Coordinates(x, y),
                    Heading = z,
                },
                Commands = commands
                    .ToCharArray()
                    .Select(c => (CommandTypes)Enum.Parse(typeof(CommandTypes), c.ToString()))
                    .ToList()
            };

            MovementService movementService = new MovementService();
            fixture.Customizations.Add(
                new TypeRelay(
                    typeof(IMovementService),
                    typeof(MovementService)));
            RoverService sut = fixture.Freeze<RoverService>();

            Pose result = sut.ExecuteCommands(instructions, new Coordinates(5, 5));

            Assert.NotNull(result);
            Assert.Equal(expected, result.ToString());
        }

        

        [Theory]
        [InlineData("LLL", 3, 0, 0)]
        [InlineData("RRR", 0, 3, 0)]
        [InlineData("MMM", 0, 0, 3)]
        public void ExecuteCommands_ShouldMake_ExpectedMethodCalls(string commands, int left, int right, int move)
        {
            Coordinates coords = new Coordinates();    
            RoverInstructions instructions = new RoverInstructions
            {
                Pose = new Pose
                {
                    Coordinates = coords,
                    Heading = HeadingType.N,
                },
                Commands = commands
                    .ToCharArray()
                    .Select(c => (CommandTypes)Enum.Parse(typeof(CommandTypes), c.ToString()))
                    .ToList()
            };
            
            Mock<IMovementService> movementService = new Mock<IMovementService>();
            fixture.Inject(movementService.Object);
            movementService.Setup(m => m.TurnLeft(It.IsAny<HeadingType>()))
                .Returns(HeadingType.N).Verifiable();
            movementService.Setup(m => m.TurnRight(It.IsAny<HeadingType>()))
                .Returns(HeadingType.N).Verifiable();
            movementService.Setup(m => m.MoveForward(It.IsAny<Pose>(), It.IsAny<Coordinates>()))
                .Returns(coords).Verifiable();
            RoverService sut = fixture.Freeze<RoverService>();

            Pose result = sut.ExecuteCommands(instructions, new Coordinates(5, 5));

            Assert.NotNull(result);
            movementService.Verify(m => m.TurnLeft(It.IsAny<HeadingType>()), Times.Exactly(left));
            movementService.Verify(m => m.TurnRight(It.IsAny<HeadingType>()), Times.Exactly(right));
            movementService.Verify(m => m.MoveForward(It.IsAny<Pose>(), It.IsAny<Coordinates>()), Times.Exactly(move));
        }
    }
}
