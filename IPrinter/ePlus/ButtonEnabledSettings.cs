
namespace ePlus
{
    partial class frmMain
    {

        void SetButtonNewEnable(bool value)
        {
            //if (btnNew.Visible)
            //    btnNew.Enabled = value;
            //if (miNew.Visible)
            //    miNew.Enabled = value;
        }

        void SetButtonCloseEnable(bool value)
        {
            //if (btnClose.Visible)
            //    btnClose.Enabled = value;
            //if (miClose.Visible)
            //    miClose.Enabled = value;
        }

        void SetButtonConnectEnable(bool value)
        {
            btnConnect.Enabled = value;
            miConnect.Enabled = value; 
        }

        void SetButtonDisConnectEnable(bool value)
        {
            btnDisconnect.Enabled = value;
            miDisconnect.Enabled = value;
        }

        void SetButtonCutEnable(bool value)
        {
            btnCut.Enabled = value;
            miCut.Enabled = value; 
        }

        void SetButtonCopyEnable(bool value)
        {
            btnCopy.Enabled = value;
            miCopy.Enabled = value;
        }

        void SetButtonPasteEnable(bool value)
        {
            btnPaste.Enabled = value;
            miPaste.Enabled = value; 
        }

        void SetEditButtonEnable(bool value)
        {
            SetButtonCutEnable(value);
            SetButtonCopyEnable(value);
            SetButtonPasteEnable(value); 
        }

        void SetButtonSOEEnable(bool value)
        {
            //btnSetSOE.Enabled = value; 
        }
    }
}
