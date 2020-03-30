using Microsoft.CSharp;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GuiScratch
{
    public class Run
    {
        public void runProgram(List<Block> blocks, Values myValues, string folderPath)
        {
            this.blocks = blocks;
            this.myValues = myValues;
            
            string Output = folderPath;
            if (folderPath != "")
            {
                Output += "\\";
            }
            Output += "Program.exe";

            string source = createProgramCode();
            Clipboard.SetText(source);
            CompilerResults results = buildExe(source, Output);

            if (results.Errors.Count > 0)
            {
                for (int i = 0; i <= results.Errors.Count - 1; i++)
                {
                    MessageBox.Show(results.Errors[i].Line.ToString() + ": " + results.Errors[i].ErrorText);
                }
            }
            else
            {
                Process.Start(Output);
            }

        }

        private CompilerResults buildExe(string source, string Output)
        {
            //the start project

            /*using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace FormWithButton
{
    public class Form1 : Form
    {

        [DllImport("kernel32.dll")]
        static extern IntPtr GetConsoleWindow();

        [DllImport("user32.dll")]
        static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);


        public Button button1;
        public Form1()
        {
            button1 = new Button();
            button1.Size = new Size(40, 40);
            button1.Location = new Point(30, 30);
            button1.Text = "Click me";
            this.Controls.Add(button1);
            button1.Click += new EventHandler(button1_Click);
        }
        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Hello World");
        }
        [STAThread]
        static void Main()
        {
            ShowWindow(GetConsoleWindow(), 0);

            Application.EnableVisualStyles();
     Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
*/
            CSharpCodeProvider codeProvider = new CSharpCodeProvider();
            ICodeCompiler icc = codeProvider.CreateCompiler();

            //textBox2.Text = "";
            System.CodeDom.Compiler.CompilerParameters parameters = new CompilerParameters();
            //Make sure we generate an EXE, not a DLL
            parameters.GenerateExecutable = true;
            parameters.OutputAssembly = Output;

            /*if (codeProvider.Supports(GeneratorSupport.Resources))
            {
                parameters.EmbeddedResources.Add("pathToYourGeneratedResourceFile");
            }*/
            parameters.ReferencedAssemblies.Add("System.dll");
            parameters.ReferencedAssemblies.Add("System.Windows.Forms.dll");
            parameters.ReferencedAssemblies.Add("System.Drawing.dll");
            parameters.ReferencedAssemblies.Add("System.Threading.dll");
            parameters.ReferencedAssemblies.Add("System.Runtime.InteropServices.dll");
            
            CompilerResults results = icc.CompileAssemblyFromSource(parameters, source);

            return results;

        }

        private string createProgramCode()
        {
            string res = "";
            //add the usings
            res += "using System;\nusing System.ComponentModel;\nusing System.Drawing;\nusing System.Windows.Forms;" +
                "\nusing System.Runtime.InteropServices;\nusing System.Threading;\nusing System.Collections.Generic; ";

            //add the namespace and class
            res += "\n\nnamespace FormWithButton\n{\npublic class Form1 : Form\n{";

            //add the code to close the command line window and open the form window
            res += "\n[DllImport(\"kernel32.dll\")]\nstatic extern IntPtr GetConsoleWindow();\n\n[DllImport(\"user32.dll\")]\n static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);";

            //add the main func
            res += "\n\n[STAThread]\nstatic void Main()\n{\n\nShowWindow(GetConsoleWindow(), 0);\nApplication.EnableVisualStyles();\nApplication.SetCompatibleTextRenderingDefault(false);\nApplication.Run(new Form1());\n}";
            
            //add the form1 func
            res += "\n\npublic Form1()\n{\nInitializeComponent();\n}";

            //add InitalizeComponentFunc
            res += "\n" + getInitalizeComponentFunc();

            //add the users code
            res += "\n" + getUserCode();

            //add the user funcs
            res += "\n" + getUserFuncs();

            //close the namespace and class
            res += "\n}\n}";

            return res;
        }

        private string getInitalizeComponentFunc()
        {
            //add the controls names in the class so it will be global
            string res = addUserControlsNames();

            //add the variabels names in the class so it will be global
            res += addUserVariabelsAndLists();

            res += "\n\nprivate void InitializeComponent()\n{";
            
            res += "\nActivated += Form1_Activated;";

            //add the user controls
            res += addUserControls();

            //add the user events
            res += addUserEvents();

            //close the initalize component func
            res += "\n}";

            //add the user events funcs
            //res += userEventsFuncs();

            return res;
        }
        
        #region get user code

        private string getUserCode()
        {
            string res = "\nstatic private bool splashShown = false;\nThread t;\n\nprivate void Form1_Activated(object sender, EventArgs e)\n{";

            res += "if (!splashShown)\n{\nTopMost = true;";

            //add the user thread
            res += "\nt = new Thread(startFunc);\nt.Start();";

            //add the when closing kill the thread
            res += "\nFormClosing += closing;";

            res += "\nTopMost = false;\nsplashShown = true;\n}\n}";

            //add the user func
            res += "\n\nprivate void startFunc()\n{";

            //add the user code
            res += getCodeFromFunc(blocks[0] as OnStartBlock, false);

            res += "\n}";

            //add the when closing func
            res += "\n\nprivate void closing(object sender, FormClosingEventArgs e)\n{\nt.Abort();\n}";

            return res;
        }

        private string getCodeFromFunc(OnStartBlock block, bool containFuncBlock = true)
        {
            string res = "";

            if (containFuncBlock)
            {
                res = block.getCode();
            }
            else
            {
                if (block.Client != null)
                {
                    res = block.Client.getCode();
                }
            }

            return res;
        }

        #endregion

        #region user controls

        private string addUserControlsNames()
        {
            string res = "";

            for (int i = 0; i <= myValues.controls.Count - 1; i++)
            {
                addControlNameToCode(ref res, myValues.controls[i]);
            }

            return res;
        }

        private void addControlNameToCode(ref string res, MyControl myControl)
        {
            if (myControl.Kind != MyControl.ControlKinds.Form)
            {
                res += "\n" + myControl.Kind.ToString() + " " + Values.getControlName(myControl.Name) + ";";
            }
        }

        private string addUserControls()
        {
            string res = "";
            for (int i = 0; i <= myValues.controls.Count - 1; i++)
            {
                addControlToCode(ref res, myValues.controls[i]);
            }

            return res;
        }

        private void addControlToCode(ref string res, MyControl c)
        {
            if (c.Kind == MyControl.ControlKinds.Form)
            {
                return;
            }

            string name = Values.getControlName(c.Name);

            //make the control
            res += "\n" + name + " = new " + c.Kind.ToString() + "();";

            //set the properties

            res += "\n" + name + ".AutoSize = true;";
            res += "\n" + name + ".Text = \"" + c.Text + "\";";
            res += "\n" + name + ".Visible = " + c.Visible.ToString().ToLower() + ";";
            res += "\n" + name + ".Location = new Point(" + c.location.X.ToString() + ", " + c.location.Y.ToString() + ");";
            res += "\n" + name + ".Size = new Size(" + c.size.Width.ToString() + ", " + c.size.Height.ToString() + ");";

            res += "\nControls.Add(" + name + ");";
        }

        #endregion

        #region user events

        private string addUserEvents()
        {
            string res = "";

            for (int i = 0; i <= myValues.events.Count - 1; i++)
            {
                res += myValues.events[i].getText();
            }

            return res;
        }

        private string getUserFuncs()
        {
            string res = "";

            for (int i = 0; i <= blocks.Count - 1; i++)
            {
                if (blocks[i].Kind == Block.BlockKinds.OnStart)
                {
                    if (blocks[i].BlockIndex != 0) //isnt the start block
                    {
                        res += getCodeFromFunc(blocks[i] as OnStartBlock);
                    }
                }
            }

            return res;
        }

        #endregion

        #region user vars
        
        private string addUserVariabelsAndLists()
        {
            string res = "";

            //vars
            for (int i = 0; i <= myValues.vars.Count - 1; i++)
            {
                res += "string " + Values.getUserVarName(myValues.vars[i].Name) + " = \"\";";
            }

            //lists
            for (int i = 0; i <= myValues.lists.Count - 1; i++)
            {
                res += "List<string> " + Values.getUserListName(myValues.lists[i].Name) + " = new List<string>();";
            }

            return res;
        }

        #endregion

        List<Block> blocks;
        Values myValues;
    }
}
