namespace MarsRovers.Models
{
    public class Coordinates
    {
        public Coordinates(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }

        /// <summary>
        /// Location of the Rover on the X axis.
        /// </summary>
        public int X { get; set; }

        /// <summary>
        /// Location of the Rover on the Y axis.
        /// </summary>
        public int Y { get; set; }
    }
}