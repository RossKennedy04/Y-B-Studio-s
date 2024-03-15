global using Vector2 = Microsoft.Xna.Framework.Vector2;
global using Colour = Microsoft.Xna.Framework.Color;
global using MonoGameEngine;

global using StudentProject.Code.GameObjects;
global using StudentProject.Code.Screens;
using System;


namespace StudentProject
{
    public static class Program
    {
        [STAThread]
        static void Main()
        {
            using (var game = new MyGame())
                game.Run();
        }
    }
}

namespace StudentProject.Code.GameObjects
{
    // For global using purposes
}
