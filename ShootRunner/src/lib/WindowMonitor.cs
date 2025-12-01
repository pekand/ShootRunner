using System.Runtime.InteropServices;
using System.Timers;

#pragma warning disable IDE0079
#pragma warning disable IDE0130
#pragma warning disable CA1822

namespace ShootRunner
{
    public class WindowMonitor
    {
        public IntPtr hook = IntPtr.Zero;
        Thread? eventHookThread = null;
        public List<IntPtr> taskbarWindows = [];
        public List<IntPtr> createdWindows = [];
        public List<IntPtr> excludedWindows = [];
        public List<IntPtr> includedWindows = [];

        private System.Timers.Timer? timer;

        public delegate void WindowCreate(IntPtr Handle);
        public delegate void WindowDestroy(IntPtr Handle);

        public event WindowCreate? OnWindowCreateTriggered;
        public event WindowDestroy? OnWindowDestroyTriggered;

        public void InitTaskbarWindowsList(bool allowExclude = false)
        {
            List<IntPtr> taskbarWindowsNew = [];

            try
            {
                List<IntPtr> windows = ToolsWindow.GetAlWindows();

                foreach (IntPtr Handle in windows)
                {
                    try
                    {
                        if (this.CheckWindowHandle(Handle, allowExclude) && !taskbarWindowsNew.Contains(Handle))
                        {
                            taskbarWindowsNew.Add(Handle);
                            
                        }
                    }
                    catch (Exception ex)
                    {
                        excludedWindows.Add(Handle);
                        Program.Error(ex.Message);
                    }

                }

                foreach (IntPtr Handle in taskbarWindowsNew)
                {
                    try
                    {
                        if (!taskbarWindows.Contains(Handle))
                        {
                            taskbarWindows.Add(Handle);
                            OnWindowCreateTriggered?.Invoke(Handle);
                        }
                    }
                    catch (Exception ex)
                    {
                        excludedWindows.Add(Handle);
                        Program.Error(ex.Message);
                    }

                }
            }
            catch (Exception ex)
            {

                Program.Error(ex.Message);
            }
        }

        public bool CheckWindowCandidateHandle(IntPtr Handle)
        {
            try
            {
                if (!WinApi.IsWindow(Handle))
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
                Program.Error(ex.Message);
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

                if (!WinApi.IsWindow(Handle))
                {
                    if (allowExclude && !excludedWindows.Contains(Handle)) excludedWindows.Add(Handle);
                    return false;
                }

                if (ToolsWindow.IsToolWindow(Handle))
                {
                    if (allowExclude && !excludedWindows.Contains(Handle)) excludedWindows.Add(Handle);
                    return false;
                }

                /*if (IsWindowPopup(window.Handle))
                {
                    excludedWindows.Add(window.Handle);
                    continue;
                }*/

                /*if (ToolsWindow.IsChild(Handle))
                {
                    //if (allowExclude && !excludedWindows.Contains(Handle)) excludedWindows.Add(Handle);
                    return false;
                }*/

                /*if (ToolsWindow.HaveParent(Handle))
                {
                    return false;
                }*/

                if (!WinApi.IsWindowVisible(Handle))
                {
                    return false;
                }

                if (!ToolsWindow.IsWindowVisible2(Handle))
                {
                    return false;
                }

                /*if (ToolsWindow.IsUWPWindow(Handle))
                {
                    if (allowExclude && !excludedWindows.Contains(Handle)) excludedWindows.Add(Handle);
                    return false;
                }*/

                if (ToolsWindow.IsWindowCloaked(Handle))
                {
                    //if (allowExclude && !excludedWindows.Contains(Handle)) excludedWindows.Add(Handle);
                    return false;
                }

                /*string className = ToolsWindow.GetWindowClassName(Handle);
                if (className == "Windows.UI.Core.CoreWindow")
                {  // is container window
                    if (allowExclude && !excludedWindows.Contains(Handle)) excludedWindows.Add(Handle);
                    return false;
                }*/

                if (includedWindows.Contains(Handle))
                {
                    return true;
                }

                if (Handle == ToolsWindow.GetShellWindowHandle()) // is explorer window
                {
                    if (allowExclude && !excludedWindows.Contains(Handle)) excludedWindows.Add(Handle);
                    return false;
                }

                string title = ToolsWindow.GetWindowTitle(Handle);

                if (string.IsNullOrEmpty(title))
                {
                    if (allowExclude && !excludedWindows.Contains(Handle)) excludedWindows.Add(Handle);
                    return false;
                }

                if (ToolsWindow.GetWindowSessionId(Handle) <= 0)
                {
                    if (allowExclude && !excludedWindows.Contains(Handle)) excludedWindows.Add(Handle);
                    return false;
                }

                if (!includedWindows.Contains(Handle)) {
                    includedWindows.Add(Handle);
                }

                return true;

            }
            catch (Exception ex)
            {
                excludedWindows.Add(Handle);
                Program.Error(ex.Message);
            }

            return false;
        }

