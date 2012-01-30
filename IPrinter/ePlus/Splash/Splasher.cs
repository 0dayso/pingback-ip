using System;
using System.Windows.Forms;
using System.Threading;
using System.Reflection;
using System.Drawing;

namespace SplashScreen
{
    public class Splasher
    {
        private static Form m_SplashForm = null;
        private static ISplashForm m_SplashInterface = null;
        private static Thread m_SplashThread = null;
        private static string m_TempStatus = string.Empty;
        private static Image m_TempBanner = null;

        /// <summary>
        /// Show the SplashForm
        /// </summary>
        public static void Show(Type splashFormType)
        {
            if (m_SplashThread != null)
                return;
            if (splashFormType == null)
            {
                throw (new Exception("splashFormType is null"));
            }

            m_SplashThread = new Thread(new ThreadStart(delegate()
            {
                CreateInstance(splashFormType);
                Application.Run(m_SplashForm);
            }));

            m_SplashThread.IsBackground = true;
            m_SplashThread.SetApartmentState(ApartmentState.STA);
            m_SplashThread.Start();
        }



        /// <summary>
        /// set the loading Status
        /// </summary>
        public static string Status
        {
            set
            {
                if (m_SplashInterface == null || m_SplashForm == null)
                {
                    m_TempStatus = value;
                    return;
                }
                m_SplashForm.Invoke(
                        new SplashStatusChangedHandle(delegate(string str) { m_SplashInterface.SetStatusInfo(str); }),
                        new object[] { value }
                    );
            }

        }

        /// <summary>
        /// ����Banner����ͼƬ
        /// </summary>
        public static Image Banner
        {
            set
            {
                if (m_SplashInterface == null || m_SplashForm == null)
                {
                    m_TempBanner = value;
                    return;
                }

                try
                {
                    m_SplashForm.Invoke(
                            new SplashBannerChangedHandle(delegate(Image banner) { m_SplashInterface.SetBanner(banner); }),
                            new object[] { value }
                        );
                }
                catch { }
            }

        }

        /// <summary>
        /// Colse the SplashForm
        /// </summary>
        public static void Close()
        {
            if (m_SplashThread == null || m_SplashForm == null) return;

            try
            {
                m_SplashForm.Invoke(new MethodInvoker(m_SplashForm.Close));
            }
            catch (Exception)
            {
            }
            m_SplashThread = null;
            m_SplashForm = null;
            //�ͷŸ�ͼƬ��Դ�����������������޷��޸ĸ��ļ�,����2�����ȱһ����
            if (m_TempBanner != null)
            {
                m_TempBanner.Dispose();
                m_TempBanner = null;
            }
        }

        private static void CreateInstance(Type FormType)
        {

            object obj = FormType.InvokeMember(null,
                                BindingFlags.DeclaredOnly |
                                BindingFlags.Public | BindingFlags.NonPublic |
                                BindingFlags.Instance | BindingFlags.CreateInstance, null, null, null);
            m_SplashForm = obj as Form;
            m_SplashInterface = obj as ISplashForm;
            if (m_SplashForm == null)
            {
                throw (new Exception("Splash Screen must inherit from System.Windows.Forms.Form"));
            }
            if (m_SplashInterface == null)
            {
                throw (new Exception("must implement interface ISplashForm"));
            }

            if (!string.IsNullOrEmpty(m_TempStatus))
                m_SplashInterface.SetStatusInfo(m_TempStatus);
            if (m_TempBanner != null)
                m_SplashInterface.SetBanner(m_TempBanner);
        }


        private delegate void SplashStatusChangedHandle(string NewStatusInfo);
        private delegate void SplashBannerChangedHandle(Image Banner);
    }
}
