/// KEYBOARD.CS
/// (c) 2006 by Emma Burrows
/// This file contains the following items:
///  - KeyboardHook: class to enable low-level keyboard hook using
///    the Windows API.
///  - KeyboardHookEventHandler: delegate to handle the KeyIntercepted
///    event raised by the KeyboardHook class.
///  - KeyboardHookEventArgs: EventArgs class to contain the information
///    returned by the KeyIntercepted event.
///    
/// Change history:
/// 17/06/06: 1.0 - First version.
/// 18/06/06: 1.1 - Modified proc assignment in constructor to make class backward 
///                 compatible with 2003.
/// 10/07/06: 1.2 - Added support for modifier keys:
///                 -Changed filter in HookCallback to WM_KEYUP instead of WM_KEYDOWN
///                 -Imported GetKeyState from user32.dll
///                 -Moved native DLL imports to a separate internal class as this 
///                  is a Good Idea according to Microsoft's guidelines
/// 13/02/07: 1.3 - Improved modifier key support:
///                 -Added CheckModifiers() method
///                 -Deleted LoWord/HiWord methods as they weren't necessary
///                 -Implemented Barry Dorman's suggestion to AND GetKeyState
///                  values with 0x8000 to get their result
/// 23/03/07: 1.4 - Fixed bug which made the Alt key appear stuck
///                 - Changed the line
///                     if (nCode >= 0 && (wParam == (IntPtr)WM_KEYUP || wParam == (IntPtr)WM_SYSKEYUP))
///                   to
///                     if (nCode >= 0)
///                     {
///                        if (wParam == (IntPtr)WM_KEYUP || wParam == (IntPtr)WM_SYSKEYUP)
///                        ...
///                   Many thanks to "Scottie Numbnuts" for the solution.


using System;
using System.Diagnostics;
using System.Windows.Forms;
using System.Data;
using System.Runtime.InteropServices;
using System.Text;
using MyInput;
using System.Threading;
using System.Collections.Generic;

/// <summary>
/// Low-level keyboard intercept class to trap and suppress system keys.
/// </summary>
public class GlobalHook : IDisposable
{
    /// <summary>
    /// Parameters accepted by the KeyboardHook constructor.
    /// </summary>
    public enum Parameters
    {
        None,
        AllowAltTab,
        AllowWindowsKey,
        AllowAltTabAndWindows,
        PassAllKeysToNextApp
    }


    //Keyboard API constants
    private const int WH_KEYBOARD_LL = 13;
    private const int WH_CBT = 5;
    private const int WH_MOUSE_LL = 14;
    private const int WM_KEYUP = 0x0101;
    private const int WM_SYSKEYUP = 0x0105;
    private const int WM_KEYDOWN = 0x0100;
    private const int WM_SYSKEYDOWN = 0x0104;
    private const int MK_LBUTTON = 513;
    private const int WM_LBUTTONUP = 0x0202;
    private const int WM_RBUTTONUP = 0x0205;
    private const int WM_MBUTTONUP = 0x0208;
    private const int MK_RBUTTON = 516;
    private const int MK_MBUTTON = 519;
    private const int HSHELL_WINDOWACTIVATED = 4;

    //Modifier key constants
    private const int VK_SHIFT = 0x10;
    private const int VK_CONTROL = 0x11;
    private const int VK_WIN = 91;
    private const int VK_MENU = 0x12;
    private const int VK_CAPITAL = 0x14;

    //Variables used in the call to SetWindowsHookEx
    private HookHandlerDelegate proc;
    private MHookHandlerDelegate Mproc;
    //private MHookHandlerDelegate Sproc;
    private IntPtr hookID = IntPtr.Zero;
    private IntPtr MhookID = IntPtr.Zero;
    private IntPtr ShookID = IntPtr.Zero;
    internal delegate IntPtr HookHandlerDelegate(
        int nCode, IntPtr wParam, ref KBDLLHOOKSTRUCT lParam);


    internal delegate IntPtr MHookHandlerDelegate(
    int nCode, IntPtr wParam, IntPtr lParam);
    /// <summary>
    /// Event triggered when a keystroke is intercepted by the 
    /// low-level hook.
    /// </summary>
    public event KeyboardHookEventHandler KeyIntercepted;

    // Structure returned by the hook whenever a key is pressed
    internal struct KBDLLHOOKSTRUCT
    {
        public int vkCode;
        int scanCode;
        public int flags;
        int time;
        int dwExtraInfo;
    }

