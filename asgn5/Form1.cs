using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.IO;
using System.Text;

namespace asgn5v1
{
    /// <summary>
    /// Summary description for Transformer.
    /// </summary>
    public class Transformer : Form
    {
        #region SidebarComponenets
        private IContainer components;
        private ImageList tbimages;
        private ToolBar toolBar1;
        private ToolBarButton transleftbtn;
        private ToolBarButton transrightbtn;
        private ToolBarButton transupbtn;
        private ToolBarButton transdownbtn;
        private ToolBarButton toolBarButton1;
        private ToolBarButton scaleupbtn;
        private ToolBarButton scaledownbtn;
        private ToolBarButton toolBarButton2;
        private ToolBarButton rotxby1btn;
        private ToolBarButton rotyby1btn;
        private ToolBarButton rotzby1btn;
        private ToolBarButton toolBarButton3;
        private ToolBarButton rotxbtn;
        private ToolBarButton rotybtn;
        private ToolBarButton rotzbtn;
        private ToolBarButton toolBarButton4;
        private ToolBarButton shearrightbtn;
        private ToolBarButton shearleftbtn;
        private ToolBarButton toolBarButton5;
        private ToolBarButton resetbtn;
        private ToolBarButton exitbtn;
        #endregion SidebarComponenets

        private int  numpts   = 0;
        private int  numlines = 0;
        private bool gooddata = false;
        private bool xRotationRunning, yRotationRunning, zRotationRunning = false;

        private double[,] vertices;
        private double[,] originalVertices;
        private int[,] lines;

        private Timer timer = new Timer();

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.Run(new Transformer());
        }

