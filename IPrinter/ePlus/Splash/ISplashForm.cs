using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace SplashScreen
{
    /// <summary>
    /// interface for Splash Screen
    /// </summary>
    public interface ISplashForm
    {
        void SetStatusInfo(string NewStatusInfo);

        void SetBanner(Image Banner);
    }
}
