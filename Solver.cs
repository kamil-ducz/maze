using System;

public enum Direction { North, West, South, East };

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
        //public string facingAt = Direction.North;
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

        //TODO fill turnLeft and checkRigthWall
        //public void turnLeft()
        //{
        //    switch (this.facingAt)
        //    {
        //        case Direction.North: this.facingAt = Direction.West; break;
        //        case Direction.West: this.facingAt = Direction.South; break;
        //        case Direction.South: this.facingAt = Direction.East; break;
        //        case Direction.East: this.facingAt = Direction.North; break;
        //        default: throw new Exception();
        //    }

        //}

        /// <summary>
        ///check if there is a wall on the right
        /// </summary>
        //public bool checkRightWall(Brick brick)
        //{
        //    //this.x = brick.x;
        //    //this.y = brick.y;

        //    if (this.facingAt == Direction.North && brick.rightWall == true)
        //        return true;
        //    return false;
        //}

    }
}