    #region Constructors
    /// <summary>
    /// Sets up a keyboard hook to trap all keystrokes without 
    /// passing any to other applications.
    /// </summary>
    public GlobalHook()
    {
        //Initiate();
    }

    public void Initiate()
    {
        proc = new HookHandlerDelegate(HookCallback);
        Mproc = new MHookHandlerDelegate(MHookCallback);
        using (Process curProcess = Process.GetCurrentProcess())
        using (ProcessModule curModule = curProcess.MainModule)
        {
            hookID = NativeMethods.SetWindowsHookEx(WH_KEYBOARD_LL, proc,
                NativeMethods.GetModuleHandle(curModule.ModuleName), 0);
            MhookID = NativeMethods.SetWindowsHookEx(WH_MOUSE_LL, Mproc,
                NativeMethods.GetModuleHandle(curModule.ModuleName), 0);
        }

    }

    public void SetHandle(Main mfm)
    {
        this.mfm = mfm;
    }

    Main mfm;
    /// <summary>
    /// Sets up a keyboard hook with custom parameters.
    /// </summary>
    /// <param name="param">A valid name from the Parameter enum; otherwise, the 
    /// default parameter Parameter.None will be used.</param>

    /// <summary>
    /// Sets up a keyboard hook with custom parameters.
    /// </summary>
    /// <param name="param">A value from the Parameters enum.</param>


    #endregion

    #region Check Modifier keys
    /// <summary>
    /// Checks whether Alt, Shift, Control or CapsLock
    /// is enabled at the same time as another key.
    /// Modify the relevant sections and return type 
    /// depending on what you want to do with modifier keys.
    /// </summary>
    private void CheckModifiers()
    {
        if ((NativeMethods.GetKeyState(VK_SHIFT) & 0x8000) != 0)
        {
            shift = true;
        }
        else
        {
            shift = false;
        }
        if ((NativeMethods.GetKeyState(VK_CONTROL) & 0x8000) != 0)
        {
            ctrl = true;
        }
        if ((NativeMethods.GetKeyState(VK_MENU) & 0x8000) != 0)
        {
            alt = true;
        }
        else
        {
            alt = false;
        }
        if ((NativeMethods.GetKeyState(VK_MENU) & 0x8000) != 0)
        {
            alt = true;
        }
        else
        {
            alt = false;
        }
        if ((NativeMethods.GetKeyState(VK_WIN) & 0x8000) != 0)
        {
            win = true;
        }
        else
        {
            win = false;
        }
    }
    #endregion Check Modifier keys

    #region Hook Callback Method

    private IntPtr MHookCallback(
    int nCode, IntPtr wParam, IntPtr lParam)
    {
        if (nCode >= 0)
        {
            if (wParam == (IntPtr)WM_LBUTTONUP)
            {
                mfm.log.write("LMUP");
                mfm.SystemKeyEvent();
            }
            else if (wParam == (IntPtr)WM_RBUTTONUP)
            {
                mfm.log.write("LMUP");
                mfm.SystemKeyEvent();
            }
            else if (wParam == (IntPtr)WM_MBUTTONUP)
            {
                mfm.log.write("LMUP");
                mfm.SystemKeyEvent();
            }            
        }
        return NativeMethods.CallNextHookEx(MhookID, nCode, wParam, lParam);
    }

    public class KeyStroke
    {
        public bool control;
        public bool alt;
        public bool shift;
        public int key;

        public bool Equals(KeyStroke k)
        {
            if (k.control != control) return false;
            if (k.shift != shift) return false;
            if (k.alt != alt) return false;
            if (k.key != key) return false;
            return true;
        }
    }

    private bool win;
    private bool altpressed;
    private bool alt;
    private bool ctrl;
    private bool shift;

    public KeyStroke k1 = null;
    DateTime k1time = new DateTime(0);
    public KeyStroke k2 = null;

