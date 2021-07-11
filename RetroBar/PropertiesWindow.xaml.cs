﻿using System.ComponentModel;
using ManagedShell.Common.Helpers;
using RetroBar.Utilities;
using System.Windows;

namespace RetroBar
{
    /// <summary>
    /// Interaction logic for PropertiesWindow.xaml
    /// </summary>
    public partial class PropertiesWindow : Window
    {
        private static PropertiesWindow _instance;

        private readonly ThemeManager _themeManager;

        private PropertiesWindow(ThemeManager themeManager)
        {
            _themeManager = themeManager;

            InitializeComponent();

            LoadThemes();

            Left = 10;
            Top = (ScreenHelper.PrimaryMonitorDeviceSize.Height / DpiHelper.DpiScale) - Height - 40;
        }

        public static void Open(ThemeManager themeManager)
        {
            if (_instance == null)
            {
                _instance = new PropertiesWindow(themeManager);
                _instance.Show();
            }
            else
            {
                _instance.Activate();
            }
        }

        private void LoadThemes()
        {
            foreach (var theme in _themeManager.GetThemes())
            {
                cboThemeSelect.Items.Add(theme);
            }
        }

        private void OK_OnClick(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void SetQuickLaunchLocation_OnClick(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.FolderBrowserDialog fbd = new System.Windows.Forms.FolderBrowserDialog();
            fbd.Description = "Quick Launch - Choose a folder";
            fbd.UseDescriptionForTitle = true;
            fbd.ShowNewFolderButton = false;
            fbd.SelectedPath = Settings.Instance.QuickLaunchPath;

            if (fbd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                Settings.Instance.QuickLaunchPath = fbd.SelectedPath;
            }
        }

        private void PropertiesWindow_OnClosing(object sender, CancelEventArgs e)
        {
            _instance = null;
        }
    }
}
