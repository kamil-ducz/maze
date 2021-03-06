﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace labyrinth
{
    public partial class Form1 : Form
    {
        //you can only make references here
        List<Brick> bricks;
        Graphics gObject;
        Brush black;
        Brush white;
        Pen blackPen;
        Pen whitePen;

        //declare vars for exit and start indexes
        int exitBrickIndex ;
        int startingBrickIndex;

        async Task PutTaskDelay()
        {
            await Task.Delay(500);
        }

        public Form1()
        {
            InitializeComponent();
        }

        async private void Form1_Load(object sender, EventArgs e)
        {
            await PutTaskDelay();
            bricks = new List<Brick>();
            mazeTrace.ResetText();
            gObject = canvas.CreateGraphics();
            black = new SolidBrush(Color.Black);
            white = new SolidBrush(Color.White);
            blackPen = new Pen(black, 4);
            whitePen = new Pen(white, 4);

            int line = 0;
            int column = 1;
            for (int i = 0; i < 25; i++)
            {
                if (i % 5 == 0)
                {
                    line++;
                    column = 1;
                }
                bricks.Add(new Brick((80 * column), 80 * line));

                //define logic of bricks for borders of maze
                //draw borders of maze
                if (i >= 0 && i <= 4)
                {
                    bricks[i].upperWall = true;
                    gObject.DrawLine(blackPen, bricks[i].x - 40, bricks[i].y - 40, bricks[i].x + 40, bricks[i].y - 40);
                }
                if (i >= 20 && i <= 24)
                {
                    bricks[i].bottomWall = true; gObject.DrawLine(blackPen, bricks[i].x - 40, bricks[i].y + 40, bricks[i].x + 40, bricks[i].y + 40);
                }
                if (i % 5 == 0)
                {
                    bricks[i].leftWall = true;
                    gObject.DrawLine(blackPen, bricks[i].x - 40, bricks[i].y - 40, bricks[i].x - 40, bricks[i].y + 40);
                }
                if (i % 5 == 4)
                {
                    bricks[i].rightWall = true;
                    gObject.DrawLine(blackPen, bricks[i].x + 40, bricks[i].y - 40, bricks[i].x + 40, bricks[i].y + 40);

                }

                column++;
            }


            mazeTrace.AppendText("Generating maze...\n");
            //manually define brick walls inside maze
            bricks[0].rightWall = true;
            bricks[1].leftWall = true;
            bricks[3].rightWall = true;
            bricks[4].leftWall = true;
            bricks[5].rightWall = true;
            bricks[6].leftWall = true;
            bricks[7].bottomWall = true;
            bricks[8].rightWall = true;
            bricks[9].leftWall = true;
            bricks[10].rightWall = true;
            bricks[11].leftWall = true;
            bricks[11].bottomWall = true;
            bricks[11].rightWall = true;
            bricks[12].leftWall = true;
            bricks[12].rightWall = true;
            bricks[12].upperWall = true;
            bricks[12].bottomWall = true;
            bricks[13].leftWall = true;
            bricks[16].upperWall = true;
            bricks[17].upperWall = true;
            bricks[18].bottomWall = true;

            bricks[20].rightWall = true;

            bricks[22].rightWall = true;
            bricks[23].rightWall = true;
            bricks[23].leftWall = true;
            bricks[23].upperWall = true;
            bricks[24].leftWall = true;

            //draw walls inside maze
            for (int i = 0; i <= 24; i++)
            {
                if (bricks[i].leftWall == true)
                    gObject.DrawLine(blackPen, bricks[i].x - 40, bricks[i].y + 40, bricks[i].x - 40, bricks[i].y - 40);
                if (bricks[i].rightWall == true)
                    gObject.DrawLine(blackPen, bricks[i].x + 40, bricks[i].y + 40, bricks[i].x + 40, bricks[i].y - 40);
                if (bricks[i].upperWall == true)
                    gObject.DrawLine(blackPen, bricks[i].x - 40, bricks[i].y - 40, bricks[i].x + 40, bricks[i].y - 40);
                if (bricks[i].bottomWall == true)
                    gObject.DrawLine(blackPen, bricks[i].x - 40, bricks[i].y + 40, bricks[i].x + 40, bricks[i].y + 40);
            }

            //draw exit
            gObject.DrawLine(whitePen, bricks[21].x - 15, bricks[21].y + 40, bricks[21].x + 15, bricks[21].y + 40);

        }

        private async void solveMazeButton_Click(object sender, EventArgs e)
        {
            //define exit and starting points
            exitBrickIndex = 21;
            startingBrickIndex = 7;

            //define starting point
            //create drawing point for solver position
            Solver mySolver = new Solver(bricks[startingBrickIndex].x, bricks[startingBrickIndex].y, Direction.North + 3);
            Rectangle recti = new Rectangle(1, 40, 1, 1); //for solver position
            recti.X = mySolver.x;
            recti.Y = mySolver.y;
            gObject.DrawEllipse(blackPen, recti);

            mazeTrace.AppendText("Maze ready\n");
            mazeTrace.AppendText("Current position:" + mySolver.x + "," + mySolver.y + " brick nr " + startingBrickIndex + "\n");

            if (mySolver.x != bricks[exitBrickIndex].x && mySolver.y == bricks[exitBrickIndex].y)
            {
                MessageBox.Show("Starting bricks is exit brick");
                mazeTrace.AppendText("Exit found\n");
            }

            //index affect on solver position
            //+1 move solver one brick right for facing east
            //-1 move solver one brick left for facing west only
            //-5 move solver one brick up for facing north only
            //+5 move solver one brick down for facing south only
            int currentBrickIndex = startingBrickIndex;

            //TODO what if starting point is near enclosed square and solve keeps turning around - find workaround
            #region            
            while (currentBrickIndex != exitBrickIndex)
            {
                //1 facing north cases
                if ((bricks[currentBrickIndex].rightWall == true || bricks[currentBrickIndex].leftWall == true) && bricks[currentBrickIndex].upperWall == false && mySolver.facingAt == Direction.North && bricks[currentBrickIndex - 5].rightWall == true)
                {
                    recti.X = bricks[currentBrickIndex].x;
                    recti.Y = bricks[currentBrickIndex].y;
                    gObject.DrawEllipse(whitePen, recti);
                    currentBrickIndex -= 5;
                    mySolver.x = bricks[currentBrickIndex].x;
                    mySolver.y = bricks[currentBrickIndex].y;
                    recti.X = mySolver.x;
                    recti.Y = mySolver.y;
                    gObject.DrawEllipse(blackPen, recti);
                    mySolver.facingAt = Direction.North;
                }

                else if (bricks[currentBrickIndex].rightWall == true && bricks[currentBrickIndex].upperWall == false && mySolver.facingAt == Direction.North && bricks[currentBrickIndex - 5].rightWall == false && bricks[currentBrickIndex + 1].upperWall == false)
                {
                    recti.X = bricks[currentBrickIndex].x;
                    recti.Y = bricks[currentBrickIndex].y;
                    gObject.DrawEllipse(whitePen, recti);
                    currentBrickIndex += 1; //overleap two steps wall and keep right
                    mySolver.x = bricks[currentBrickIndex].x;
                    mySolver.y = bricks[currentBrickIndex].y;
                    recti.X = mySolver.x;
                    recti.Y = mySolver.y;
                    gObject.DrawEllipse(blackPen, recti);
                    mySolver.facingAt = Direction.South;
                }

                else if (bricks[currentBrickIndex].rightWall == true && bricks[currentBrickIndex].upperWall == false && mySolver.facingAt == Direction.North && bricks[currentBrickIndex - 5].rightWall == false)
                {
                    recti.X = bricks[currentBrickIndex].x;
                    recti.Y = bricks[currentBrickIndex].y;
                    gObject.DrawEllipse(whitePen, recti);
                    currentBrickIndex -= 4; //overleap wall and keep right
                    mySolver.x = bricks[currentBrickIndex].x;
                    mySolver.y = bricks[currentBrickIndex].y;
                    recti.X = mySolver.x;
                    recti.Y = mySolver.y;
                    gObject.DrawEllipse(blackPen, recti);
                    mySolver.facingAt = Direction.East;
                }

                else if (bricks[currentBrickIndex].rightWall == true && bricks[currentBrickIndex].upperWall == true && bricks[currentBrickIndex].leftWall == true && mySolver.facingAt == Direction.North)
                {
                    mySolver.facingAt = Direction.South;
                }
                else if (bricks[currentBrickIndex].rightWall == true && bricks[currentBrickIndex].upperWall == true && bricks[currentBrickIndex].leftWall == false && mySolver.facingAt == Direction.North)
                {
                    mySolver.facingAt = Direction.West;
                }
                else if (bricks[currentBrickIndex].upperWall == true && bricks[currentBrickIndex].leftWall == false && bricks[currentBrickIndex].rightWall == false && mySolver.facingAt == Direction.North)
                {
                    mySolver.facingAt = Direction.West;
                }
                else if (bricks[currentBrickIndex].rightWall == false && bricks[currentBrickIndex].upperWall == true && bricks[currentBrickIndex].leftWall == true && bricks[currentBrickIndex].bottomWall == false && mySolver.facingAt == Direction.North)
                {
                    mySolver.facingAt = Direction.South;
                }
                else if (bricks[currentBrickIndex].rightWall == false && bricks[currentBrickIndex].upperWall == true && bricks[currentBrickIndex].leftWall == true && bricks[currentBrickIndex].bottomWall == false && mySolver.facingAt == Direction.North)
                {
                    mySolver.facingAt = Direction.South;
                }

                if (currentBrickIndex == exitBrickIndex)
                {
                    MessageBox.Show("Exit found");
                    mazeTrace.AppendText("Exit found\n");
                    break;

                }

                //facing south cases
                if ((bricks[currentBrickIndex].leftWall || bricks[currentBrickIndex].rightWall == true) && bricks[currentBrickIndex].bottomWall == false && mySolver.facingAt == Direction.South && bricks[currentBrickIndex + 5].leftWall == true)
                {
                    recti.X = bricks[currentBrickIndex].x;
                    recti.Y = bricks[currentBrickIndex].y;
                    gObject.DrawEllipse(whitePen, recti);
                    currentBrickIndex += 5;
                    mySolver.x = bricks[currentBrickIndex].x;
                    mySolver.y = bricks[currentBrickIndex].y;
                    recti.X = mySolver.x;
                    recti.Y = mySolver.y;
                    gObject.DrawEllipse(blackPen, recti);
                    mySolver.facingAt = Direction.South;
                }
                else if ((bricks[currentBrickIndex].leftWall || bricks[currentBrickIndex].rightWall == true) && bricks[currentBrickIndex].bottomWall == false && mySolver.facingAt == Direction.South && bricks[currentBrickIndex + 5].leftWall == false && bricks[currentBrickIndex - 1].bottomWall == false)
                {
                    recti.X = bricks[currentBrickIndex].x;
                    recti.Y = bricks[currentBrickIndex].y;
                    gObject.DrawEllipse(whitePen, recti);
                    currentBrickIndex -= 1; //overleap two steps keep right
                    mySolver.x = bricks[currentBrickIndex].x;
                    mySolver.y = bricks[currentBrickIndex].y;
                    recti.X = mySolver.x;
                    recti.Y = mySolver.y;
                    gObject.DrawEllipse(blackPen, recti);
                    mySolver.facingAt = Direction.North;
                }
                else if ((bricks[currentBrickIndex].leftWall || bricks[currentBrickIndex].rightWall == true) && bricks[currentBrickIndex].bottomWall == false && mySolver.facingAt == Direction.South && bricks[currentBrickIndex + 5].leftWall == false)
                {
                    recti.X = bricks[currentBrickIndex].x;
                    recti.Y = bricks[currentBrickIndex].y;
                    gObject.DrawEllipse(whitePen, recti);
                    currentBrickIndex += 4; //overleap keep right
                    mySolver.x = bricks[currentBrickIndex].x;
                    mySolver.y = bricks[currentBrickIndex].y;
                    recti.X = mySolver.x;
                    recti.Y = mySolver.y;
                    gObject.DrawEllipse(blackPen, recti);
                    mySolver.facingAt = Direction.North;
                }

                else if (bricks[currentBrickIndex].rightWall == true && bricks[currentBrickIndex].leftWall == true && bricks[currentBrickIndex].bottomWall == true && mySolver.facingAt == Direction.South)
                {
                    mySolver.facingAt = Direction.North;
                }
                else if (bricks[currentBrickIndex].rightWall == false && bricks[currentBrickIndex].upperWall == false && bricks[currentBrickIndex].leftWall == true && bricks[currentBrickIndex].bottomWall == true && mySolver.facingAt == Direction.South)
                {
                    mySolver.facingAt = Direction.East;
                }
                else if (bricks[currentBrickIndex].rightWall == true && bricks[currentBrickIndex].upperWall == false && bricks[currentBrickIndex].leftWall == false && bricks[currentBrickIndex].bottomWall == true && mySolver.facingAt == Direction.South)
                {
                    mySolver.facingAt = Direction.East;
                }

                if (currentBrickIndex == exitBrickIndex)
                {
                    MessageBox.Show("Exit found");
                    mazeTrace.AppendText("Exit found\n");
                    break;

                }

                //facing east cases
                if ((bricks[currentBrickIndex].bottomWall == true || bricks[currentBrickIndex].upperWall == true) && bricks[currentBrickIndex].rightWall == false && mySolver.facingAt == Direction.East && bricks[currentBrickIndex + 1].bottomWall == true)
                {
                    recti.X = bricks[currentBrickIndex].x;
                    recti.Y = bricks[currentBrickIndex].y;
                    gObject.DrawEllipse(whitePen, recti);
                    currentBrickIndex += 1;
                    mySolver.x = bricks[currentBrickIndex].x;
                    mySolver.y = bricks[currentBrickIndex].y;
                    recti.X = mySolver.x;
                    recti.Y = mySolver.y;
                    gObject.DrawEllipse(blackPen, recti);
                    mySolver.facingAt = Direction.South;
                }
                else if ((bricks[currentBrickIndex].bottomWall == true || bricks[currentBrickIndex].upperWall == true) && bricks[currentBrickIndex].rightWall == false && mySolver.facingAt == Direction.East && bricks[currentBrickIndex + 1].bottomWall == false && bricks[currentBrickIndex + 6].leftWall == false)
                {
                    recti.X = bricks[currentBrickIndex].x;
                    recti.Y = bricks[currentBrickIndex].y;
                    gObject.DrawEllipse(whitePen, recti);
                    currentBrickIndex += 5; //overleap two steps
                    mySolver.x = bricks[currentBrickIndex].x;
                    mySolver.y = bricks[currentBrickIndex].y;
                    recti.X = mySolver.x;
                    recti.Y = mySolver.y;
                    gObject.DrawEllipse(blackPen, recti);
                    mySolver.facingAt = Direction.West;
                }

                else if ((bricks[currentBrickIndex].bottomWall == true || bricks[currentBrickIndex].upperWall == true) && bricks[currentBrickIndex].rightWall == false && mySolver.facingAt == Direction.East && bricks[currentBrickIndex + 1].bottomWall == false)
                {
                    recti.X = bricks[currentBrickIndex].x;
                    recti.Y = bricks[currentBrickIndex].y;
                    gObject.DrawEllipse(whitePen, recti);
                    currentBrickIndex += 6; //overleap
                    mySolver.x = bricks[currentBrickIndex].x;
                    mySolver.y = bricks[currentBrickIndex].y;
                    recti.X = mySolver.x;
                    recti.Y = mySolver.y;
                    gObject.DrawEllipse(blackPen, recti);
                    mySolver.facingAt = Direction.South;
                }
                else if (bricks[currentBrickIndex].rightWall == true && bricks[currentBrickIndex].bottomWall == true && bricks[currentBrickIndex].upperWall == true && mySolver.facingAt == Direction.East)
                {
                    mySolver.facingAt = Direction.East;
                }
                else if (bricks[currentBrickIndex].rightWall == false && bricks[currentBrickIndex].bottomWall == false && bricks[currentBrickIndex].upperWall == false && bricks[currentBrickIndex].leftWall == true && mySolver.facingAt == Direction.East)
                {
                    mySolver.facingAt = Direction.North;
                }
                else if (bricks[currentBrickIndex].rightWall == true && bricks[currentBrickIndex].bottomWall == true && bricks[currentBrickIndex].upperWall == false && mySolver.facingAt == Direction.East)
                {
                    mySolver.facingAt = Direction.North;
                }

                if (currentBrickIndex == exitBrickIndex)
                {
                    MessageBox.Show("Exit found");
                    mazeTrace.AppendText("Exit found\n");
                    break;

                }

                //facing west cases
                if ((bricks[currentBrickIndex].upperWall == true || bricks[currentBrickIndex].bottomWall == true) && bricks[currentBrickIndex].leftWall == false && mySolver.facingAt == Direction.West && bricks[currentBrickIndex - 1].upperWall == true)
                {
                    recti.X = bricks[currentBrickIndex].x;
                    recti.Y = bricks[currentBrickIndex].y;
                    gObject.DrawEllipse(whitePen, recti);
                    currentBrickIndex -= 1;
                    mySolver.x = bricks[currentBrickIndex].x;
                    mySolver.y = bricks[currentBrickIndex].y;
                    recti.X = mySolver.x;
                    recti.Y = mySolver.y;
                    gObject.DrawEllipse(blackPen, recti);
                    mySolver.facingAt = Direction.West;
                }
                else if ((bricks[currentBrickIndex].upperWall == true || bricks[currentBrickIndex].bottomWall == true) && bricks[currentBrickIndex].leftWall == false && mySolver.facingAt == Direction.West && bricks[currentBrickIndex - 1].upperWall == false && bricks[currentBrickIndex - 6].rightWall == false)
                {
                    recti.X = bricks[currentBrickIndex].x;
                    recti.Y = bricks[currentBrickIndex].y;
                    gObject.DrawEllipse(whitePen, recti);
                    currentBrickIndex -= 5; //two steps overleap
                    mySolver.x = bricks[currentBrickIndex].x;
                    mySolver.y = bricks[currentBrickIndex].y;
                    recti.X = mySolver.x;
                    recti.Y = mySolver.y;
                    gObject.DrawEllipse(blackPen, recti);
                    mySolver.facingAt = Direction.North;
                }
                else if ((bricks[currentBrickIndex].upperWall == true || bricks[currentBrickIndex].bottomWall == true) && bricks[currentBrickIndex].leftWall == false && mySolver.facingAt == Direction.West && bricks[currentBrickIndex - 1].upperWall == false)
                {
                    recti.X = bricks[currentBrickIndex].x;
                    recti.Y = bricks[currentBrickIndex].y;
                    gObject.DrawEllipse(whitePen, recti);
                    currentBrickIndex -= 6; //overleap
                    mySolver.x = bricks[currentBrickIndex].x;
                    mySolver.y = bricks[currentBrickIndex].y;
                    recti.X = mySolver.x;
                    recti.Y = mySolver.y;
                    gObject.DrawEllipse(blackPen, recti);
                    mySolver.facingAt = Direction.North;
                }
                else if (bricks[currentBrickIndex].upperWall == true && bricks[currentBrickIndex].leftWall == true && bricks[currentBrickIndex].bottomWall == true && mySolver.facingAt == Direction.West)
                {
                    mySolver.facingAt = Direction.East;
                }
                else if (bricks[currentBrickIndex].upperWall == true && bricks[currentBrickIndex].leftWall == true && bricks[currentBrickIndex].bottomWall == false && mySolver.facingAt == Direction.West)
                {
                    mySolver.facingAt = Direction.South;
                }

                if (currentBrickIndex == exitBrickIndex)
                {
                    MessageBox.Show("Exit found");
                    mazeTrace.AppendText("Exit found\n");
                    break;
                }

                mazeTrace.AppendText("Current position:" + mySolver.x + "," + mySolver.y + " brick nr " + currentBrickIndex + "\n");
                await PutTaskDelay();

            }
            #endregion

        }

        private void exitProgramButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
