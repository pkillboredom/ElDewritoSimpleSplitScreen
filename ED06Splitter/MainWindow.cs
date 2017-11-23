using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ED06Splitter
{
    public partial class MainWindow : Form
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        #region QuickSettingButtons

        private void GameOne1080TopLeft_Click(object sender, EventArgs e)
        {
            GameOnePosX.Value = 0;
            GameOnePosY.Value = 0;
            GameOneSizeX.Value = 960;
            GameOneSizeY.Value = 540;
        }

        private void GameOne1080TopHalf_Click(object sender, EventArgs e)
        {
            GameOnePosX.Value = 0;
            GameOnePosY.Value = 0;
            GameOneSizeX.Value = 1920;
            GameOneSizeY.Value = 540;
        }

        private void GameOne1080LeftSide_Click(object sender, EventArgs e)
        {
            GameOnePosX.Value = 0;
            GameOnePosY.Value = 0;
            GameOneSizeX.Value = 960;
            GameOneSizeY.Value = 1080;
        }

        private void GameOne4KTopLeft_Click(object sender, EventArgs e)
        {
            GameOnePosX.Value = 0;
            GameOnePosY.Value = 0;
            GameOneSizeX.Value = 1920;
            GameOneSizeY.Value = 1080;
        }

        private void GameOne4KTopHalf_Click(object sender, EventArgs e)
        {
            GameOnePosX.Value = 0;
            GameOnePosY.Value = 0;
            GameOneSizeX.Value = 3840;
            GameOneSizeY.Value = 1080;
        }

        private void GameOne4KLeftSide_Click(object sender, EventArgs e)
        {
            GameOnePosX.Value = 0;
            GameOnePosY.Value = 0;
            GameOneSizeX.Value = 1920;
            GameOneSizeY.Value = 2160;
        }

        private void GameTwo1080pTopRight_Click(object sender, EventArgs e)
        {
            GameTwoPosX.Value = 960;
            GameTwoPosY.Value = 0;
            GameTwoSizeX.Value = 960;
            GameTwoSizeY.Value = 540;
        }

        private void GameTwo1080pBottomHalf_Click(object sender, EventArgs e)
        {
            GameTwoPosX.Value = 0;
            GameTwoPosY.Value = 540;
            GameTwoSizeX.Value = 1920;
            GameTwoSizeY.Value = 540;
        }

        private void GameTwo1080pRightSide_Click(object sender, EventArgs e)
        {
            GameTwoPosX.Value = 960;
            GameTwoPosY.Value = 0;
            GameTwoSizeX.Value = 960;
            GameTwoSizeY.Value = 1080;
        }

        private void GameTwo4KTopRight_Click(object sender, EventArgs e)
        {
            GameTwoPosX.Value = 1920;
            GameTwoPosY.Value = 0;
            GameTwoSizeX.Value = 1920;
            GameTwoSizeY.Value = 1080;
        }

        private void GameTwo4KBottomHalf_Click(object sender, EventArgs e)
        {
            GameTwoPosX.Value = 0;
            GameTwoPosY.Value = 1080;
            GameTwoSizeX.Value = 3840;
            GameTwoSizeY.Value = 1080;
        }

        private void GameTwo4KRightSide_Click(object sender, EventArgs e)
        {
            GameTwoPosX.Value = 1920;
            GameTwoPosY.Value = 0;
            GameTwoSizeX.Value = 1920;
            GameTwoSizeY.Value = 2160;
        }

        private void GameThree1080BottomLeft_Click(object sender, EventArgs e)
        {
            GameThreePosX.Value = 0;
            GameThreePosY.Value = 540;
            GameThreeSizeX.Value = 960;
            GameThreeSizeY.Value = 540;
        }

        private void GameThree4KBottomLeft_Click(object sender, EventArgs e)
        {
            GameThreePosX.Value = 0;
            GameThreePosY.Value = 1080;
            GameThreeSizeX.Value = 1920;
            GameThreeSizeY.Value = 1080;
        }

        private void GameFour1080BottomRight_Click(object sender, EventArgs e)
        {
            GameFourPosX.Value = 960;
            GameFourPosY.Value = 540;
            GameFourSizeX.Value = 960;
            GameFourSizeY.Value = 540;
        }

        private void GameFour4KBottomRight_Click(object sender, EventArgs e)
        {
            GameFourPosX.Value = 1920;
            GameFourPosY.Value = 1080;
            GameFourSizeX.Value = 1920;
            GameFourSizeY.Value = 1080;
        }

        #endregion

        #region Launch Buttons

        private void GameOneLaunch_Click(object sender, EventArgs e)
        {
            Thread gameLaunchThread = new Thread(() => LaunchInstanceThread(0, (int)GameOnePosX.Value, (int)GameOnePosY.Value, (int)GameOneSizeX.Value, (int)GameOneSizeY.Value));
            gameLaunchThread.Start();
        }

        private void GameTwoLaunch_Click(object sender, EventArgs e)
        {
            Thread gameLaunchThread = new Thread(() => LaunchInstanceThread(1, (int)GameTwoPosX.Value, (int)GameTwoPosY.Value, (int)GameTwoSizeX.Value, (int)GameTwoSizeY.Value));
            gameLaunchThread.Start();
        }

        private void GameThreeLaunch_Click(object sender, EventArgs e)
        {
            Thread gameLaunchThread = new Thread(() => LaunchInstanceThread(2, (int)GameThreePosX.Value, (int)GameThreePosY.Value, (int)GameThreeSizeX.Value, (int)GameThreeSizeY.Value));
            gameLaunchThread.Start();
        }

        private void GameFourLaunch_Click(object sender, EventArgs e)
        {
            Thread gameLaunchThread = new Thread(() => LaunchInstanceThread(3, (int)GameFourPosX.Value, (int)GameFourPosY.Value, (int)GameFourSizeX.Value, (int)GameFourSizeY.Value));
            gameLaunchThread.Start();
        }

        #endregion

        private static void LaunchInstanceThread(int InstanceNumber, int XPosition, int YPosition, int XSize, int YSize)
        {
            try
            {
                Process gameProcess = Process.Start(".\\eldorado.exe", "-windowed -width " + XSize + " -height " + YSize + " -instance " + InstanceNumber);
                gameProcess.WaitForInputIdle(); // Don't try to modify the window until the control loop returns idle.
                try
                {
                    IntPtr window = gameProcess.MainWindowHandle;
                    SetWindowLong(window, gwlStyle, wsSysMenu);
                    SetWindowPos(window, 0, XPosition, YPosition, XSize, YSize, 0x40);
                    DrawMenuBar(window);
                }
                catch
                {
                    MessageBox.Show("Failed to set window location.", "Halo 0.6 Simple Split Screen.");
                }

            }
            catch (System.IO.FileNotFoundException e)
            {
                ErrorPane errorPane = new ErrorPane();
                errorPane.SetErrorString("eldorado.exe could not be found. Is this tool located in the same folder as it?\n" + e.Message);
                Application.Run(errorPane);
            }
            catch (Exception e)
            {
                ErrorPane errorPane = new ErrorPane();
                errorPane.SetErrorString("Some error occured launching the game.\n" + e.Message);
                Application.Run(errorPane);
            }
        }

        #region externs courtesy PIGGS's split screen tool

        /// <summary>
        /// Imports window changing function
        /// </summary>
        /// <param name="hWnd"></param>
        /// <param name="nIndex"></param>
        /// <param name="dwNewLong"></param>
        /// <returns></returns>
        [DllImport("USER32.DLL")]
        public static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

        /// <summary>
        /// Imports find window function
        /// </summary>
        /// <param name="ZeroOnly"></param>
        /// <param name="lpWindowName"></param>
        /// <returns></returns>
        [DllImport("user32.dll", EntryPoint = "FindWindow", SetLastError = true)]
        static extern IntPtr FindWindowByCaption(IntPtr ZeroOnly, string lpWindowName);

        /// <summary>
        /// Imports force window draw function
        /// </summary>
        /// <param name="hWnd"></param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        static extern bool DrawMenuBar(IntPtr hWnd);

        /// <summary>
        /// Imports window placement function
        /// </summary>
        /// <param name="hWnd"></param>
        /// <param name="hWndInsertAfter"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="cx"></param>
        /// <param name="cy"></param>
        /// <param name="wFlags"></param>
        /// <returns></returns>
        [DllImport("user32.dll", EntryPoint = "SetWindowPos")]
        public static extern IntPtr SetWindowPos(IntPtr hWnd, int hWndInsertAfter, int x, int y, int cx, int cy, int wFlags);

        private const int gwlStyle = -16;              //hex constant for style changing
        private const int wsBorder = 0x00800000;       //window with border
        private const int wsCaption = 0x00C00000;      //window with a title bar
        private const int wsSysMenu = 0x00080000;      //window with no borders etc.
        private const int wsMinimizeBox = 0x00020000;  //window with minimizebox

        #endregion
    }
}
