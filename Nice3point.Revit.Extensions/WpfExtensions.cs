﻿using System.Windows;
using System.Windows.Interop;

namespace Nice3point.Revit.Extensions;

/// <summary>
///     Extensions to simplify the interaction of custom windows with Revit
/// </summary>
public static class WpfExtensions
{
    /// <summary>
    ///     Opens a window and returns without waiting for the newly opened window to close. 
    /// </summary>
    /// <exception cref="T:System.InvalidOperationException">
    /// <see cref="M:System.Windows.Window.Show" /> is called on a window that is closing (<see cref="E:System.Windows.Window.Closing" />) or has been closed (<see cref="E:System.Windows.Window.Closed" />)</exception>
    /// <param name="window">Child window</param>
    /// <param name="handle">Owner window handle</param>
    /// <example>_view.Show(uiApplication.MainWindowHandle)</example>
    public static void Show(this Window window, IntPtr handle)
    {
        var _ = new WindowInteropHelper(window)
        {
            Owner = handle
        };

        window.Show();
    }
}