using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentProject.Code.GameObjects
{
    internal class Grid : GameObject
    {
        const int gridBoxSize = 56;
        const int screenDimensionsX = 1176;
        const int screenDimensionsY = 952;
        int screenBufferX = ((int)Settings.ScreenDimensions.X - screenDimensionsX) / 2;
        int screenBufferY = ((int)Settings.ScreenDimensions.Y - screenDimensionsY) / 2;
        const int gridWidth = (screenDimensionsX / gridBoxSize);
        const int gridLength = (screenDimensionsY / gridBoxSize);
        private bool[,] TheGrid = new bool[gridWidth, gridLength];

        public Grid() 
        {
            SetSprite("Grid");
        }

        public override void Update(float deltaTime)
        {
           
        }

        public bool GetGridLocked(int x, int y)
        {
            if (x > 21)
            {
                x = 21;
            }
            if (y > 15)
            {
                y = 15;
            }
            if (TheGrid[x, y] == true)
            {
                return true;
            } else
            {
                return false;
            }
        }

        public void SetGridLocked(int x, int y, bool state)
        {
            TheGrid[x, y] = state;
        }

        //Used to get the center x coordinate of a grid square
        public int GetGridXLocation(int gridX)
        {
            if (gridX > gridWidth)
            {
                gridX = gridWidth;
            }
            return screenBufferX + (gridBoxSize * (gridX - 1)) + (gridBoxSize / 2);
        }

        //Used to get the center y coordinate of a grid square
        public int GetGridYLocation(int gridY)
        {
            if (gridY > gridLength)
            {
                gridY = gridLength;
            }
            return (int)Settings.ScreenDimensions.Y - screenBufferY - (gridBoxSize * (gridY - 1)) - (gridBoxSize / 2);
        }


        //Used to find the coresponding Grid X location based on the x-coordinate.
        public int FindGridX(int x)
        {
            x = x - screenBufferX;
            return (x / gridBoxSize) + 1;
        }
        //Used to find the coresponding Grid Y location based on the y-coordinate.
        public int FindGridY(int y)
        {
            y = y - screenBufferY;
            return (y / gridBoxSize) + 1;
        }
    }
}
