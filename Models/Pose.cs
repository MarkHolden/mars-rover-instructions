using System;

namespace MarsRovers.Models
{
    public class Pose
    {
        /// <summary>
        /// The X, Y Coordinates of where the Rover is located.
        /// </summary>
        public Coordinates Coordinates { get; set; }

        /// <summary>
        /// Cardinal direction the rover is pointing.
        /// </summary>
        public HeadingType Heading { get; set; }

        public override string ToString()
        {
            return $"{this.Coordinates.X} {this.Coordinates.Y} {this.Heading}";
        }
    }
}