    bool _alt = false;
    bool _ctrl = false;
    bool _shift = false;
    private IntPtr HookCallback(
        int nCode, IntPtr wParam, ref KBDLLHOOKSTRUCT lParam)
    {
        if(mfm == null)
            return NativeMethods.CallNextHookEx(hookID, nCode, wParam, ref lParam);
        if (nCode >= 0)
        {
            bool eat = false;

            shift = false;
            alt = false;
            ctrl = false;
            win = false;
            CheckModifiers();


            if (wParam == (IntPtr)WM_KEYDOWN || wParam == (IntPtr)WM_SYSKEYDOWN)
            {


                if (lParam.vkCode == 0xA0 || lParam.vkCode == 0xA1)
                    _shift = true;
                else if (lParam.vkCode == 164 || lParam.vkCode == 165)
                    _alt = true;
                else if (lParam.vkCode == 0xA2 || lParam.vkCode == 0xA3)
                    _ctrl = true;
                else {

                    bool toofar = false;

                    if (k1time.Ticks == 0)
                    {
                        toofar = true;
                    }
                    else
                    {
                        if ((DateTime.Now - k1time).TotalMilliseconds > 800)
                        {
                            toofar = true;
                        }
                    }

                    k1time = DateTime.Now;
                    if (k2 != null)
                        k1 = k2;
                    else
                        k2 = new KeyStroke();
                    k2.shift = shift;
                    k2.alt = alt;
                    k2.control = ctrl;
                    k2.key = lParam.vkCode;
                    _shift = false;
                    _alt = false;
                    _ctrl = false;

                    if (toofar)
                    {
                       eat = mfm.CheckForShortCut(new KeyStroke(), k2);
                    }else
                       eat = mfm.CheckForShortCut(k1,k2);
                }

                
                if (lParam.flags == 16 && lParam.vkCode == 8)
                {
                }
                else
                {
                    if (lParam.vkCode == 231)
                    {
                        mfm.log.write("KEY:REC UNI");
                    }
                    else if (isSysKey(lParam.vkCode))
                    {
                        mfm.log.write("KEYIS: " + lParam.vkCode.ToString());
                        mfm.SystemKeyEvent();
                    }
                    else if (mfm.kime == null && lParam.vkCode == 0x0d)
                    {
                        mfm.log.write("KEYIS: " + lParam.vkCode.ToString());
                        mfm.SystemKeyEvent();
                    }
                   /* else if (lParam.vkCode == 0xA0 || lParam.vkCode == 0xA1)
                    {
                        // do nothing
                    }*/
                    else if (win || ctrl || (alt && lParam.vkCode != 165))
                    {
                        mfm.log.write("KEYIS: " + lParam.vkCode.ToString());
                        mfm.SystemKeyEvent();
                    }
                    else
                    {
                        if (altpressed)
                            alt = true;
                        eat = mfm.KeyEvent(nCode, wParam, lParam, shift, alt);
                    }
                    if (lParam.vkCode == mfm.togglekey)
                        eat = mfm.togglePressed();
                    if (lParam.vkCode == mfm.enablekey)
                        eat = mfm.enablePressed();
                    if (lParam.vkCode == mfm.scrkey)
                        eat = mfm.scrPressed();
                    if (lParam.vkCode == mfm.osk)
                    {
                        eat = mfm.oskPressed();
                    }

                    if (!(lParam.vkCode == 0xA0 || lParam.vkCode == 0xA1))
                    {
                        altpressed = (lParam.vkCode == 165) ? !altpressed : false;
                        mfm.ChangeState(false, altpressed);
                    }
                    else
                    {
                        mfm.ChangeState(true, altpressed);
                    }

                    if (lParam.vkCode == 165) eat = mfm.active;
                    mfm.log.write("KHDOWN: " + lParam.vkCode.ToString() + " Blocked: " + eat.ToString());
                }
                if (mfm.kime != null && lParam.vkCode == 0x1b && mfm.active)
                    eat = true;
            }
            else if (wParam == (IntPtr)WM_KEYUP || wParam == (IntPtr)WM_SYSKEYUP)
            {
                if (true)
                {
                    if (lParam.vkCode == 0xA0 || lParam.vkCode == 0xA1 || lParam.vkCode == 0xA2 || lParam.vkCode == 0xA3 || lParam.vkCode == 164 || lParam.vkCode == 165)
                    {
                        if (_shift || _ctrl || _alt)
                        {
                            bool toofar = false;
                            
                            if (k1time.Ticks == 0)
                            {
                                toofar = true;
                            }
                            else
                            {
                                TimeSpan ts = DateTime.Now - k1time;
                                int i = ts.Milliseconds;
                                if ((DateTime.Now - k1time).TotalMilliseconds > 800)
                                {
                                    toofar = true;
                                }
                            }

                            k1time = DateTime.Now;
                            if (k2 != null)
                                k1 = k2;
                            else
                                k2 = new KeyStroke();
                            k2.shift = _shift;
                            k2.alt = _alt;
                            k2.control = _ctrl;
                            k2.key = 0;
                            _shift = false;
                            _alt = false;
                            _ctrl = false;
                            if (toofar)
                            {
                                eat = mfm.CheckForShortCut(new KeyStroke(), k2);
                            }
                            else
                            {
                                eat = mfm.CheckForShortCut(k1, k2);
                            }
                        }
                    }
                }

                if (lParam.vkCode == 0xA0 || lParam.vkCode == 0xA1)
                {
                    mfm.ChangeState(false, altpressed);
                }
                if (mfm.kime != null && lParam.vkCode==0x1b && mfm.active)
                    eat = true;
                mfm.log.write("KHUP: " + lParam.vkCode.ToString() + " Blocked: " + eat.ToString());
            }
            if (eat)
                return (System.IntPtr)0;
        }
        return NativeMethods.CallNextHookEx(hookID, nCode, wParam, ref lParam);
    }
    #endregion

