using System;
using ClickableTransparentOverlay;
using System.Runtime.InteropServices;
using ImGuiNET;

namespace HideStreamOBs
{
     class Program : Overlay
    {
        [DllImport("user32.dll")]
        static extern IntPtr FindWindow(string lpClassName, string lpWindowName); //short => IntPtr sorry my mistake


        [DllImport("user32.dll")]
        static extern bool SetWindowDisplayAffinity(IntPtr hWnd, uint dwAffinity); //short => bool

        enum WDA
        {
            WDA_NONE = 0x00000000,
            WDA_MONITOR = 0x00000001,
            WDA_EXCLUDEFROMCAPTURE = 0x00000011,
        }
        static bool isBypassStream = false; //Setup basic thing ....
        
        public static void Main(string[] args)
        {
            Program program = new Program();
            program.Run(); //Run menu here
            while (true)
            {
                string overlay = "Overlay"; //how to know this ?? got that!
                IntPtr OverlayHwnd = FindWindow(null, overlay);
                if (isBypassStream)
                {
                    SetWindowDisplayAffinity(OverlayHwnd, (uint)WDA.WDA_EXCLUDEFROMCAPTURE); //Bypass on
                }
                else
                {
                    SetWindowDisplayAffinity(OverlayHwnd, (uint)WDA.WDA_NONE); //Bypass off
                }
                Thread.Sleep(3);
            }
            
        }

        
        protected override void Render() //Create Menu
        {
            ImGui.Begin("Hide From OBS");
            ImGui.Checkbox("Stream Bypass", ref isBypassStream);
            ImGui.End();
        }
    }
}
