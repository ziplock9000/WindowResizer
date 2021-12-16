﻿using System.Windows.Automation;

namespace WindowResizer.Library
{
    public class WindowEventHandler
    {
        private static AutomationEventHandler? _eventHandler;

        public WindowEventHandler(Action<IntPtr> windowCreatedHandler)
        {
            _eventHandler = (sender, _) =>
            {
                var src = sender as AutomationElement;
                if (src != null)
                {
                    windowCreatedHandler(new IntPtr(src.Current.NativeWindowHandle));
                }
            };
        }

        public void AddWindowCreateHandle() =>
            Automation.AddAutomationEventHandler(
                WindowPattern.WindowOpenedEvent,
                AutomationElement.RootElement,
                TreeScope.Subtree,
                _eventHandler
            );

        public void RemoveWindowCreateHandle()
        {
            Automation.RemoveAutomationEventHandler(
                WindowPattern.WindowOpenedEvent,
                AutomationElement.RootElement,
                _eventHandler
            );
        }
    }
}