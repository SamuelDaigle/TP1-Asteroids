using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Exercice5
{
    public class InputHandler
    {
        private GamePadState oldGamePadState;
        private GamePadState currentGamePadState;

        private KeyboardState oldKeyboardState;
        private KeyboardState currentKeyboardState;

        public enum GamePadThumbSticksSide { LEFT, RIGHT }

        public InputHandler()
        {
            currentGamePadState = GamePad.GetState(PlayerIndex.One);
            currentKeyboardState = Keyboard.GetState();
        }

        public void Update()
        {
            // Must be done after handling all inputs
            oldGamePadState = currentGamePadState;
            oldKeyboardState = currentKeyboardState;
            currentGamePadState = GamePad.GetState(PlayerIndex.One);
            currentKeyboardState = Keyboard.GetState();
        }

        public bool IsGamePadOneConnected()
        {
            return currentGamePadState.IsConnected;
        }

        public bool IsInputDown(Keys _input)
        {
            return currentKeyboardState.IsKeyDown(_input);
        }

        public bool IsInputDown(Buttons _input)
        {
            return currentGamePadState.IsButtonDown(_input);
        }

        public bool IsInputUp(Keys _input)
        {
            return currentKeyboardState.IsKeyUp(_input);
        }

        public bool IsInputUp(Buttons _input)
        {
            return currentGamePadState.IsButtonUp(_input);
        }

        public bool IsInputPressed(Keys _input)
        {
            if (currentKeyboardState.IsKeyDown(_input) == true && oldKeyboardState.IsKeyDown(_input) == false)
            {
                return true;
            }
            return false;
        }

        public bool IsInputPressed(Buttons _input)
        {
            if (currentGamePadState.IsButtonDown(_input) == true && oldGamePadState.IsButtonDown(_input) == false)
            {
                return true;
            }
            return false;
        }

        public bool IsInputReleased(Keys _input)
        {
            if (currentKeyboardState.IsKeyDown(_input) == false && oldKeyboardState.IsKeyDown(_input) == true)
            {
                return true;
            }
            return false;
        }

        public bool IsInputReleased(Buttons _input)
        {
            if (currentGamePadState.IsButtonDown(_input) == false && oldGamePadState.IsButtonDown(_input) == true)
            {
                return true;
            }
            return false;
        }

        public bool IsThumbStickDown(GamePadThumbSticksSide _side, float _limit)
        {
            Vector2 currentOrientation;
            Vector2 oldOrientation;
            if (_side == GamePadThumbSticksSide.LEFT)
            {
                currentOrientation = currentGamePadState.ThumbSticks.Left;
                oldOrientation = oldGamePadState.ThumbSticks.Left;
            }
            else // Right
            {
                currentOrientation = currentGamePadState.ThumbSticks.Right;
                oldOrientation = oldGamePadState.ThumbSticks.Right;
            }

            if (currentOrientation.Y < _limit && oldOrientation.Y > _limit)
            {
                return true;
            }

            return false;
        }

        public bool IsThumbStickUp(GamePadThumbSticksSide _side, float _limit)
        {
            Vector2 currentOrientation;
            Vector2 oldOrientation;
            if (_side == GamePadThumbSticksSide.LEFT)
            {
                currentOrientation = currentGamePadState.ThumbSticks.Left;
                oldOrientation = oldGamePadState.ThumbSticks.Left;
            }
            else // Right
            {
                currentOrientation = currentGamePadState.ThumbSticks.Right;
                oldOrientation = oldGamePadState.ThumbSticks.Right;
            }

            if (currentOrientation.Y > _limit && oldOrientation.Y < _limit)
            {
                return true;
            }

            return false;
        }

        public GamePadThumbSticks GetGamePadJoystick()
        {
            return currentGamePadState.ThumbSticks;
        }

        public List<Keys> GetPressedKeys()
        {
            List<Keys> pressedKeys = new List<Keys>();
            foreach (Keys key in currentKeyboardState.GetPressedKeys())
            {
                if (key != Keys.None)
                {
                    if (oldKeyboardState.IsKeyUp(key))
                    {
                        pressedKeys.Add(key);
                    }
                }  
            }
            return pressedKeys;
        }

    }
}
