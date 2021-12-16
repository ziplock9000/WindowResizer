using System.Runtime.InteropServices;

namespace WindowResizer.Core;

public sealed class KeyboardHook : IDisposable
{
    [DllImport("user32.dll")]
    private static extern bool RegisterHotKey(IntPtr hWnd, int id, uint fsModifiers, uint vk);

    [DllImport("user32.dll")]
    private static extern bool UnregisterHotKey(IntPtr hWnd, int id);

    private class Window : NativeWindow, IDisposable
    {
        private const int WmHotkey = 0x0312;

        public Window()
        {
            NewHandle();
        }

        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);

            if (m.Msg != WmHotkey)
                return;

            var key = (Keys)(((int)m.LParam >> 16) & 0xFFFF);
            var modifier = (ModifierKeys)((int)m.LParam & 0xFFFF);
            KeyPressed?.Invoke(this, new KeyPressedEventArgs(modifier, key));
        }

        private void NewHandle()
        {
            CreateHandle(new CreateParams());
        }

        public event EventHandler<KeyPressedEventArgs>? KeyPressed;

        #region IDisposable Members

        public void Dispose()
        {
            DestroyHandle();
        }

        #endregion
    }

    private readonly Window _window = new();
    private int _currentId;

    public KeyboardHook()
    {
        _window.KeyPressed += delegate(object? _, KeyPressedEventArgs args)
        {
            KeyPressed?.Invoke(this, args);
        };
    }

    public void RegisterHotKey(ModifierKeys modifier, Keys key)
    {
        _currentId += 1;

        if (!RegisterHotKey(_window.Handle, _currentId, (uint)modifier, (uint)key))
        {
            throw new InvalidOperationException("Couldn't register the hot key.");
        }
    }

    public void UnRegisterHotKey()
    {
        for (var i = _currentId; i > 0; i--)
        {
            UnregisterHotKey(_window.Handle, i);
        }
    }

    /// <summary>
    /// A hot key has been pressed.
    /// </summary>
    public event EventHandler<KeyPressedEventArgs>? KeyPressed;

    #region IDisposable Members

    public void Dispose()
    {
        UnRegisterHotKey();
        _window.Dispose();
    }

    #endregion
}

public class KeyPressedEventArgs : EventArgs
{
    internal KeyPressedEventArgs(ModifierKeys modifier, Keys key)
    {
        Modifier = modifier;
        Key = key;
    }

    public ModifierKeys Modifier { get; }

    public Keys Key { get; }
}


[Flags]
public enum ModifierKeys : uint
{
    Alt = 1,
    Ctrl = 2,
    Shift = 4,
    Win = 8
}
