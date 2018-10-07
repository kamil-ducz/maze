//represents single brick of maze

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace labyrinth
{
    public class Brick
    {
        //center coordinates
        public int x = 0;
        public int y = 0;
        public bool upperWall = false;
        public bool bottomWall = false;
        public bool leftWall = false;
        public bool rightWall = false;
        public bool isCurrent = false; //is this brick currently visited or starting point?

        public Brick(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
    }
}
