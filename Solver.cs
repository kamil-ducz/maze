public enum Direction { North, West, South, East };

namespace labyrinth
{
    /// <summary>
    ///represents object wandering in maze looking for exit
    /// </summary>
    public class Solver
    {
        public int x;
        public int y;
        public Direction facingAt = Direction.North;

        public Solver(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public Solver(int x, int y, Direction direction)
        {
            this.x = x;
            this.y = y;
            this.facingAt = direction;
        }

    }
}
