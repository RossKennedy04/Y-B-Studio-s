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
        private MenuObject[] _menuObjects = new MenuObject[3];
        private int _optionSelection = 0;

        public override void Start(Core core)
        {
            base.Start(core);
            // TODO: Add your Screen starting code below here
            Settings.BackgroundFill = Colour.Black;
            Settings.IsMouseVisible = true;

            _cursor = new MenuCursor();
            AddObject(_cursor, 0, 0);
            GameInput.SetMousePosition(Settings.ScreenDimensions / 2);

            for(int i = 0; i < _menuObjects.Length; i++)
            {
                _menuObjects[i] = new MenuObject();
                AddObject(_menuObjects[i], (((int)Settings.ScreenDimensions.X / 2) - (_menuObjects[i].GetSprite().GetWidth() / 2)), (500 + (96 * i)));
                _menuObjects[i].SetVisible(false);
            }
            _menuObjects[_optionSelection].SetVisible(true);
        }
        public override void Update(float deltaTime)
        {
            base.Update(deltaTime);
            // TODO: Add your Screen updated code below here
            if (GameInput.IsKeyPressed("Down") || GameInput.IsKeyPressed("S"))
            {
                _menuObjects[_optionSelection].SetVisible(false);
                if (_optionSelection < 2)
                {
                    _optionSelection++;
                } else
                {
                    _optionSelection = 0;
                }
                _menuObjects[_optionSelection].SetVisible(true);
            }
            if (GameInput.IsKeyPressed("Up") || GameInput.IsKeyPressed("W"))
            {
                _menuObjects[_optionSelection].SetVisible(false);
                if (_optionSelection > 0)
                {
                    _optionSelection--;
                }
                else
                {
                    _optionSelection = 2;
                }
                _menuObjects[_optionSelection].SetVisible(true);
            }

        }
    }
}
