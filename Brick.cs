namespace labyrinth
{
    /// <summary>
    ///represents single field of maze
    /// </summary>
    public class Brick
    {
        public int x { get; set; }
        public int y { get; set; }
        public bool upperWall { get; set; }
        public bool bottomWall { get; set; }
        public bool leftWall { get; set; }
        public bool rightWall { get; set; }

        public Brick(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

    }
}
