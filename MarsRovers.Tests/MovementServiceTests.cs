using System;
using Xunit;
using MarsRovers.Models;
using AutoFixture;
using MarsRovers.Services;

namespace MarsRovers.Tests
{
    public class MovementServiceTests
    {
        private readonly Fixture fixture;
        public MovementServiceTests()
        {
            this.fixture = new Fixture();
        }

        [Theory]
        [InlineData(1, 0, "W")]
        [InlineData(0, 1, "S")]
        public void MoveForward_Should_ReturnNewPosition_WhenMoveIsValid(int x, int y, string z)
        {
            Enum.TryParse(z, out HeadingType heading);
            Pose pose = new Pose
            {
                Heading = heading,
                Coordinates = new Coordinates(x, y),
            };

            MovementService sut = fixture.Freeze<MovementService>();

            Coordinates result = sut.MoveForward(pose, new Coordinates(1, 1));
            Assert.NotNull(result);
            Assert.Equal(0, result.X);
            Assert.Equal(0, result.Y);
        }

        [Theory]
        [InlineData(1, 0, "E")]
        [InlineData(0, 1, "N")]
        public void MoveForward_Should_ThrowArgumentOutOfRangeException_WhenMoveOutOfBounds(int x, int y, string z)
        {
            Enum.TryParse(z, out HeadingType heading);
            Pose pose = new Pose
            {
                Heading = heading,
                Coordinates = new Coordinates(x, y),
            };

            MovementService sut = fixture.Freeze<MovementService>();

            Assert.Throws<ArgumentOutOfRangeException>(() => sut.MoveForward(pose, new Coordinates(1, 1)));
        }

        [Theory]
        [InlineData(HeadingType.N, HeadingType.W)]
        [InlineData(HeadingType.W, HeadingType.S)]
        [InlineData(HeadingType.S, HeadingType.E)]
        [InlineData(HeadingType.E, HeadingType.N)]
        public void TurnLeft_Should_ReturnCorrectHeading(HeadingType input, HeadingType expected)
        {
            MovementService sut = fixture.Freeze<MovementService>();

            HeadingType result = sut.TurnLeft(input);

            Assert.NotNull(result);
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(HeadingType.N, HeadingType.E)]
        [InlineData(HeadingType.W, HeadingType.N)]
        [InlineData(HeadingType.S, HeadingType.W)]
        [InlineData(HeadingType.E, HeadingType.S)]
        public void TurnRight_Should_ReturnCorrectHeading(HeadingType input, HeadingType expected)
        {
            MovementService sut = fixture.Freeze<MovementService>();

            HeadingType result = sut.TurnRight(input);

            Assert.NotNull(result);
            Assert.Equal(expected, result);
        }
    }
}
