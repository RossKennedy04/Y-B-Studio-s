using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentProject.Code.Screens
{
    internal class MainMenu : Screen
    {
        private MenuCursor _cursor;
        private MenuObject _object1;
        private MenuObject _object2;
        private MenuObject _object3;

        public override void Start(Core core)
        {
            base.Start(core);
            // TODO: Add your Screen starting code below here
            Settings.BackgroundFill = Colour.Black;
            Settings.IsMouseVisible = true;

            _cursor = new MenuCursor();
            AddObject(_cursor, 0, 0);
            GameInput.SetMousePosition(Settings.ScreenDimensions / 2);

            _object1 = new MenuObject();
            _object2 = new MenuObject();
            _object3 = new MenuObject();
            AddObject(_object1, 0, 0);
            AddObject(_object2, 0, 96);
            AddObject(_object3, 0, 176);
        }

        public override void Update(float deltaTime)
        {
            base.Update(deltaTime);
            // TODO: Add your Screen updated code below here

        }
    }
}
