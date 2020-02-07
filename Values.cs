using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace GuiScratch
{
    public class Values
    {
        public Values()
        {
            controls = new List<MyControl>();

            events = new List<Event>();
        }

        #region to and from string

        public override string ToString()
        {
            //returns eventsCount events controlsCount controls

            //events
            string res = "\x0001"+events.Count.ToString();

            for (int i = 0; i <= events.Count - 1; i++)
            {
                res += events[i].ToString();
            }

            //controls
            res += "\x0001" + controls.Count.ToString();

            for (int i = 0; i <= controls.Count - 1; i++)
            {
                res += controls[i].ToString();
            }
            
            return res;
        }
        
        public Values(string[] vars, ref int index)
        {
            //set the events
            int eventsCount = int.Parse(vars[index++]);
            events = new List<Event>();

            for (int i = 0; i <= eventsCount - 1; i++)
            {
                events.Add(new Event(vars, ref index));
            }

            //set the controls
            int controlsCount = int.Parse(vars[index++]);
            controls = new List<MyControl>();

            for (int i = 0; i <= controlsCount - 1; i++)
            {
                controls.Add(new MyControl(vars, ref index));
            }
            
        }

        #endregion

        public string getName(MyControl.ControlKinds kind)
        {
            string name = kind.ToString()+"_";
            int i = 1;

            while (checkIfNameIsInUse(name+i.ToString()))
            {
                i++;
            }

            return name + i.ToString();
        }

        public bool checkIfNameIsInUse(string name, int curIndex = -1)
        {
            for (int i = 0; i <= controls.Count - 1; i++)
            {
                if (controls[i].Name == name && i!=curIndex)
                {
                    return true;
                }
            }
            return false;
        }

        public Action updateBlocksFunc;

        #region events

        public List<Event> events;

        int userEventCount = 0;
        public string getEventName(Event.Kinds eventKind, string name)
        {
            //add the event start
            userEventCount++;
            return  "userEvent_" + userEventCount+"_"+eventKind.ToString();
        }

        public string getEventFuncName(Event.Kinds eventKind, string funcName)
        {
            string res = "private void " + funcName;

            res += "(";

            switch (eventKind)
            {
                case Event.Kinds.mouseDown:
                case Event.Kinds.mouseMove:
                case Event.Kinds.mouseUp:
                    res += "object sender, MouseEventArgs e";
                    break;
            }

            res += ")";
            return res;
        }

        #endregion

        #region controls

        public List<MyControl> controls;

        private static string userControlStartString = "userControl_";
        public static string getControlName(string name)
        {
            if (name == "this")
            {
                return "this";
            }
            return userControlStartString + name;
        }

        public void AddControl(MyControl myControl)
        {
            controls.Add(myControl);

            if (updateBlocksFunc != null)
            {
                updateBlocksFunc();
            }
        }

        public string[] getControls()
        {
            string[] names = new string[controls.Count];
            //add all the controls names to the list
            for (int i = 0; i <= controls.Count - 1; i++)
            {
                names[i] = controls[i].Name;
            }

            return names;
        }

        #endregion

        #region vars

        private static string userVarStartString = "userControl_";
        public static string getVarName(string name)
        {
            return userVarStartString + name;
        }

        decimal num = 0;
        public string getVarNumberName()
        {
            num++;
            return "i"+num.ToString();
        }

        #endregion

        public void SetupBeforeRuning()
        {
            num = 0;
        }

    }
}