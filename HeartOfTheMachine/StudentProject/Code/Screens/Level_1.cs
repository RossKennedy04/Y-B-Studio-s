using StudentProject.Code.GameObjects;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentProject.Code.Screens
{
    internal class Level_1 : Screen
    {
        // USE THIS FOR CENTER OF THE SCREEN
        // X COORDINATE: (screenDimensionsX / 2) + screenBufferX
        // Y COORDINATE: (screenDimensionsY / 2) + screenBufferY

        //Const is used because this variable will not be changed and so it can be used in the array.
        //Reference: https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/const
        //https://stackoverflow.com/questions/72981795/how-to-make-a-2d-grid-with-c-sharp
        const int gridBoxSize = 56;
        const int screenDimensionsX = 1176;
        const int screenDimensionsY = 952;
        int screenBufferX = ((int)Settings.ScreenDimensions.X - screenDimensionsX) / 2;
        int screenBufferY = ((int)Settings.ScreenDimensions.Y - screenDimensionsY) / 2;
        const int gridWidth = (screenDimensionsX / gridBoxSize);
        const int gridLength = (screenDimensionsY / gridBoxSize);
        private bool[,] Grid = new bool[gridWidth, gridLength];

        Level_1_Bounds bounds = new Level_1_Bounds();
        Janitor janitor = new Janitor();

        public override void Start(Core core)
        {
            Settings.BackgroundFill = Colour.White;
            Settings.IsMouseVisible = true;
            base.Start(core);
            bounds.GetSprite().SetOrigin(0.5f, 0.5f);
            AddObject(bounds, (int)Settings.ScreenDimensions.X / 2, (int)Settings.ScreenDimensions.Y / 2);

            //Temporary so I can visualise the grid
            int startingGridPointX = screenBufferX;
            int startingGridPointY = screenBufferY;
            for (int i = 0; i < gridWidth; i++)
            {
                for (int j = 0; j < gridLength; j++)
                {
                    Grid grid = new Grid();
                    grid.GetSprite().SetOrigin(0.0f, 0.0f);
                    AddObject(grid, startingGridPointX + (i * gridBoxSize), startingGridPointY + (j * gridBoxSize));
                }
            }
            janitor.GetSprite().SetOrigin(0.5f, 0.5f);
            AddObject(janitor, GetGridXLocation(11), GetGridYLocation(6));
            GenerateWallsBox(11, 6, 3, 3, 2);
            GenerateWallsBox(11, 6, 7, 7, 1);

        }

        public override void Update(float deltaTime)
        {
            base.Update(deltaTime);
            // TODO: Add your Screen updated code below here
            if (Core.GetRandomNumber(25) == 0)
            {
                int randomX = Core.GetRandomNumber(gridWidth);
                int randomY = Core.GetRandomNumber(gridLength);
                if (Grid[randomX, randomY] != true)
                {
                    Crumb newCrumb = new Crumb();
                    newCrumb.GetSprite().SetOrigin(0.5f, 0.5f);
                    AddObject(newCrumb, GetGridXLocation(randomX), GetGridYLocation(randomY));
                }
            }

        }

        //Used to get the center x coordinate of a grid square
        private int GetGridXLocation(int gridX)
        {
            if (gridX > gridWidth)
            {
                gridX = gridWidth;
            }
            return screenBufferX + (gridBoxSize * (gridX - 1)) + (gridBoxSize / 2);
        }

        //Used to get the center y coordinate of a grid square
        private int GetGridYLocation(int gridY)
        {
            if (gridY > gridLength)
            {
                gridY = gridLength;
            }
            return (int)Settings.ScreenDimensions.Y - screenBufferY - (gridBoxSize * (gridY - 1)) - (gridBoxSize / 2);
        }


        //Used to find the coresponding Grid X location based on the x-coordinate.
        private int FindGridX(int x)
        {
            x = x - screenBufferX;
            return (x / gridBoxSize) + 1;
        }
        //Used to find the coresponding Grid Y location based on the y-coordinate.
        private int FindGridY(int y)
        {
            y = y - screenBufferY;
            return (y / gridBoxSize) + 1;
        }

        private bool IsWallPlaceable(int x, int y)
        {

            return true;
        }

        // WallOpening 1 = Right, 2 = Bottom, 3 = Left, 4 = Top, 5 = None
        // GenerateWallsBox(Grid Location X, Grid Location Y, Size of grid X, Size of Grid Y, Opening in Wall? See above ^)
        private void GenerateWallsBox(int gridX, int gridY, int sizeX, int sizeY, int wallOpening)
        {
            if (sizeX < 2)
            {
                sizeX = 2;
            }
            if (sizeY < 2)
            {
                sizeY = 2;
            }

            int centerWallX = (sizeX / 2) + 1;
            int centerWallY = (sizeY / 2) + 1;
            float rotationCorner = 0.0f;
            float rotationLength = 90.0f;
            Boolean skipWallGeneration = false;
            // Starts at top right
            Debug.WriteLine(gridX + ((sizeX / 2) + 1));
            int currentX = gridX + ((sizeX / 2) + 1);
            int currentY = gridY + ((sizeY / 2) + 1);
            int size;
            int centerWall;
            for (int side = 1; side < 5; side++)
            {
                rotationCorner = ((side - 1) * 90.0f);
                rotationLength = (side * 90.0f);
                if (side == 1 || side == 3)
                {
                    size = sizeY;
                    centerWall = centerWallY;
                }
                else
                {
                    size = sizeX;
                    centerWall = centerWallX;
                }
                for (int wall = 0; wall < size + 1; wall++)
                {
                    if (wall == 0)
                    {
                        WallCorner corner = new WallCorner();
                        corner.GetSprite().SetRotation(rotationCorner);
                        corner.GetSprite().SetOrigin(0.5f, 0.5f);
                        AddObject(corner, GetGridXLocation(currentX), GetGridYLocation(currentY));
                        Grid[currentX, currentY] = true;
                    }
                    else
                    {
                        skipWallGeneration = false;
                        if (centerWall == wall && wallOpening == side)
                        {
                            skipWallGeneration = true;
                        }
                        if (skipWallGeneration == false)
                        {
                            WallLength newWall = new WallLength();
                            newWall.GetSprite().SetRotation(rotationLength);
                            newWall.GetSprite().SetOrigin(0.5f, 0.5f);
                            AddObject(newWall, GetGridXLocation(currentX), GetGridYLocation(currentY));
                            Grid[currentX, currentY] = true;
                        }
                        skipWallGeneration = false;

                    }
                    if (side == 1)
                    {
                        currentY--;
                    }
                    else if (side == 3)
                    {
                        currentY++;
                    }
                    if (side == 2)
                    {
                        currentX--;
                    }
                    else if (side == 4)
                    {
                        currentX++;
                    }
                }
            }
        }
     }
 }
