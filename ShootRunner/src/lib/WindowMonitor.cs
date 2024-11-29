using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ShootRunner
{
    public class WindowMonitor
    {
        public IntPtr hook = IntPtr.Zero;
        Thread? eventHookThread = null;
        public List<IntPtr> taskbarWindows = new List<IntPtr>();
        public List<IntPtr> createdWindows = new List<IntPtr>();
        public List<IntPtr> excludedWindows = new List<IntPtr>();
        public List<IntPtr> includedWindows = new List<IntPtr>();

        [StructLayout(LayoutKind.Sequential)]
        public struct MSG
        {
            public IntPtr hwnd;
            public uint message;
            public IntPtr wParam;
            public IntPtr lParam;
            public uint time;
            public int pt_x;
            public int pt_y;
        }

        const uint EVENT_OBJECT_CREATE = 0x8000;
        const uint EVENT_OBJECT_DESTROY = 0x8001;
        const uint EVENT_OBJECT_SHOW = 0x8002;
        const uint WINEVENT_OUTOFCONTEXT = 0x0000;
        private const uint PM_REMOVE = 0x0001;

        private const int INFINITE = -1;
        private const int WAIT_OBJECT_0 = 0;
        private const int WAIT_TIMEOUT = 0x102;
        private const uint QS_ALLEVENTS = 0x04FF;

        private const int WM_QUIT = 0x0012;
        private const int WM_CUSTOM_STOP = 0x9001; // Custom message to stop

        public delegate void WindowCreate(IntPtr Handle);
        public delegate void WindowDestroy(IntPtr Handle);

        public event WindowCreate? OnWindowCreateTriggered;
        public event WindowDestroy? OnWindowDestroyTriggered;


        public delegate void WinEventDelegate(
            IntPtr hWinEventHook, uint eventType, IntPtr hwnd, int idObject, int idChild,
            uint dwEventThread, uint dwmsEventTime);

        [DllImport("user32.dll")]
        static extern IntPtr SetWinEventHook(
            uint eventMin, uint eventMax, IntPtr hmodWinEventProc,
            WinEventDelegate lpfnWinEventProc, uint idProcess, uint idThread, uint dwFlags);

        [DllImport("user32.dll")]
        static extern bool UnhookWinEvent(IntPtr hWinEventHook);

        [DllImport("user32.dll")]
        private static extern int GetMessage(out MSG lpMsg, IntPtr hWnd, uint wMsgFilterMin, uint wMsgFilterMax);

        [DllImport("user32.dll")]
        private static extern bool PostThreadMessage(uint idThread, uint Msg, IntPtr wParam, IntPtr lParam);

        [DllImport("kernel32.dll")]
        private static extern uint GetCurrentThreadId();

        [DllImport("user32.dll")]
        private static extern bool PeekMessage(out MSG lpMsg, IntPtr hWnd, uint wMsgFilterMin, uint wMsgFilterMax, uint wRemoveMsg);

        [DllImport("user32.dll")]
        private static extern uint MsgWaitForMultipleObjects(uint nCount, IntPtr[] pHandles, bool bWaitAll, uint dwMilliseconds, uint dwWakeMask);

        [DllImport("user32.dll")]
        public static extern IntPtr DispatchMessage(ref MSG lpmsg);

        [DllImport("user32.dll")]
        public static extern IntPtr TranslateMessage(ref MSG lpmsg);

        public void InitTaskbarWindowsList(bool allowExclude = false)
        {
            List<IntPtr> taskbarWindows = new List<IntPtr>();

            try
            {
                List<IntPtr> windows = ToolsWindow.GetAlWindows();

                foreach (IntPtr Handle in windows)
                {
                    try
                    {
                        if (this.CheckWindowHandle(Handle, allowExclude))
                        {
                            taskbarWindows.Add(Handle);
                            
                        }
                    }
                    catch (Exception ex)
                    {
                        excludedWindows.Add(Handle);
                        Program.error(ex.Message);
                    }

                }

                foreach (IntPtr Handle in taskbarWindows)
                {
                    try
                    {
                        if (OnWindowCreateTriggered != null)
                        {
                            OnWindowCreateTriggered.Invoke(Handle);
                        }
                    }
                    catch (Exception ex)
                    {
                        excludedWindows.Add(Handle);
                        Program.error(ex.Message);
                    }

                }
            }
            catch (Exception ex)
            {

                Program.error(ex.Message);
            }
        }

        public bool CheckWindowCandidateHandle(IntPtr Handle)
        {
            try
            {
                if (!ToolsWindow.IsWindow(Handle))
                {
                    return false;
                }

                if (ToolsWindow.IsToolWindow(Handle))
                {
                    return false;
                }

                if (ToolsWindow.IsChild(Handle))
                {
                    return false;
                }

                if (ToolsWindow.HaveParent(Handle))
                {
                    return false;
                }

                string title = ToolsWindow.GetWindowTitle(Handle);

                if (string.IsNullOrEmpty(title))
                {
                    return false;
                }

                if (ToolsWindow.GetWindowSessionId(Handle) <= 0)
                {
                    return false;
                }

                return true;

            }
            catch (Exception ex)
            {
                Program.error(ex.Message);
            }

            return false;
        }

        public bool CheckWindowHandle(IntPtr Handle, bool allowExclude = false) {
            try
            {
                /*Window w = new Window(); //debug
                w.Handle = Handle;
                ToolsWindow.SetWindowData(w);*/

                if (excludedWindows.Contains(Handle))
                {
                    return false;
                }

                if (!ToolsWindow.IsWindow(Handle))
                {
                    return false;
                }

                if (ToolsWindow.IsToolWindow(Handle))
                {
                    //if (allowExclude) excludedWindows.Add(Handle);
                    return false;
                }

                if (ToolsWindow.IsChild(Handle))
                {
                    //if (allowExclude) excludedWindows.Add(Handle);
                    return false;
                }

                if (ToolsWindow.HaveParent(Handle))
                {
                    return false;
                }

                if (!ToolsWindow.IsWindowVisible(Handle))
                {
                    if (allowExclude) excludedWindows.Add(Handle);
                    return false;
                }

                if (!ToolsWindow.IsWindowVisible2(Handle))
                {
                    if (allowExclude) excludedWindows.Add(Handle);
                    return false;
                }

                /*if (IsWindowPopup(window.Handle))
                {
                    excludedWindows.Add(window.Handle);
                    continue;
                }*/

                if (ToolsWindow.IsUWPWindow(Handle))
                {
                    if (ToolsWindow.IsWindowCloaked(Handle)) {
                        Thread.Sleep(200);
                    }

                    if (ToolsWindow.IsWindowCloaked(Handle))
                    {
                        if (allowExclude) excludedWindows.Add(Handle);
                        return false;
                    }
                    
                }

                if (includedWindows.Contains(Handle))
                {
                    taskbarWindows.Add(Handle);
                    return true;
                }

                

                if (Handle == ToolsWindow.GetShellWindowHandle()) // is explorer window
                {
                    if (allowExclude) excludedWindows.Add(Handle);
                    return false;
                }

                string title = ToolsWindow.GetWindowTitle(Handle);

                if (string.IsNullOrEmpty(title))
                {
                    if (allowExclude) excludedWindows.Add(Handle);
                    return false;
                }

                string className = ToolsWindow.GetWindowClassName(Handle);
                if (className == "Windows.UI.Core.CoreWindow")
                {  // is container window
                    if (allowExclude) excludedWindows.Add(Handle);
                    return false;
                }

                if (ToolsWindow.GetWindowSessionId(Handle) <= 0)
                {
                    if (allowExclude) excludedWindows.Add(Handle);
                    return false;
                }

                includedWindows.Add(Handle);

                taskbarWindows.Add(Handle);

                return true;

            }
            catch (Exception ex)
            {
                excludedWindows.Add(Handle);
                Program.error(ex.Message);
            }

            return false;
        }

        public void WinEventProc(IntPtr hWinEventHook, uint eventType, IntPtr hwnd, int idObject, int idChild, uint dwEventThread, uint dwmsEventTime)
        {
            Program.message("WindowMonitor WinEventProc Start "+ hwnd.ToString());
            if (idObject == 0 && idChild == 0) // OBJID_WINDOW
            {
                if (eventType == EVENT_OBJECT_CREATE)
                {
                    Program.message("WindowMonitor WinEventProc EVENT_OBJECT_CREATE " + hwnd.ToString());

                    if (!createdWindows.Contains(hwnd)) {
                        if (this.CheckWindowCandidateHandle(hwnd))
                        {
                            Program.message("WindowMonitor WinEventProc Found window candidate " + hwnd.ToString());
                            createdWindows.Add(hwnd);
                        }
                    }

                } else if (eventType == EVENT_OBJECT_SHOW)
                {
                    Program.message("WindowMonitor WinEventProc EVENT_OBJECT_SHOW " + hwnd.ToString());

                    if (createdWindows.Contains(hwnd))
                    {
                        createdWindows.Remove(hwnd);
                        if (!taskbarWindows.Contains(hwnd))
                        {
                            if (this.CheckWindowHandle(hwnd, true))
                            {
                                Program.message("WindowMonitor WinEventProc Show window event " + hwnd.ToString());
                                taskbarWindows.Add(hwnd);
                                if (OnWindowCreateTriggered != null)
                                {
                                    OnWindowCreateTriggered.Invoke(hwnd);
                                }
                            }
                        }
                    }
                }
                else if (eventType == EVENT_OBJECT_DESTROY)
                {
                    Program.message("WindowMonitor WinEventProc EVENT_OBJECT_DESTROY " + hwnd.ToString());

                    if (includedWindows.Contains(hwnd))
                    {
                        includedWindows.Remove(hwnd);
                    }

                    if (excludedWindows.Contains(hwnd))
                    {
                        excludedWindows.Remove(hwnd);
                    }

                    if (createdWindows.Contains(hwnd))
                    {
                        createdWindows.Remove(hwnd);
                    }

                    if (taskbarWindows.Contains(hwnd)) {
                        taskbarWindows.Remove(hwnd);
                        if (OnWindowDestroyTriggered != null)
                        {
                            Program.message("WindowMonitor WinEventProc Remove window event " + hwnd.ToString());
                            OnWindowDestroyTriggered.Invoke(hwnd);
                        }                        
                    }
                }
            }

            Program.message("WindowMonitor WinEventProc End " + hwnd.ToString());
        }

        public WinEventDelegate? winEventDelegate;

        public void Register() {
            
            Program.message("WindowMonitor Registration");

            this.InitTaskbarWindowsList();
            using (var cancellationTokenSource = new CancellationTokenSource())
            {
                var token = cancellationTokenSource.Token;
                winEventDelegate = WinEventProc;
                eventHookThread = new Thread(() => EventHookThread(token, winEventDelegate))
                {
                    IsBackground = true
                };
                eventHookThread.Start();
            }
        }
        private static uint _hookThreadId;

        private void EventHookThread(CancellationToken token, WinEventDelegate winEventDelegate)
        {

            _hookThreadId = GetCurrentThreadId();

            hook = SetWinEventHook(
                EVENT_OBJECT_CREATE,
                EVENT_OBJECT_SHOW,
                IntPtr.Zero,
                winEventDelegate,
                0,
                0,
                WINEVENT_OUTOFCONTEXT
            );

            if (hook == IntPtr.Zero)
            {
                return;
            }

            bool running = true;

            while (running && !token.IsCancellationRequested)
            {
                try
                {
                    while (PeekMessage(out MSG msg, IntPtr.Zero, 0, 0, PM_REMOVE))
                    {
                        if (msg.message == WM_CUSTOM_STOP)
                        {
                            running = false;
                            break;
                        }

                        TranslateMessage(ref msg);
                        DispatchMessage(ref msg);
                    }

                    Thread.Sleep(1);
                }
                catch (ThreadAbortException ex)
                {
                    Program.error(ex.Message);
                }
                catch (Exception ex)
                {
                    Program.error(ex.Message);
                }
            }

        }

        public void UnRegister()
        {
            Program.message("WindowMonitor Unregister");

            if (hook == IntPtr.Zero || eventHookThread == null)
            {
                return;
            }

            PostThreadMessage(_hookThreadId, WM_CUSTOM_STOP, IntPtr.Zero, IntPtr.Zero);
            eventHookThread.Join();

            UnhookWinEvent(hook);

            Program.message("WindowMonitor Thread {_hookThreadId} closed succerusfuly");
        }
    }

    
}
