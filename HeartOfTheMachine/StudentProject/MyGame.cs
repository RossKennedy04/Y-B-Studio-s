using StudentProject.Code.Screens;
using System;

namespace StudentProject
{
    public class MyGame : Core
    { 
        protected override void Initialize()
        {
            Window.Title = "MyGame";
            // TODO: Add your game's initialization logic below here
            StartScreen<MyWorld>();
            base.Initialize();
        }
    }
}
