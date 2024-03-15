using StudentProject.Code.Screens;
using System;

namespace StudentProject
{
    public class MyGame : Core
    { 
        protected override void Initialize()
        {
            Window.Title = "Heart of the Machine";
            // TODO: Add your game's initialization logic below here
            StartScreen<MainMenu>();
            base.Initialize();
        }
    }
}
