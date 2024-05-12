using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Microsoft.Win32;

namespace DiffPlex_השוואת_טקסטים
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            LoadData();
        }

        private void LoadData()
        {
            setColor(Properties.Settings.Default.IsDarkMode);

            Grid grid = DiffView.Content as Grid;
            //drill doen grid and set all textblocks to wrapwithoverflow

            //DiffView.OldText = TestData.DuplicateText(TestData.OldText, 100);
            //DiffView.NewText = TestData.DuplicateText(TestData.NewText, 100);
            //DiffView.SetHeaderAsOldToNew();        
            //DiffButton.Background = FutherActionsButton.Background = WindowButton.Background = new SolidColorBrush(isDark ? Color.FromRgb(80, 160, 240) : Color.FromRgb(160, 216, 240));
            //IgnoreUnchangedCheckBox.Content = TestData.RemoveHotkey(DiffView.CollapseUnchangedSectionsToggleTitle);
            //MarginLineCountLabel.Content = TestData.RemoveHotkey(DiffView.ContextLinesMenuItemsTitle);
        }

      
        void setColor(bool isDark)
        {
            DarkMode.Content = isDark ? '☼' : '☽';
            Properties.Settings.Default.IsDarkMode = isDark;
            Properties.Settings.Default.Save();
            DiffView.Foreground = new SolidColorBrush(isDark ? Color.FromRgb(240, 240, 240) : Color.FromRgb(32, 32, 32));
            Background = new SolidColorBrush(isDark ? Color.FromRgb(32, 32, 32) : Color.FromRgb(251, 251, 251));
        }

        private void DiffButton_Click(object sender, RoutedEventArgs e)
        {
            if (DiffView.IsInlineViewMode)
            {
                DiffView.ShowSideBySide();
                return;
            }

            DiffView.ShowInline();
        }

        private void FutherActionsButton_Click(object sender, RoutedEventArgs e)
        {
            DiffView.OpenViewModeContextMenu();
        }

        private void WindowButton_Click(object sender, RoutedEventArgs e)
        {
           MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
        }       

        private void PasteOldText_Click(object sender, RoutedEventArgs e)
        {
            string textToPaste = Clipboard.GetText();
            if (!string.IsNullOrWhiteSpace(textToPaste))
            {
                DiffView.OldText = textToPaste;
                if (string.IsNullOrWhiteSpace(DiffView.NewText))
                {
                    DiffView.NewText = "אנא הזן טקסט להשוואה";
                }
            }
        }

        private void OpenOldTextFile_Click(object sender, RoutedEventArgs e)
        {
            DiffView.OpenFileOnLeft();
            if (!string.IsNullOrWhiteSpace(DiffView.OldText))
            {
                DiffView.NewText = "אנא הזן טקסט להשוואה";
            }
        }

        private void PasteNewText_Click(object sender, RoutedEventArgs e)
        {
            string textToPaste = Clipboard.GetText();
            if (!string.IsNullOrWhiteSpace(textToPaste))
            {
                DiffView.NewText = textToPaste;
                if (string.IsNullOrWhiteSpace(DiffView.OldText))
                {
                    DiffView.OldText = "אנא הזן טקסט להשוואה";
                }
            }
        }

        private void OpenNewTextFile_Click(object sender, RoutedEventArgs e)
        {
            DiffView.OpenFileOnRight();
            if (!string.IsNullOrWhiteSpace(DiffView.NewText))
            {
                DiffView.OldText = "אנא הזן טקסט להשוואה";
            }
        }

        private void ClearOldText_Click(object sender, RoutedEventArgs e)
        {           
            if (!string.IsNullOrWhiteSpace(DiffView.NewText))
            {
                DiffView.OldText = "אנא הזן טקסט להשוואה";
            }
            else { DiffView.OldText = ""; }
        }

        private void ClearNewText_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(DiffView.OldText))
            {
                DiffView.NewText = "אנא הזן טקסט להשוואה";
            }
            else { DiffView.NewText = ""; }

            
        }

        private void DarkMode_Click(object sender, RoutedEventArgs e)
        {
            setColor(!Properties.Settings.Default.IsDarkMode);
        }

        private void SearchPreviousButton_Click(object sender, RoutedEventArgs e)
        {
            DiffView.PreviousDiff();
        }

        private void SearchNextButton_Click(object sender, RoutedEventArgs e)
        {
            DiffView.NextDiff();
        }
    }
}