    private int[] notsyskeys = { 0xC0, 0x31, 0x32, 0x33, 0x34, 0x35, 0x36, 0x37, 0x38, 0x39, 0x30, 0x51, 0x57, 0x45, 0x52, 0x54, 0x59, 0x55, 0x49, 0x4F, 0x50, 0xDB, 0xDD, 0xDC, 0x41, 0x53, 0x44, 0x46, 0x47, 0x48, 0x4A, 0x4B, 0x4C, 0xBA, 0xDE, 0x5A, 0x58, 0x43, 0x56, 0x42, 0x4E, 0x4D, 0xBC, 0xBE, 0xBF, 0x08, 0xA0, 0xA1, 165, 0x20, 0x0d};
    private bool isSysKey(int key)
    {
        foreach (int i in notsyskeys)
        {
            if (key == i)
                return false;
        }
        return true;
    }

    #region Event Handling
    /// <summary>
    /// Raises the KeyIntercepted event.
    /// </summary>
    /// <param name="e">An instance of KeyboardHookEventArgs</param>
    public void OnKeyIntercepted(KeyboardHookEventArgs e)
    {
        if (KeyIntercepted != null)
            KeyIntercepted(e);
    }

    /// <summary>
    /// Delegate for KeyboardHook event handling.
    /// </summary>
    /// <param name="e">An instance of InterceptKeysEventArgs.</param>
    public delegate void KeyboardHookEventHandler(KeyboardHookEventArgs e);

    /// <summary>
    /// Event arguments for the KeyboardHook class's KeyIntercepted event.
    /// </summary>
    public class KeyboardHookEventArgs : System.EventArgs
    {

        private string keyName;
        private int keyCode;
        private bool passThrough;

        /// <summary>
        /// The name of the key that was pressed.
        /// </summary>
        public string KeyName
        {
            get { return keyName; }
        }

        /// <summary>
        /// The virtual key code of the key that was pressed.
        /// </summary>
        public int KeyCode
        {
            get { return keyCode; }
        }

        /// <summary>
        /// True if this key combination was passed to other applications,
        /// false if it was trapped.
        /// </summary>
        public bool PassThrough
        {
            get { return passThrough; }
        }

        public KeyboardHookEventArgs(int evtKeyCode, bool evtPassThrough)
        {
            keyName = ((Keys)evtKeyCode).ToString();
            keyCode = evtKeyCode;
            passThrough = evtPassThrough;
        }

    }

    #endregion

    #region IDisposable Members
    /// <summary>
    /// Releases the keyboard hook.
    /// </summary>
    public void Dispose()
    {
        NativeMethods.UnhookWindowsHookEx(hookID);
    }
    #endregion

    #region Native methods

    [ComVisibleAttribute(false),
     System.Security.SuppressUnmanagedCodeSecurity()]
    internal class NativeMethods
    {
        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern IntPtr GetModuleHandle(string lpModuleName);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern IntPtr SetWindowsHookEx(int idHook,
            HookHandlerDelegate lpfn, IntPtr hMod, uint dwThreadId);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern IntPtr SetWindowsHookEx(int idHook,
            MHookHandlerDelegate lpfn, IntPtr hMod, uint dwThreadId);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool UnhookWindowsHookEx(IntPtr hhk);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode,
            IntPtr wParam, ref KBDLLHOOKSTRUCT lParam);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode,
            IntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true, CallingConvention = CallingConvention.Winapi)]
        public static extern short GetKeyState(int keyCode);
        
    } 
 

    #endregion
}
