﻿/* This code is licensed under the ekzFreeUse license
 * If a license wasn't included with the program,
 * refer to https://github.com/9xbt/SVGAIITerminal/blob/main/LICENSE.md */

using System;
using System.Collections.Generic;
using Cosmos.System;
using PrismAPI.Graphics;

namespace OpenNIX.GUI
{
    public class Window
    {
        // See https://github.com/9xbt/GoOS/blob/Development/GoOS/GUI/Window.cs for the original window drag code.

        private int dragStartX, dragStartY, dragStartMouseX, dragStartMouseY;
        private bool dragging = false;

        protected static Color BorderColor1 = new Color(150, 150, 150);
        protected static Color BorderColor2 = new Color(200, 200, 200);

        public int X, Y;
        public ushort Width, Height;
        public string Name;
        public bool Movable = true;

        public List<Control> Controls;

        public bool Focused
        {
            get
            {
                return WindowManager.FocusedWindow == this;
            }
        }

        public bool IsMouseOver
        {
            get
            {
                return MouseManager.X > X && MouseManager.X < X + Width && MouseManager.Y > Y && MouseManager.Y < Y + Height;
            }
        }

        public Canvas Contents;

        public Window(int X, int Y, int Width, int Height, string Name = "No name")
        {
            this.X = X;
            this.Y = Y;
            this.Width = (ushort)Width;
            this.Height = (ushort)Height;
            this.Name = Name;

            Contents = new Canvas(this.Width, this.Height);
            Controls = new List<Control>();

            Render();
        }

        public virtual void Render()
        {
            Contents.DrawRectangle(1, 1, Convert.ToUInt16(Contents.Width - 3), Convert.ToUInt16(Contents.Height - 3), 0, BorderColor1);
            Contents.DrawRectangle(0, 0, Convert.ToUInt16(Contents.Width - 1), Convert.ToUInt16(Contents.Height - 1), 0, BorderColor2);
            Contents[Contents.Width - 2, Contents.Height - 2] = BorderColor1;
            Contents[Contents.Width - 1, Contents.Height - 1] = BorderColor2;

            foreach (Control control in Controls)
            {
                if (control != null)
                {
                    Contents.DrawImage(control.X, control.Y, control.Contents, false);
                }
            }
        }

        public virtual void Update()
        {
            if (Movable && IsMouseOver && Focused && MouseManager.LastMouseState == MouseState.None && MouseManager.MouseState == MouseState.Left)
            {
                dragStartX = X;
                dragStartY = Y;
                dragStartMouseX = (int)MouseManager.X;
                dragStartMouseY = (int)MouseManager.Y;
                dragging = true;
            }

            if (dragging)
            {
                X = (int)(dragStartX + (MouseManager.X - dragStartMouseX));
                Y = (int)(dragStartY + (MouseManager.Y - dragStartMouseY));
                MouseDriver.Mouse = Resources.MouseDrag;
                MouseDriver.MouseOffsetX = 7;
                MouseDriver.MouseOffsetY = 7;
            }

            if (IsMouseOver && !Focused)
            {
                MouseDriver.Mouse = Resources.Link;
                MouseDriver.MouseOffsetY = 1;
            }

            if (MouseManager.MouseState == MouseState.None)
            {
                dragging = false;
            }

            if (IsMouseOver && MouseManager.LastMouseState == MouseState.Left && MouseManager.MouseState == MouseState.None && MouseDriver.Mouse == Resources.Link)
            {
                WindowManager.MoveWindowToFront(this);
                MouseManager.LastMouseState = MouseState.None;
            }

            foreach (Control control in Controls)
            {
                if (control != null)
                {
                    control.Update();
                }
            }
        }

        public virtual void HandleKey(KeyEvent key) { }
    }
}
