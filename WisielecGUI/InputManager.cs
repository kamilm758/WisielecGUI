using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WisielecGUI
{
    public static class InputManager
    {
        public static KeyboardState CurrentState;
        public static KeyboardState PreviousState;
        public static MouseState CurrentStateMouse;
        public static MouseState PreviousStateMouse;
        private static StringBuilder builder = new StringBuilder();

        public static void Update()
        {
            PreviousState = CurrentState;
            CurrentState = Keyboard.GetState();
            PreviousStateMouse = CurrentStateMouse;
            CurrentStateMouse = Mouse.GetState();
        }

        public static List<Keys> GetPressedKeys()
        {
            List<Keys> keys = new List<Keys>();
            if (CurrentState.GetPressedKeys().Length == 0)
            {
                return keys;
            }
            else
            {
                foreach(var key in CurrentState.GetPressedKeys())
                {
                    if (PreviousState.IsKeyUp(key))
                    {
                        keys.Add(key);
                    }
                }
            }
            return keys;
        }

        public static string GetNapis()
        {
            var keys = InputManager.GetPressedKeys();
            foreach (var key in keys)
            {
                if (key == Keys.Back)
                {
                    if (builder.Length == 0)
                        continue;

                    builder.Remove(builder.Length - 1, 1);
                }
                else if(key!=Keys.LeftShift && key!=Keys.RightShift)
                {
                    if (ContainsShift())
                        builder.Append(key.ToString());
                    else
                        builder.Append(key.ToString().ToLower());
                }
            }
            if (builder.Length == 0)
                return "";
            return builder.ToString();
        }


        public static string GetJednaLitere()
        {
            var keys = InputManager.GetPressedKeys();
            foreach (var key in keys)
            {
                if (key == Keys.Back)
                {
                    if (builder.Length == 0)
                        continue;
                }
                else if (key != Keys.LeftShift && key != Keys.RightShift)
                {
                    if(builder.Length!=0)
                        builder.Remove(builder.Length - 1, 1);
                    builder.Append(key.ToString().ToLower());
                }
            }
            if (builder.Length == 0)
                return "";
            return builder.ToString();
        }

        public static bool LeftButtonPressed()
        {
            if(CurrentStateMouse.LeftButton== ButtonState.Pressed && PreviousStateMouse.LeftButton == ButtonState.Released)
            {
                return true;
            }
            return false;
        }

        private static bool ContainsShift()
        {
            var keys = PreviousState.GetPressedKeys();
            foreach(var key in keys)
            {
                if(key==Keys.RightShift || key==Keys.LeftShift)
                {
                    return true;
                }
            }
            return false;
        }
        public static void NapisFlush()
        {
            builder.Clear();
        }
    }
}
