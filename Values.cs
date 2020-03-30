using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace GuiScratch
{
    public class Values
    {
        public Values()
        {
            //start all the lists
            controls = new List<MyControl>();

            events = new List<Event>();

            vars = new List<MyVar>();

            lists = new List<MyList>();

            funcs = new List<MyFunc>();
        }

        #region to and from string

        public override string ToString()
        {
            //returns eventsCount events controlsCount controls varsCount vars

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

            //vars
            res += "\x0001" + vars.Count.ToString();

            for (int i = 0; i <= vars.Count - 1; i++)
            {
                res += vars[i].ToString();
            }

            //lists
            res += "\x0001" + lists.Count.ToString();

            for (int i = 0; i <= lists.Count - 1; i++)
            {
                res += lists[i].ToString();
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

            //set the vars
            int varsCount = int.Parse(vars[index++]);
            this.vars = new List<MyVar>();

            for (int i = 0; i <= varsCount - 1; i++)
            {
                this.vars.Add(new MyVar(vars, ref index));
            }

            //set the vars
            int listsCount = int.Parse(vars[index++]);
            lists = new List<MyList>();

            for (int i = 0; i <= listsCount - 1; i++)
            {
                lists.Add(new MyList(vars, ref index));
            }
        }
        
        #endregion

        public Action updateBlocksFunc;

        #region user adds

        private void callUpdate()
        {
            if (updateBlocksFunc != null)
            {
                updateBlocksFunc();
            }
        }

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

        public void AddControl(MyControl myControl)
        {
            controls.Add(myControl);

            callUpdate();
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

        #region variabels

        public List<MyVar> vars;

        public void addVar(MyVar var)
        {
            vars.Add(var);

            callUpdate();
        }

        public static string userVarStartString = "userVar_";
        public static string getUserVarName(string name)
        {
            return userVarStartString + name;
        }

        public string[] getVars()
        {
            string[] names = new string[vars.Count];
            //add all the controls names to the list
            for (int i = 0; i <= vars.Count - 1; i++)
            {
                names[i] = vars[i].Name;
            }

            return names;
        }

        #endregion

        #region lists
        
        public List<MyList> lists;

        public void addList(MyList list)
        {
            lists.Add(list);

            callUpdate();
        }

        public static string userListStartString = "userList_";
        public static string getUserListName(string name)
        {
            return userVarStartString + name;
        }

        public string[] getLists()
        {
            string[] names = new string[lists.Count];
            //add all the controls names to the list
            for (int i = 0; i <= lists.Count - 1; i++)
            {
                names[i] = lists[i].Name;
            }

            return names;
        }
        
        #endregion

        #region vars

        private static string VarStartString = "var_";
        public static string getVarName(string name)
        {
            return VarStartString + name;
        }

        decimal num = 0;
        public string getVarNumberName()
        {
            num++;
            return "i"+num.ToString();
        }
        
        public static bool checkIfTheNameIsGoodForVarOrList(ref string reason, string text, bool checkEmpty = true)
        {
            if (text == "")
            {
                if (checkEmpty)
                {
                    reason = "The name can't be nothing";
                    return false;
                }
                else
                {
                    return true;
                }
            }

            //check if the name contains characters that is not a letters
            if (!Regex.IsMatch(text, @"^[0-9a-zA-Z_]+$"))
            {
                reason = "You can use in the name only the chracters a-z, A-Z, 0-9 and _";
                return false;
            }

            int checkStart = 0;
            if (int.TryParse(text[0].ToString(), out checkStart))
            {
                reason = "Variabel name can't start with a number";
                return false;
            }

            return true;
        }

        #endregion

        #region funcs
        
        public string getFuncName()
        {
            //add the event start
            userEventCount++;
            return "userFunc" + userEventCount;
        }

        public List<MyFunc> funcs;

        public void addFunc(MyFunc func)
        {
            funcs.Add(func);

            callUpdate();
        }
        
        #endregion

        #endregion

        public void SetupBeforeRuning()
        {
            num = 0;
        }

    }
}