        public void WinEventProc(IntPtr hWinEventHook, uint eventType, IntPtr hwnd, int idObject, int idChild, uint dwEventThread, uint dwmsEventTime)
        {



            if (idObject == 0 && idChild == 0) // OBJID_WINDOW
            {
                switch (eventType)
                {
                    case WinApi.EVENT_OBJECT_CREATE:
                        Program.Message("Event EVENT_OBJECT_CREATE");
                        break;

                    case WinApi.EVENT_OBJECT_DESTROY:
                        Program.Message("Event EVENT_OBJECT_DESTROY");
                        break;

                    case WinApi.EVENT_OBJECT_SHOW:
                        Program.Message("Event EVENT_OBJECT_SHOW");
                        break;

                    case WinApi.EVENT_OBJECT_HIDE:
                        Program.Message("Event EVENT_OBJECT_HIDE");
                        break;

                    case WinApi.EVENT_OBJECT_CLOAKED:
                        Program.Message("Event EVENT_OBJECT_CLOAKED");
                        break;
                    case WinApi.EVENT_OBJECT_UNCLOAKED:
                        Program.Message("Event EVENT_OBJECT_UNCLOAKED");
                        break;
                }


                Program.Message("WindowMonitor WinEventProc Start " + hwnd.ToString());
                if (eventType == WinApi.EVENT_OBJECT_CREATE)
                {
                    Program.Message("WindowMonitor WinEventProc EVENT_OBJECT_CREATE " + hwnd.ToString());

                    if (!createdWindows.Contains(hwnd))
                    {
                        if (this.CheckWindowCandidateHandle(hwnd))
                        {
                            Program.Message("WindowMonitor WinEventProc Found window candidate " + hwnd.ToString());
                            createdWindows.Add(hwnd);
                        }
                    }

                }
                else if ((eventType == WinApi.EVENT_OBJECT_SHOW || eventType == WinApi.EVENT_OBJECT_UNCLOAKED) && createdWindows.Contains(hwnd))
                {
                    Program.Message("WindowMonitor WinEventProc EVENT_OBJECT_SHOW " + hwnd.ToString());

                    if (!taskbarWindows.Contains(hwnd))
                    {
                        if (this.CheckWindowHandle(hwnd, true))
                        {
                            Program.Message("WindowMonitor WinEventProc Show window event " + hwnd.ToString());
                            taskbarWindows.Add(hwnd);
                            OnWindowCreateTriggered?.Invoke(hwnd);
                        }
                    }

                }
                else if ((eventType == WinApi.EVENT_OBJECT_HIDE || eventType == WinApi.EVENT_OBJECT_CLOAKED) && createdWindows.Contains(hwnd))
                {
                    Program.Message("WindowMonitor WinEventProc EVENT_OBJECT_HIDE " + hwnd.ToString());

                    includedWindows.Remove(hwnd);

                    excludedWindows.Remove(hwnd);

                    if (taskbarWindows.Remove(hwnd))
                    {
                        if (OnWindowDestroyTriggered != null)
                        {
                            Program.Message("WindowMonitor WinEventProc Remove window event " + hwnd.ToString());
                            OnWindowDestroyTriggered.Invoke(hwnd);
                        }
                    }
                }
                else if (eventType == WinApi.EVENT_OBJECT_DESTROY)
                {
                    Program.Message("WindowMonitor WinEventProc EVENT_OBJECT_DESTROY " + hwnd.ToString());

                    includedWindows.Remove(hwnd);
                    excludedWindows.Remove(hwnd);
                    createdWindows.Remove(hwnd);

                    if (taskbarWindows.Remove(hwnd))
                    {
                        if (OnWindowDestroyTriggered != null)
                        {
                            Program.Message("WindowMonitor WinEventProc Remove window event " + hwnd.ToString());
                            OnWindowDestroyTriggered.Invoke(hwnd);
                        }
                    }

                }
                    Program.Message("WindowMonitor WinEventProc End " + hwnd.ToString());
                }
        }

        public WinApi.WinEventDelegate? winEventDelegate;

        public void Register() {
            
            Program.Message("WindowMonitor Registration");

            this.InitTaskbarWindowsList(true);
            this.InitTimer();

            using var cancellationTokenSource = new CancellationTokenSource();
            var token = cancellationTokenSource.Token;
            winEventDelegate = WinEventProc;
            eventHookThread = new Thread(() => EventHookThread(WinApi.EVENT_OBJECT_CREATE, WinApi.EVENT_OBJECT_HIDE, winEventDelegate, token))
            {
                IsBackground = true
            };
            eventHookThread.Start();
        }
        private static uint _hookThreadId;

