using System;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using WindowResizer.Properties;

namespace WindowResizer
{
    public class TrayContext : ApplicationContext
    {
        private readonly NotifyIcon _trayIcon;
        private readonly KeyboardHook _hook = new KeyboardHook();

        private static SettingForm _settingForm;

        private static WindowEventHandler _windowEventHandler;

        public TrayContext()
        {
            try
            {
                ConfigLoader.Load();
            }
            catch (Exception e)
            {
                MessageBox.Show($"WindowResizer: config load failed! Exception: {e.Message}");
            }

            try
            {
                RegisterHotkey();
            }
            catch (Exception e)
            {
                MessageBox.Show($"WindowResizer: register hotkey failed! Exception: {e.Message}");
            }

            _trayIcon = new NotifyIcon
            {
                Icon = Resources.AppIcon,
                ContextMenu =
                    new ContextMenu(new MenuItem[] { new MenuItem("Setting", OnSetting), new MenuItem("Exit", OnExit) }),
                Visible = true,
                Text = "WindowResizer"
            };

            _trayIcon.DoubleClick += OnSetting;

            _windowEventHandler = new WindowEventHandler(OnWindowCreated);
            _windowEventHandler.AddWindowCreateHandle();
        }

        private void OnExit(object sender, EventArgs e)
        {
            _settingForm?.Close();
            _windowEventHandler.RemoveWindowCreateHandle();
            ConfigLoader.Save();
            _trayIcon.Dispose();
            _hook.Dispose();
            Environment.Exit(0);
        }

        private void OnSetting(object sender, EventArgs e)
        {
            if (_settingForm == null)
            {
                _settingForm = new SettingForm(_hook);
            }

            _settingForm.Show();
        }

        private void RegisterHotkey()
        {
            if (!ConfigLoader.Config.SaveKey.ValidateKeys())
            {
                MessageBox.Show("Save window hotkeys not valid.");
            }

            if (!ConfigLoader.Config.RestoreKey.ValidateKeys())
            {
                MessageBox.Show("Restore window hotkeys not valid.");
            }

            _hook.RegisterHotKey(ConfigLoader.Config.SaveKey.GetModifierKeys(), ConfigLoader.Config.SaveKey.GetKey());
            _hook.RegisterHotKey(ConfigLoader.Config.RestoreKey.GetModifierKeys(), ConfigLoader.Config.RestoreKey.GetKey());
            _hook.RegisterHotKey(ConfigLoader.Config.RestoreAllKey.GetModifierKeys(), ConfigLoader.Config.RestoreAllKey.GetKey());
            _hook.KeyPressed += OnKeyPressed;
        }

        private void OnKeyPressed(object sender, KeyPressedEventArgs e)
        {
            if (ConfigLoader.Config.DisableInFullScreen && WindowControl.IsForegroundFullScreen())
            {
                return;
            }

            if (ConfigLoader.Config.WindowSizes == null)
            {
                ConfigLoader.Config.WindowSizes = new BindingList<WindowSize>();
            }

            if (e.Modifier == ConfigLoader.Config.RestoreAllKey.GetModifierKeys()
                && e.Key == ConfigLoader.Config.RestoreAllKey.GetKey())
            {
                var windows = WindowControl.GetOpenWindows();
                windows.Reverse();
                foreach (var window in windows)
                {
                    ResizeWindow(window, false);
                }
            }
            else
            {
                var handle = WindowControl.GetForegroundHandle();

                if (e.Modifier == ConfigLoader.Config.SaveKey.GetModifierKeys() &&
                    e.Key == ConfigLoader.Config.SaveKey.GetKey())
                {
                    UpdateOrSaveWindowSize(handle);
                }
                else if (e.Modifier == ConfigLoader.Config.RestoreKey.GetModifierKeys() &&
                    e.Key == ConfigLoader.Config.RestoreKey.GetKey())
                {
                    ResizeWindow(handle, true);
                }
            }
        }

        private void OnWindowCreated(IntPtr handle)
        {
            if (WindowControl.IsWindowVisible(handle))
            {
                ResizeWindow(handle, false, true);
            }
        }

        private void ResizeWindow(IntPtr handle, bool tips = false, bool auto = false)
        {
            if (WindowControl.IsChildWindow(handle))
            {
                return;
            }

            var process = WindowControl.GetRealProcess(handle);
            if (process == null) return;

            var processName = process.MainModule?.ModuleName;
            if (string.IsNullOrWhiteSpace(processName)) return;

            var title = process.MainWindowTitle;
            var match = GetMatchWindowSize(ConfigLoader.Config.WindowSizes, processName, title, auto);
            if (!match.NoMatch)
            {
                MoveMatchWindow(match, handle);
            }
            else
            {
                if (tips)
                {
                    var titleStr = string.IsNullOrWhiteSpace(title) ? "" : $"({title})";
                    _trayIcon.ShowBalloonTip(2000, "WindowResizer",
                        $"No saved settings for <{processName}>{titleStr}.", ToolTipIcon.Info);
                }
            }
        }