        public Transformer()
        {
            InitializeComponent();
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.DoubleBuffer, true);
            Text = "COMP 4560:  Assignment 5 (A00866713) (Jeffrey Schweigler)";
            ResizeRedraw = true;
            BackColor = Color.Black;
            MenuItem miNewDat = new MenuItem("New &Data...", new EventHandler(MenuNewDataOnClick));
            MenuItem miExit = new MenuItem("E&xit", new EventHandler(MenuFileExitOnClick));
            MenuItem miDash = new MenuItem("-");
            MenuItem miFile = new MenuItem("&File", new MenuItem[] { miNewDat, miDash, miExit });
            MenuItem miAbout = new MenuItem("&About", new EventHandler(MenuAboutOnClick));
            Menu = new MainMenu(new MenuItem[] { miFile, miAbout });
        }

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code
        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Transformer));
            this.tbimages = new System.Windows.Forms.ImageList(this.components);
            this.toolBar1 = new System.Windows.Forms.ToolBar();
            this.transleftbtn = new System.Windows.Forms.ToolBarButton();
            this.transrightbtn = new System.Windows.Forms.ToolBarButton();
            this.transupbtn = new System.Windows.Forms.ToolBarButton();
            this.transdownbtn = new System.Windows.Forms.ToolBarButton();
            this.toolBarButton1 = new System.Windows.Forms.ToolBarButton();
            this.scaleupbtn = new System.Windows.Forms.ToolBarButton();
            this.scaledownbtn = new System.Windows.Forms.ToolBarButton();
            this.toolBarButton2 = new System.Windows.Forms.ToolBarButton();
            this.rotxby1btn = new System.Windows.Forms.ToolBarButton();
            this.rotyby1btn = new System.Windows.Forms.ToolBarButton();
            this.rotzby1btn = new System.Windows.Forms.ToolBarButton();
            this.toolBarButton3 = new System.Windows.Forms.ToolBarButton();
            this.rotxbtn = new System.Windows.Forms.ToolBarButton();
            this.rotybtn = new System.Windows.Forms.ToolBarButton();
            this.rotzbtn = new System.Windows.Forms.ToolBarButton();
            this.toolBarButton4 = new System.Windows.Forms.ToolBarButton();
            this.shearrightbtn = new System.Windows.Forms.ToolBarButton();
            this.shearleftbtn = new System.Windows.Forms.ToolBarButton();
            this.toolBarButton5 = new System.Windows.Forms.ToolBarButton();
            this.resetbtn = new System.Windows.Forms.ToolBarButton();
            this.exitbtn = new System.Windows.Forms.ToolBarButton();
            this.SuspendLayout();
            // 
            // tbimages
            // 
            this.tbimages.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("tbimages.ImageStream")));
            this.tbimages.TransparentColor = System.Drawing.Color.Transparent;
            this.tbimages.Images.SetKeyName(0, "");
            this.tbimages.Images.SetKeyName(1, "");
            this.tbimages.Images.SetKeyName(2, "");
            this.tbimages.Images.SetKeyName(3, "");
            this.tbimages.Images.SetKeyName(4, "");
            this.tbimages.Images.SetKeyName(5, "");
            this.tbimages.Images.SetKeyName(6, "");
            this.tbimages.Images.SetKeyName(7, "");
            this.tbimages.Images.SetKeyName(8, "");
            this.tbimages.Images.SetKeyName(9, "");
            this.tbimages.Images.SetKeyName(10, "");
            this.tbimages.Images.SetKeyName(11, "");
            this.tbimages.Images.SetKeyName(12, "");
            this.tbimages.Images.SetKeyName(13, "");
            this.tbimages.Images.SetKeyName(14, "");
            this.tbimages.Images.SetKeyName(15, "");
            // 
            // toolBar1
            // 
            this.toolBar1.Buttons.AddRange(new System.Windows.Forms.ToolBarButton[] {
            this.transleftbtn,
            this.transrightbtn,
            this.transupbtn,
            this.transdownbtn,
            this.toolBarButton1,
            this.scaleupbtn,
            this.scaledownbtn,
            this.toolBarButton2,
            this.rotxby1btn,
            this.rotyby1btn,
            this.rotzby1btn,
            this.toolBarButton3,
            this.rotxbtn,
            this.rotybtn,
            this.rotzbtn,
            this.toolBarButton4,
            this.shearrightbtn,
            this.shearleftbtn,
            this.toolBarButton5,
            this.resetbtn,
            this.exitbtn});
            this.toolBar1.Dock = System.Windows.Forms.DockStyle.Right;
            this.toolBar1.DropDownArrows = true;
            this.toolBar1.ImageList = this.tbimages;
            this.toolBar1.Location = new System.Drawing.Point(484, 0);
            this.toolBar1.Name = "toolBar1";
            this.toolBar1.ShowToolTips = true;
            this.toolBar1.Size = new System.Drawing.Size(24, 306);
            this.toolBar1.TabIndex = 0;
            this.toolBar1.ButtonClick += new System.Windows.Forms.ToolBarButtonClickEventHandler(this.toolBar1_ButtonClick);
            // 
            // transleftbtn
            // 
            this.transleftbtn.ImageIndex = 1;
            this.transleftbtn.Name = "transleftbtn";
            this.transleftbtn.ToolTipText = "translate left";
            // 
            // transrightbtn
            // 
            this.transrightbtn.ImageIndex = 0;
            this.transrightbtn.Name = "transrightbtn";
            this.transrightbtn.ToolTipText = "translate right";
            // 
            // transupbtn
            // 
            this.transupbtn.ImageIndex = 2;
            this.transupbtn.Name = "transupbtn";
            this.transupbtn.ToolTipText = "translate up";
            // 
            // transdownbtn
            // 
            this.transdownbtn.ImageIndex = 3;
            this.transdownbtn.Name = "transdownbtn";
            this.transdownbtn.ToolTipText = "translate down";
            // 
            // toolBarButton1
            // 
            this.toolBarButton1.Name = "toolBarButton1";
            this.toolBarButton1.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
            // 
            // scaleupbtn
            // 
            this.scaleupbtn.ImageIndex = 4;
            this.scaleupbtn.Name = "scaleupbtn";
            this.scaleupbtn.ToolTipText = "scale up";
            // 
            // scaledownbtn
            // 
            this.scaledownbtn.ImageIndex = 5;
            this.scaledownbtn.Name = "scaledownbtn";
            this.scaledownbtn.ToolTipText = "scale down";
            // 
            // toolBarButton2
            // 
            this.toolBarButton2.Name = "toolBarButton2";
            this.toolBarButton2.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
            // 
            // rotxby1btn
            // 
            this.rotxby1btn.ImageIndex = 6;
            this.rotxby1btn.Name = "rotxby1btn";
            this.rotxby1btn.ToolTipText = "rotate about x by 1";
            // 
            // rotyby1btn
            // 
            this.rotyby1btn.ImageIndex = 7;
            this.rotyby1btn.Name = "rotyby1btn";
            this.rotyby1btn.ToolTipText = "rotate about y by 1";
            // 
            // rotzby1btn
            // 
            this.rotzby1btn.ImageIndex = 8;
            this.rotzby1btn.Name = "rotzby1btn";
            this.rotzby1btn.ToolTipText = "rotate about z by 1";
            // 
            // toolBarButton3
            // 
            this.toolBarButton3.Name = "toolBarButton3";
            this.toolBarButton3.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
            // 
            // rotxbtn
            // 
            this.rotxbtn.ImageIndex = 9;
            this.rotxbtn.Name = "rotxbtn";
            this.rotxbtn.ToolTipText = "rotate about x continuously";
            // 
            // rotybtn
            // 
            this.rotybtn.ImageIndex = 10;
            this.rotybtn.Name = "rotybtn";
            this.rotybtn.ToolTipText = "rotate about y continuously";
            // 
            // rotzbtn
            // 
            this.rotzbtn.ImageIndex = 11;
            this.rotzbtn.Name = "rotzbtn";
            this.rotzbtn.ToolTipText = "rotate about z continuously";
            // 
            // toolBarButton4
            // 
            this.toolBarButton4.Name = "toolBarButton4";
            this.toolBarButton4.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
            // 
            // shearrightbtn
            // 
            this.shearrightbtn.ImageIndex = 12;
            this.shearrightbtn.Name = "shearrightbtn";
            this.shearrightbtn.ToolTipText = "shear right";
            // 
            // shearleftbtn
            // 
            this.shearleftbtn.ImageIndex = 13;
            this.shearleftbtn.Name = "shearleftbtn";
            this.shearleftbtn.ToolTipText = "shear left";
            // 
            // toolBarButton5
            // 
            this.toolBarButton5.Name = "toolBarButton5";
            this.toolBarButton5.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
            // 
            // resetbtn
            // 
            this.resetbtn.ImageIndex = 14;
            this.resetbtn.Name = "resetbtn";
            this.resetbtn.ToolTipText = "restore the initial image";
            // 
            // exitbtn
            // 
            this.exitbtn.ImageIndex = 15;
            this.exitbtn.Name = "exitbtn";
            this.exitbtn.ToolTipText = "exit the program";
            // 
            // Transformer
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(508, 306);
            this.Controls.Add(this.toolBar1);
            this.Name = "Transformer";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Transformer_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion

        protected override void OnPaint(PaintEventArgs pea)
        {
            Pen pen = new Pen(Color.White, 3);
            if (gooddata)
            {
                for (int i = 0; i < numlines; i++)
                {
                    pea.Graphics.DrawLine(pen, (int)vertices[lines[i, 0], 0], (int)vertices[lines[i, 0], 1],
                        (int)vertices[lines[i, 1], 0], (int)vertices[lines[i, 1], 1]);
                }
            }
        }

        void MenuNewDataOnClick(object obj, EventArgs ea)
        {
            //MessageBox.Show("New Data item clicked.");
            gooddata = GetNewData();
        }

        void MenuFileExitOnClick(object obj, EventArgs ea)
        {
            Close();
        }

        void MenuAboutOnClick(object obj, EventArgs ea)
        {
            AboutDialogBox dlg = new AboutDialogBox();
            dlg.ShowDialog();
        }

        bool GetNewData()
        {
            string strinputfile, text;
            ArrayList coorddata = new ArrayList();
            ArrayList linesdata = new ArrayList();
            OpenFileDialog opendlg = new OpenFileDialog();
            opendlg.Title = "Choose File with Coordinates of Vertices";
            if (opendlg.ShowDialog() == DialogResult.OK)
            {
                strinputfile = opendlg.FileName;
                FileInfo coordfile = new FileInfo(strinputfile);
                StreamReader reader = coordfile.OpenText();
                do
                {
                    text = reader.ReadLine();
                    if (text != null) coorddata.Add(text);
                } while (text != null);
                reader.Close();
                DecodeCoords(coorddata);
            }
            else
            {
                MessageBox.Show("***Failed to Open Coordinates File***");
                return false;
            }

            opendlg.Title = "Choose File with Data Specifying Lines";
            if (opendlg.ShowDialog() == DialogResult.OK)
            {
                strinputfile = opendlg.FileName;
                FileInfo linesfile = new FileInfo(strinputfile);
                StreamReader reader = linesfile.OpenText();
                do
                {
                    text = reader.ReadLine();
                    if (text != null) linesdata.Add(text);
                } while (text != null);
                reader.Close();
                DecodeLines(linesdata);
            }
            else
            {
                MessageBox.Show("***Failed to Open Line Data File***");
                return false;
            }
 
            return true;
        }

        void DecodeCoords(ArrayList coorddata)
        {
            // This may allocate slightly more rows than necessary
            vertices = new double[coorddata.Count, 4];
            numpts = 0;
            string[] text = null;
            for (int i = 0; i < coorddata.Count; i++)
            {
                text = coorddata[i].ToString().Split(' ', ',');
                vertices[numpts, 0] = double.Parse(text[0]);
                if (vertices[numpts, 0] < 0.0d)
                    break;
                vertices[numpts, 1] = -double.Parse(text[1]);
                vertices[numpts, 2] = double.Parse(text[2]);
                vertices[numpts, 3] = 1.0d;
                numpts++;
            }

            // Make note of original vertices for when resetting
            originalVertices = vertices;

            // Center object and scale up
            double centerWidth  = ClientSize.Width / 2;
            double centerHeight = ClientSize.Height / 2;
            double scaleAmount  = centerHeight / (Transformations.FindMaxY(vertices) - Transformations.FindMinY(vertices));

            vertices = Transformations.CenterObjectAndScaleUp(vertices, centerWidth, centerHeight, scaleAmount);

            Refresh();
        }

        void DecodeLines(ArrayList linesdata)
        {
            //this may allocate slightly more rows that necessary
            lines = new int[linesdata.Count, 2];
            numlines = 0;
            string[] text = null;
            for (int i = 0; i < linesdata.Count; i++)
            {
                text = linesdata[i].ToString().Split(' ', ',');
                lines[numlines, 0] = int.Parse(text[0]);
                if (lines[numlines, 0] < 0) break;
                lines[numlines, 1] = int.Parse(text[1]);
                numlines++;
            }
        }

        private void Transformer_Load(object sender, System.EventArgs e)
        {

        }

        private void MyTimer_Tick(object sender, EventArgs e)
        {
            MessageBox.Show("The form will now be closed.", "Time Elapsed");
            this.Close();
        }

        private void toolBar1_ButtonClick(object sender, ToolBarButtonClickEventArgs e)
        {
            if (e.Button == transleftbtn)
            {
                Transformations.ResetTransform();
                vertices = Transformations.Translate(vertices, -Transformations.DEFAULT_INCREMENT, 0, 0);
                Refresh();
            }
            if (e.Button == transrightbtn) 
            {
                Transformations.ResetTransform();
                vertices = Transformations.Translate(vertices, Transformations.DEFAULT_INCREMENT, 0, 0);
                Refresh();
            }
            if (e.Button == transupbtn)
            {
                Transformations.ResetTransform();
                vertices = Transformations.Translate(vertices, 0, -Transformations.DEFAULT_INCREMENT, 0);
                Refresh();
            }
            if(e.Button == transdownbtn)
            {
                Transformations.ResetTransform();
                vertices = Transformations.Translate(vertices, 0, Transformations.DEFAULT_INCREMENT, 0);
                Refresh();
            }
            if (e.Button == scaleupbtn) 
            {
                Transformations.ResetTransform();
                vertices = Transformations.UniformScale(vertices, Transformations.DEFAULT_SCALEUP, Transformations.DEFAULT_SCALEUP, Transformations.DEFAULT_SCALEUP);
                Refresh();
            }
            if (e.Button == scaledownbtn) 
            {
                Transformations.ResetTransform();
                vertices = Transformations.UniformScale(vertices, Transformations.DEFAULT_SCALEDOWN, Transformations.DEFAULT_SCALEDOWN, Transformations.DEFAULT_SCALEDOWN);
                Refresh();
            }
            if (e.Button == rotxby1btn) 
            {
                Transformations.ResetTransform();
                vertices = Transformations.RotateX(vertices, Transformations.DEFAULT_THETA);
                Refresh();
            }
            if (e.Button == rotyby1btn) 
            {
                Transformations.ResetTransform();
                vertices = Transformations.RotateY(vertices, Transformations.DEFAULT_THETA);
                Refresh();
            }
            if (e.Button == rotzby1btn) 
            {
                Transformations.ResetTransform();
                vertices = Transformations.RotateZ(vertices, Transformations.DEFAULT_THETA);
                Refresh();
            }

            if (e.Button == rotxbtn) 
            {
                Transformations.ResetTransform();
                if (xRotationRunning)
                {
                    timer.Stop();
                    timer = new Timer();
                    xRotationRunning = false;
                    return;
                }
                timer.Stop();
                timer = new Timer();
                xRotationRunning = true;
                yRotationRunning = false;
                zRotationRunning = false;
                timer.Interval = (50);
                timer.Tick += new EventHandler((ss, ee) => {
                    vertices = Transformations.RotateX(vertices, Transformations.DEFAULT_THETA);
                    Refresh();
                });
                timer.Start();
            }

            if (e.Button == rotybtn) 
            {
                Transformations.ResetTransform();
                if (yRotationRunning)
                {
                    timer.Stop();
                    timer = new Timer();
                    yRotationRunning = false;
                    return;
                }
                timer.Stop();
                timer = new Timer();
                xRotationRunning = false;
                yRotationRunning = true;
                zRotationRunning = false;
                timer.Interval = (50);
                timer.Tick += new EventHandler((ss, ee) => {
                    vertices = Transformations.RotateY(vertices, Transformations.DEFAULT_THETA);
                    Refresh();
                });
                timer.Start();
            }
            
            if (e.Button == rotzbtn) 
            {
                Transformations.ResetTransform();
                if (zRotationRunning)
                {
                    timer.Stop();
                    timer = new Timer();
                    zRotationRunning = false;
                    return;
                }
                timer.Stop();
                timer = new Timer();
                xRotationRunning = false;
                yRotationRunning = false;
                zRotationRunning = true;
                timer.Interval = (50);
                timer.Tick += new EventHandler((ss, ee) => {
                    vertices = Transformations.RotateZ(vertices, Transformations.DEFAULT_THETA);
                    Refresh();
                });
                timer.Start();
            }

            if(e.Button == shearleftbtn)
            {
                Transformations.ResetTransform();
                vertices = Transformations.ShearX(vertices, 0.2);
                Refresh();
            }

            if (e.Button == shearrightbtn)
            {
                Transformations.ResetTransform();
                vertices = Transformations.ShearX(vertices, -0.2);
                Refresh();
            }

            if (e.Button == resetbtn)
            {
                Transformations.ResetTransform();
                RestoreInitialImage();
            }

            if (e.Button == exitbtn) 
            {
                Close();
            }

        }

        private void RestoreInitialImage()
        {
            /*
            timer.Stop();
            vertices = originalVertices;

            // Center object and scale up
            double centerWidth  = ClientSize.Width  / 2;
            double centerHeight = ClientSize.Height / 2;
            double scaleAmount  = centerHeight / (Transformations.FindMaxY(vertices) - Transformations.FindMinY(vertices));

            vertices = Transformations.CenterObjectAndScaleUp(vertices, centerWidth, centerHeight, scaleAmount);

            Refresh();
            */
        }
    }
}