        private void EventHookThread(uint eventMin, uint eventMax, WinApi.WinEventDelegate winEventDelegate, CancellationToken token)
        {

            _hookThreadId = WinApi.GetCurrentThreadId();

            hook = WinApi.SetWinEventHook(
                eventMin,
                eventMax,
                IntPtr.Zero,
                winEventDelegate,
                0,
                0,
                WinApi.WINEVENT_OUTOFCONTEXT
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
                    while (WinApi.PeekMessage(out WinApi.MSG msg, IntPtr.Zero, 0, 0, WinApi.PM_REMOVE))
                    {
                        if (msg.message == WinApi.WM_CUSTOM_STOP)
                        {
                            running = false;
                            break;
                        }

                        WinApi.TranslateMessage(ref msg);
                        WinApi.DispatchMessage(ref msg);
                    }

                    Thread.Sleep(1);
                }
                catch (ThreadAbortException ex)
                {
                    Program.Error(ex.Message);
                }
                catch (Exception ex)
                {
                    Program.Error(ex.Message);
                }
            }

        }

        public void UnRegister()
        {
            Program.Message("WindowMonitor Unregister");

            if (hook == IntPtr.Zero || eventHookThread == null)
            {
                return;
            }

            WinApi.PostThreadMessage(_hookThreadId, WinApi.WM_CUSTOM_STOP, IntPtr.Zero, IntPtr.Zero);
            eventHookThread.Join();

            WinApi.UnhookWinEvent(hook);

            Program.Message("WindowMonitor Thread {_hookThreadId} closed succerusfuly");
        }

        public void InitTimer() {
            if (timer != null) { 
                return;
            }

            timer = new System.Timers.Timer(10000);
            timer.Elapsed += OnTimedEvent;
            timer.AutoReset = true;
            timer.Enabled = true;
        }

        public void UpdateTaskbarWindowsList(bool allowExclude = false)
        {
            List<IntPtr> taskbarWindowsAdd = [];
            List<IntPtr> taskbarWindowsRemove = [];

            try
            {
                List<IntPtr> windows = ToolsWindow.GetAlWindows();

                // REMOVE OLD
                foreach (IntPtr Handle in this.taskbarWindows)
                {
                    if (!windows.Contains(Handle)) {
                        includedWindows.Remove(Handle);
                        excludedWindows.Remove(Handle);
                        createdWindows.Remove(Handle);

                        if (this.taskbarWindows.Contains(Handle) &&
                            !taskbarWindowsRemove.Contains(Handle)
                        ) {
                            taskbarWindowsRemove.Add(Handle);
                        }
                    }
                }

                foreach (IntPtr Handle in windows)
                {
                    try
                    {
                        if (this.CheckWindowHandle(Handle, allowExclude))
                        {
                            if (!this.taskbarWindows.Contains(Handle) &&
                                !taskbarWindowsAdd.Contains(Handle)
                            ) {
                                taskbarWindowsAdd.Add(Handle);
                            }
                        } else {
                            if (this.taskbarWindows.Contains(Handle) &&
                                !taskbarWindowsRemove.Contains(Handle)
                            ) {
                                taskbarWindowsRemove.Add(Handle);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        excludedWindows.Add(Handle);
                        Program.Error(ex.Message);
                    }

                }

                foreach (IntPtr Handle in taskbarWindowsAdd)
                {
                    try
                    {
                        if (!this.taskbarWindows.Contains(Handle))
                        {
                            this.taskbarWindows.Add(Handle);
                            if (OnWindowCreateTriggered != null)
                            {
                                Program.Message("WindowMonitor TIMER add window event " + Handle.ToString());
                                OnWindowCreateTriggered.Invoke(Handle);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        excludedWindows.Add(Handle);
                        Program.Error(ex.Message);
                    }
                }

                foreach (IntPtr Handle in taskbarWindowsRemove)
                {
                    try
                    {
                        
                        if (this.taskbarWindows.Remove(Handle))
                        {
                            if (OnWindowDestroyTriggered != null)
                            {
                                Program.Message("WindowMonitor TIMER Remove window event " + Handle.ToString());
                                OnWindowDestroyTriggered.Invoke(Handle);
                            }
                        }
                        
                    }
                    catch (Exception ex)
                    {
                        excludedWindows.Add(Handle);
                        Program.Error(ex.Message);
                    }
                }


            }
            catch (Exception ex)
            {
                Program.Error(ex.Message);
            }
        }

        private void OnTimedEvent(Object? source, ElapsedEventArgs e)
        {
            this.UpdateTaskbarWindowsList(true);
        }
    }

    
}
