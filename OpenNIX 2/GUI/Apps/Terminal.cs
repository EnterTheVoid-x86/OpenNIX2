﻿using System;
using System.Collections.Generic;
using System.IO;
using Cosmos.System;
using PrismAPI.Graphics;
using OpenNIX_2;

namespace OpenNIX.GUI.Apps
{
    public class Terminal : Window
    {
        private Queue<KeyEvent> KeyBuffer = new Queue<KeyEvent>();
        public SVGAIITerminal Console;

        public Terminal() : base(50, 50, 720, 400, "Terminal")
        {
            Console = new SVGAIITerminal(716, 396, Resources.Font, TerminalUpdate);
        }

        public override void Render()
        {
            Contents.DrawImage(2, 2, Console.Contents, false);
            base.Render();
        }

        public override void HandleKey(KeyEvent key)
        {
            KeyBuffer.Enqueue(key);
        }

        private void TerminalUpdate()
        {
            Render();
        }

        public void DrawPrompt()
        {
            Console.Write($"[{OpenNIX_2.Kernel.Username}@[{OpenNIX_2.Kernel.Hostname} ");
            Console.Write(DiskManager.GetUnixLikePath(Directory.GetCurrentDirectory()));
            Console.Write("]# ");
        }

        public int startX = 0, startY = 0;
        public string returnValue = string.Empty;

        public override void Update()
        {
            if (IsMouseOver)
            {
                MouseDriver.Mouse = Resources.MouseText;
                MouseDriver.MouseOffsetX = 4;
                MouseDriver.MouseOffsetY = 7;
            }
                

            base.Update();

            if (Focused)
            {
                Console.TryDrawCursor();

                if (KeyBuffer.TryDequeue(out var key))
                {
                    switch (key.Key)
                    {
                        case ConsoleKeyEx.Enter:
                            Console.Contents.DrawFilledRectangle(Console.Font.Size / 2 * Console.CursorX, Console.Font.Size * Console.CursorY, Convert.ToUInt16(Console.Font.Size / 2), Console.Font.Size, 0, Console.BackgroundColor);
                            Console.CursorX = 0;
                            Console.CursorY++;
                            Console.TryScroll();
                            Console.LastInput = returnValue;

                            Shell.Run(returnValue, Console);
                            Console.Font = Resources.Font;
                            DrawPrompt();

                            startX = Console.CursorX;
                            startY = Console.CursorY;
                            returnValue = string.Empty;
                            break;

                        case ConsoleKeyEx.Backspace:
                            try
                            {
                                if (!(Console.CursorX == startX && Console.CursorY == startY))
                                {
                                    if (Console.CursorX == 0)
                                    {
                                        Console.Contents.DrawFilledRectangle(Console.Font.Size / 2 * Console.CursorX, Console.Font.Size * Console.CursorY, Convert.ToUInt16(Console.Font.Size / 2), Console.Font.Size, 0, Console.BackgroundColor);
                                        Console.CursorY--;
                                        Console.CursorX = Contents.Width / (Console.Font.Size / 2) - 1;
                                        Console.Contents.DrawFilledRectangle(Console.Font.Size / 2 * Console.CursorX, Console.Font.Size * Console.CursorY, Convert.ToUInt16(Console.Font.Size / 2), Console.Font.Size, 0, Console.BackgroundColor);
                                    }
                                    else
                                    {
                                        Console.Contents.DrawFilledRectangle(Console.Font.Size / 2 * Console.CursorX, Console.Font.Size * Console.CursorY, Convert.ToUInt16(Console.Font.Size / 2), Console.Font.Size, 0, Console.BackgroundColor);
                                        Console.CursorX--;
                                        Console.Contents.DrawFilledRectangle(Console.Font.Size / 2 * Console.CursorX, Console.Font.Size * Console.CursorY, Convert.ToUInt16(Console.Font.Size / 2), Console.Font.Size, 0, Console.BackgroundColor);
                                    }

                                    returnValue = returnValue.Remove(returnValue.Length - 1); // Remove the last character of the string
                                }
                            }
                            catch { }

                            Console.ForceDrawCursor();
                            break;

                        case ConsoleKeyEx.Tab:
                            Console.Write('\t');
                            returnValue += new string(' ', 4);

                            Console.ForceDrawCursor();
                            break;

                        case ConsoleKeyEx.UpArrow:
                            Console.SetCursorPosition(startX, startY);
                            Console.Write(new string(' ', returnValue.Length));
                            Console.SetCursorPosition(startX, startY);
                            Console.Write(Console.LastInput);
                            returnValue = Console.LastInput;

                            Console.ForceDrawCursor();
                            break;

                        default:
                            if (KeyboardManager.ControlPressed)
                            {
                                if (key.Key == ConsoleKeyEx.L)
                                {
                                    Console.Clear();
                                    returnValue = string.Empty;
                                    Console.reading = false;
                                }
                            }
                            else
                            {
                                Console.Write(key.KeyChar.ToString());
                                Console.TryScroll();
                                returnValue += key.KeyChar;
                            }

                            Console.ForceDrawCursor();
                            break;
                    }
                }
            }
        }
    }
}
