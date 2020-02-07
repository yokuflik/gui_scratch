using System;
using System.Collections.Generic;

namespace GuiScratch
{
    public class Event
    {
        public Event(Kinds kind, string funcName, string controlName)
        {
            Kind = kind;

            FuncName = funcName;
            ControlName = controlName;
        }

        public override string ToString()
        {
            //returns kind funcName controlName
            string res = "\x0001" + ((int)Kind).ToString();

            res += "\x0001" + FuncName + "\x0001" + ControlName;

            return res;
        }

        public Event(string[] vars, ref int index)
        {
            Kind = (Kinds)int.Parse(vars[index++]);

            FuncName = vars[index++];

            ControlName = vars[index++];
        }

        public string getText()
        {
            string res = "\n" + Values.getControlName(ControlName)+".";
            /*switch (Kind)
            {
                case Kinds.mouseDown:
                    res += ".MouseDown += ";
                    break;
                case Kinds.mouseMove:
                    res += ".MouseMove += ";
                    break;
                case Kinds.mouseUp:
                    res += ".MouseUp += ";
                    break;
            }*/
            //add the kind as the event kind and make the start letter to be upper case
            string func = Kind.ToString();

            res += func.Substring(0, 1).ToUpper();
            res += func.Substring(1);

            res += " += " + FuncName + ";";
            return res;
        }

        public enum Kinds { mouseDown, mouseMove, mouseUp }
        public Kinds Kind;

        public string FuncName;
        public string ControlName;
    }
}