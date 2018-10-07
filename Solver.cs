//TODO public enum facingAt instead 'str facing,At'
//{ N, W, S, E };

using System;

namespace labyrinth
{
    /// <summary>
    ///represents object wandering in maze looking for exit
    /// </summary>
    public class Solver
    {

        public bool isWallInFront = false; //sensor detecting wall in front of object
        public int x;
        public int y;
        public string facingAt = "N";

        public Solver(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public Solver(int x, int y, string direction)
        {
            this.x = x;
            this.y = y;
            this.facingAt = direction;
        }

        public void turnLeft()
        {
            switch (this.facingAt)
            {
                case "N": this.facingAt = "W"; break;
                case "W": this.facingAt = "S"; break;
                case "S": this.facingAt = "E"; break;
                case "E": this.facingAt = "N"; break;
                default: throw new Exception();
            }

        }

        /// <summary>
        ///check if there is a wall on the right
        /// </summary>
        public bool checkRightWall(Brick brick)
        {
            //this.x = brick.x;
            //this.y = brick.y;

            if (this.facingAt == "N" && brick.rightWall == true)
                return true;
            return false;
        }

    }
}
