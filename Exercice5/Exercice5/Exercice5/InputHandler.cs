using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Exercice5
{
    /// <summary>
    /// InputHandler handles both the gamepad input and the keyboard input
    /// according to what the player is playing with.
    /// </summary>
    public class InputHandler
    {
        private GamePadState oldGamePadState;
        private GamePadState currentGamePadState;

        private KeyboardState oldKeyboardState;
        private KeyboardState currentKeyboardState;

        public enum GamePadThumbSticksSide { LEFT, RIGHT }

        /// <summary>
        /// Initializes a new instance of the <see cref="InputHandler"/> class.
        /// </summary>
        public InputHandler()
        {
            currentGamePadState = GamePad.GetState(PlayerIndex.One);
            currentKeyboardState = Keyboard.GetState();
        }

        /// <summary>
        /// Updates this instance.
        /// </summary>
        public void Update()
        {
            // Must be done after handling all inputs
            oldGamePadState = currentGamePadState;
            oldKeyboardState = currentKeyboardState;
            currentGamePadState = GamePad.GetState(PlayerIndex.One);
            currentKeyboardState = Keyboard.GetState();
        }

        /// <summary>
        /// Determines whether [is game pad one connected].
        /// </summary>
        /// <returns></returns>
        public bool IsGamePadOneConnected()
        {
            return currentGamePadState.IsConnected;
        }

        /// <summary>
        /// Determines whether [is input down] [the specified _input].
        /// </summary>
        /// <param name="_input">The _input.</param>
        /// <returns></returns>
        public bool IsInputDown(Keys _input)
        {
            return currentKeyboardState.IsKeyDown(_input);
        }

        /// <summary>
        /// Determines whether [is input down] [the specified _input].
        /// </summary>
        /// <param name="_input">The _input.</param>
        /// <returns></returns>
        public bool IsInputDown(Buttons _input)
        {
            return currentGamePadState.IsButtonDown(_input);
        }

        /// <summary>
        /// Determines whether [is input up] [the specified _input].
        /// </summary>
        /// <param name="_input">The _input.</param>
        /// <returns></returns>
        public bool IsInputUp(Keys _input)
        {
            return currentKeyboardState.IsKeyUp(_input);
        }

        /// <summary>
        /// Determines whether [is input up] [the specified _input].
        /// </summary>
        /// <param name="_input">The _input.</param>
        /// <returns></returns>
        public bool IsInputUp(Buttons _input)
        {
            return currentGamePadState.IsButtonUp(_input);
        }

        /// <summary>
        /// Determines whether [is input pressed] [the specified _input].
        /// </summary>
        /// <param name="_input">The _input.</param>
        /// <returns></returns>
        public bool IsInputPressed(Keys _input)
        {
            if (currentKeyboardState.IsKeyDown(_input) == true && oldKeyboardState.IsKeyDown(_input) == false)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Determines whether [is input pressed] [the specified _input].
        /// </summary>
        /// <param name="_input">The _input.</param>
        /// <returns></returns>
        public bool IsInputPressed(Buttons _input)
        {
            if (currentGamePadState.IsButtonDown(_input) == true && oldGamePadState.IsButtonDown(_input) == false)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Determines whether [is input released] [the specified _input].
        /// </summary>
        /// <param name="_input">The _input.</param>
        /// <returns></returns>
        public bool IsInputReleased(Keys _input)
        {
            if (currentKeyboardState.IsKeyDown(_input) == false && oldKeyboardState.IsKeyDown(_input) == true)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Determines whether [is input released] [the specified _input].
        /// </summary>
        /// <param name="_input">The _input.</param>
        /// <returns></returns>
        public bool IsInputReleased(Buttons _input)
        {
            if (currentGamePadState.IsButtonDown(_input) == false && oldGamePadState.IsButtonDown(_input) == true)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Determines whether [is thumb stick down] [the specified _side].
        /// </summary>
        /// <param name="_side">The _side.</param>
        /// <param name="_limit">The _limit.</param>
        /// <returns></returns>
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

        /// <summary>
        /// Determines whether [is thumb stick up] [the specified _side].
        /// </summary>
        /// <param name="_side">The _side.</param>
        /// <param name="_limit">The _limit.</param>
        /// <returns></returns>
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

        /// <summary>
        /// Gets the game pad joystick.
        /// </summary>
        /// <returns></returns>
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
