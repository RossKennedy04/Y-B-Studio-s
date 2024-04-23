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
        Grid myGrid = new Grid();

        Level_1_Bounds bounds = new Level_1_Bounds();
        Janitor janitor = new Janitor();
        PearEnemy pearEnemy = new PearEnemy();
        OrangeEnemy orangeEnemy = new OrangeEnemy();
        CherryEnemy cherryEnemy = new CherryEnemy();
        BananaEnemy bananaEnemy = new BananaEnemy();

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
            AddObject(janitor, myGrid.GetGridXLocation(11), myGrid.GetGridYLocation(7));

            orangeEnemy.GetSprite().SetOrigin(0.5f, 0.5f);
            AddObject(orangeEnemy, myGrid.GetGridXLocation(4), myGrid.GetGridYLocation(2));
            cherryEnemy.GetSprite().SetOrigin(0.5f, 0.5f);
            AddObject(cherryEnemy, myGrid.GetGridXLocation(7), myGrid.GetGridYLocation(13));
            bananaEnemy.GetSprite().SetOrigin(0.5f, 0.5f);
            AddObject(bananaEnemy, myGrid.GetGridXLocation(15), myGrid.GetGridYLocation(13));
            pearEnemy.GetSprite().SetOrigin(0.5f, 0.5f);
            AddObject(pearEnemy, myGrid.GetGridXLocation(11), myGrid.GetGridYLocation(8));

            GenerateWallsBox(11, 7, 3, 3, 2);
            GenerateWallsBox(11, 7, 7, 7, 1);
            GenerateWallsBox(11, 9, 19, 15, 5);
            GenerateWallsBox(4, 12, 1, 5, 2);
        }

        public override void Update(float deltaTime)
        {
            base.Update(deltaTime);

            /**
            if (Core.GetRandomNumber(25) == 0)
            {
                int randomX = Core.GetRandomNumber(gridWidth) + 1;
                int randomY = Core.GetRandomNumber(gridLength) + 1;
                if (myGrid.GetGridLocked((randomX - 1), (randomY - 1)) != true)
                {
                    Crumb newCrumb = new Crumb();
                    newCrumb.GetSprite().SetOrigin(0.5f, 0.5f);
                    newCrumb.GetSprite().SetRotation(((Core.GetRandomNumber(3)) * 90));
                    AddObject(newCrumb, myGrid.GetGridXLocation(randomX), myGrid.GetGridYLocation(randomY));
                }
            }
            **/

        }

        // WallOpening 1 = Right, 2 = Bottom, 3 = Left, 4 = Top, 5 = None
        // GenerateWallsBox(Grid Location X, Grid Location Y, Size of grid X, Size of Grid Y, Opening in Wall? See above ^)
        private void GenerateWallsBox(int gridX, int gridY, int sizeX, int sizeY, int wallOpening)
        {
            if (sizeX < 1)
            {
                sizeX = 1;
            }
            if (sizeY < 1)
            {
                sizeY = 1;
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
                        AddObject(corner, myGrid.GetGridXLocation(currentX), myGrid.GetGridYLocation(currentY));
                        myGrid.SetGridLocked((currentX - 1), (currentY - 1), true);
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
                            AddObject(newWall, myGrid.GetGridXLocation(currentX), myGrid.GetGridYLocation(currentY));
                            myGrid.SetGridLocked((currentX - 1), (currentY - 1), true);
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
