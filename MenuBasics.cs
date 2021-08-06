using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace EssentialMechanics
{
    public class ScreenMode { public bool Window = false; public bool Fullscreen = true; }
    public class ScreenSize { public Vector2 _1920x1080 = new Vector2(1920, 1080); public Vector2 _1440x900 = new Vector2(1440, 900); public Vector2 _1280x720 = new Vector2(1280, 720); public Vector2 _1024x768 = new Vector2(1024, 768); }
    public class MenuBasics
    {
        float Volume;
        public enum ScreenModeEnum
        {
            Fullscreen,Window
        }
        public void setMasterVolume(float Volume) { this.Volume = Mathf.Min(Volume, 1); }
        public float getMasterVolume() { return this.Volume; }
        public void changeScreenResolution(ScreenModeEnum mode,Vector2 size) 
        {
            if (mode == ScreenModeEnum.Fullscreen)
                Screen.SetResolution((int)size.x, (int)size.y, true);
            else
                Screen.SetResolution((int)size.x, (int)size.y, false);
        }
    }
}