using Noesis;
using System;
using System.Collections.Generic;
using System.Text;

namespace VeldridNGUI
{
    public static class VeldridMapping
    {
        public static Noesis.MouseButton? GetNoesisMouseButton(Veldrid.MouseButton button)
        {
            switch (button)
            {
                case Veldrid.MouseButton.Left:
                    return MouseButton.Left;

                case Veldrid.MouseButton.Right:
                    return MouseButton.Right;

                case Veldrid.MouseButton.Middle:
                    return MouseButton.Middle;

                case Veldrid.MouseButton.Button1:
                    return MouseButton.XButton1;

                case Veldrid.MouseButton.Button2:
                    return MouseButton.XButton2;
            }

            return null;
        }

        public static Noesis.Key? GetNoesisKey(Veldrid.Key key)
        {
            switch (key)
            {
                case Veldrid.Key.ShiftLeft:
                    return Key.LeftShift;

                case Veldrid.Key.ShiftRight:
                    return Key.RightShift;

                case Veldrid.Key.ControlLeft:
                    return Key.LeftCtrl;

                case Veldrid.Key.ControlRight:
                    return Key.RightCtrl;

                case Veldrid.Key.AltLeft:
                    return Key.LeftAlt;

                case Veldrid.Key.AltRight:
                    return Key.RightAlt;

                case Veldrid.Key.WinLeft:
                    return Key.LWin;

                case Veldrid.Key.WinRight:
                    return Key.RWin;

                case Veldrid.Key.F1:
                    return Key.F1;

                case Veldrid.Key.F2:
                    return Key.F2;

                case Veldrid.Key.F3:
                    return Key.F3;

                case Veldrid.Key.F4:
                    return Key.F4;

                case Veldrid.Key.F5:
                    return Key.F5;

                case Veldrid.Key.F6:
                    return Key.F6;

                case Veldrid.Key.F7:
                    return Key.F7;

                case Veldrid.Key.F8:
                    return Key.F8;

                case Veldrid.Key.F9:
                    return Key.F9;

                case Veldrid.Key.F10:
                    return Key.F10;

                case Veldrid.Key.F11:
                    return Key.F11;

                case Veldrid.Key.F12:
                    return Key.F12;

                case Veldrid.Key.F13:
                    return Key.F13;

                case Veldrid.Key.F14:
                    return Key.F14;

                case Veldrid.Key.F15:
                    return Key.F15;

                case Veldrid.Key.F16:
                    return Key.F16;

                case Veldrid.Key.F17:
                    return Key.F17;

                case Veldrid.Key.F18:
                    return Key.F18;

                case Veldrid.Key.F19:
                    return Key.F19;

                case Veldrid.Key.F20:
                    return Key.F20;

                case Veldrid.Key.F21:
                    return Key.F21;

                case Veldrid.Key.F22:
                    return Key.F22;

                case Veldrid.Key.F23:
                    return Key.F23;

                case Veldrid.Key.F24:
                    return Key.F24;

                case Veldrid.Key.Up:
                    return Key.Up;

                case Veldrid.Key.Down:
                    return Key.Down;

                case Veldrid.Key.Left:
                    return Key.Left;

                case Veldrid.Key.Right:
                    return Key.Right;

                case Veldrid.Key.Enter:
                    return Key.Enter;

                case Veldrid.Key.Escape:
                    return Key.Escape;

                case Veldrid.Key.Space:
                    return Key.Space;

                case Veldrid.Key.Tab:
                    return Key.Tab;

                case Veldrid.Key.BackSpace:
                    return Key.Back;

                case Veldrid.Key.Insert:
                    return Key.Insert;

                case Veldrid.Key.Delete:
                    return Key.Delete;

                case Veldrid.Key.PageUp:
                    return Key.PageUp;

                case Veldrid.Key.PageDown:
                    return Key.PageDown;

                case Veldrid.Key.Home:
                    return Key.Home;

                case Veldrid.Key.End:
                    return Key.End;

                case Veldrid.Key.CapsLock:
                    return Key.CapsLock;

                case Veldrid.Key.ScrollLock:
                    return Key.Scroll;

                case Veldrid.Key.PrintScreen:
                    return Key.PrintScreen;

                case Veldrid.Key.Pause:
                    return Key.Pause;

                case Veldrid.Key.NumLock:
                    return Key.NumLock;

                case Veldrid.Key.Clear:
                    return Key.Clear;

                case Veldrid.Key.Sleep:
                    return Key.Sleep;

                case Veldrid.Key.Keypad0:
                    return Key.NumPad0;

                case Veldrid.Key.Keypad1:
                    return Key.NumPad1;

                case Veldrid.Key.Keypad2:
                    return Key.NumPad2;

                case Veldrid.Key.Keypad3:
                    return Key.NumPad3;

                case Veldrid.Key.Keypad4:
                    return Key.NumPad4;

                case Veldrid.Key.Keypad5:
                    return Key.NumPad5;

                case Veldrid.Key.Keypad6:
                    return Key.NumPad6;

                case Veldrid.Key.Keypad7:
                    return Key.NumPad7;

                case Veldrid.Key.Keypad8:
                    return Key.NumPad8;

                case Veldrid.Key.Keypad9:
                    return Key.NumPad9;

                case Veldrid.Key.KeypadDivide:
                    return Key.Divide;

                case Veldrid.Key.KeypadMultiply:
                    return Key.Multiply;

                case Veldrid.Key.KeypadSubtract:
                    return Key.Subtract;

                case Veldrid.Key.KeypadAdd:
                    return Key.Add;

                case Veldrid.Key.KeypadDecimal:
                    return Key.Decimal;

                case Veldrid.Key.KeypadEnter:
                    return Key.Enter;

                case Veldrid.Key.A:
                    return Key.A;

                case Veldrid.Key.B:
                    return Key.B;

                case Veldrid.Key.C:
                    return Key.C;

                case Veldrid.Key.D:
                    return Key.D;

                case Veldrid.Key.E:
                    return Key.E;

                case Veldrid.Key.F:
                    return Key.F;

                case Veldrid.Key.G:
                    return Key.G;

                case Veldrid.Key.H:
                    return Key.H;

                case Veldrid.Key.I:
                    return Key.I;

                case Veldrid.Key.J:
                    return Key.J;

                case Veldrid.Key.K:
                    return Key.K;

                case Veldrid.Key.L:
                    return Key.L;

                case Veldrid.Key.M:
                    return Key.M;

                case Veldrid.Key.N:
                    return Key.N;

                case Veldrid.Key.O:
                    return Key.O;

                case Veldrid.Key.P:
                    return Key.P;

                case Veldrid.Key.Q:
                    return Key.Q;

                case Veldrid.Key.R:
                    return Key.R;

                case Veldrid.Key.S:
                    return Key.S;

                case Veldrid.Key.T:
                    return Key.T;

                case Veldrid.Key.U:
                    return Key.U;

                case Veldrid.Key.V:
                    return Key.V;

                case Veldrid.Key.W:
                    return Key.W;

                case Veldrid.Key.X:
                    return Key.X;

                case Veldrid.Key.Y:
                    return Key.Y;

                case Veldrid.Key.Z:
                    return Key.Z;

                case Veldrid.Key.Number0:
                    return Key.D0;

                case Veldrid.Key.Number1:
                    return Key.D1;

                case Veldrid.Key.Number2:
                    return Key.D2;

                case Veldrid.Key.Number3:
                    return Key.D3;

                case Veldrid.Key.Number4:
                    return Key.D4;

                case Veldrid.Key.Number5:
                    return Key.D5;

                case Veldrid.Key.Number6:
                    return Key.D6;

                case Veldrid.Key.Number7:
                    return Key.D7;

                case Veldrid.Key.Number8:
                    return Key.D8;

                case Veldrid.Key.Number9:
                    return Key.D9;

                case Veldrid.Key.Tilde:
                    return Key.OemTilde;

                case Veldrid.Key.Minus:
                    return Key.OemMinus;

                case Veldrid.Key.Plus:
                    return Key.OemPlus;

                case Veldrid.Key.BracketLeft:
                    return Key.OemOpenBrackets;

                case Veldrid.Key.BracketRight:
                    return Key.OemCloseBrackets;

                case Veldrid.Key.Semicolon:
                    return Key.OemSemicolon;

                case Veldrid.Key.Quote:
                    return Key.OemQuotes;

                case Veldrid.Key.Comma:
                    return Key.OemComma;

                case Veldrid.Key.Period:
                    return Key.OemPeriod;

                case Veldrid.Key.Slash:
                    return null; // TODO

                case Veldrid.Key.BackSlash:
                    return Key.OemBackslash;
            }

            return null;
        }
    }
}