        private void UpdateOrSaveWindowSize(IntPtr handle)
        {
            var process = WindowControl.GetRealProcess(handle);
            var processName = process.MainModule?.ModuleName;
            var title = process.MainWindowTitle;
            var match = GetMatchWindowSize(ConfigLoader.Config.WindowSizes, processName, title);
            var state = WindowControl.GetWindowState(handle);
            UpdateOrSaveConfig(match, processName, title, WindowControl.GetRect(handle), state);
        }

        private static MatchWindowSize GetMatchWindowSize(BindingList<WindowSize> windowSizes, string processName,
            string title, bool auto = false)
        {
            var windows = windowSizes.Where(w => w.Name.Equals(processName, StringComparison.OrdinalIgnoreCase)).ToList();

            if (auto)
            {
                windows = windows.Where(w => w.AutoResize).ToList();
            }

            return new MatchWindowSize
            {
                FullMatch = windows.FirstOrDefault(w => w.Title == title),
                PrefixMatch = windows.FirstOrDefault(w =>
                    w.Title.StartsWith("*") && w.Title.Length > 1 && title.EndsWith(w.Title.TrimStart('*'))),
                SuffixMatch = windows.FirstOrDefault(w =>
                    w.Title.EndsWith("*") && w.Title.Length > 1 && title.StartsWith(w.Title.TrimEnd('*'))),
                WildcardMatch = windows.FirstOrDefault(w => w.Title.Equals("*"))
            };
        }

        private static void MoveMatchWindow(MatchWindowSize match, IntPtr handle)
        {
            if (match.FullMatch != null)
            {
                MoveWindow(handle, match.FullMatch);
                return;
            }

            if (match.PrefixMatch != null)
            {
                MoveWindow(handle, match.PrefixMatch);
                return;
            }

            if (match.SuffixMatch != null)
            {
                MoveWindow(handle, match.SuffixMatch);
                return;
            }

            if (match.WildcardMatch != null)
            {
                MoveWindow(handle, match.WildcardMatch);
            }
        }

        private static void MoveWindow(IntPtr handle, WindowSize match)
        {
            if (match.State == WindowState.Maximized)
            {
                WindowControl.MaximizeWindow(handle);
            }
            else
            {
                WindowControl.MoveWindow(handle, match.Rect);
            }
        }

        private static void UpdateOrSaveConfig(MatchWindowSize match, string processName, string title, Rect rect, WindowState state = WindowState.Normal)
        {
            if (string.IsNullOrWhiteSpace(processName)) return;

            if (match.NoMatch)
            {
                // Add a wildcard match for all titles
                InsertOrder(new WindowSize { Name = processName, Title = "*", Rect = rect, State = state });
                if (string.IsNullOrWhiteSpace(title))
                {
                    InsertOrder(new WindowSize { Name = processName, Title = title, Rect = rect, State = state });
                }

                ConfigLoader.Save();
                return;
            }

            if (match.FullMatch != null)
            {
                match.FullMatch.Rect = rect;
                match.FullMatch.State = state;
            }
            else if (!string.IsNullOrWhiteSpace(title))
            {
                InsertOrder(new WindowSize { Name = processName, Title = title, Rect = rect, State = state });
            }

            if (match.SuffixMatch != null)
            {
                match.SuffixMatch.Rect = rect;
                match.SuffixMatch.State = state;
            }

            if (match.PrefixMatch != null)
            {
                match.PrefixMatch.Rect = rect;
                match.PrefixMatch.State = state;
            }

            if (match.WildcardMatch != null)
            {
                match.WildcardMatch.Rect = rect;
                match.WildcardMatch.State = state;
            }
            else
            {
                InsertOrder(new WindowSize { Name = processName, Title = "*", Rect = rect, State = state });
            }

            ConfigLoader.Save();
        }

        private static void InsertOrder(WindowSize item)
        {
            var list = ConfigLoader.Config.WindowSizes;
            var backing = list.ToList();
            backing.Add(item);
            var index = backing.OrderBy(l => l.Name).ThenBy(l => l.Title).ToList().IndexOf(item);
            list.Insert(index, item);
        }
    }
}
