using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace RepairFlatWPF.ViewModel
{
    class MainWindowViewModel: BaseViewModel
    {
        private Window mWindow;

        private int mOuterMarginSize = 10;
        private int mWindowRadius = 10;
        public MainWindowViewModel(Window window)
        {
            mWindow = window;
            mWindow.StateChanged += (sender, e) => 
            {
                OnPropertyChanged(nameof(ResizeBorderThickness));
                OnPropertyChanged(nameof(OuterMarginSize));
                OnPropertyChanged(nameof(OuterMarginSizeThickness));
                OnPropertyChanged(nameof(WindowRadius));
                OnPropertyChanged(nameof(WindowCornerRadius));
            };
        }

        #region PublicMember

        public int ResizeBorder { get; set; } = 10;

        public Thickness ResizeBorderThickness { get { return new Thickness(ResizeBorder+OuterMarginSize); } }

        public int OuterMarginSize
        {
            get
            {
                return mWindow.WindowState == WindowState.Maximized ? 0: mOuterMarginSize ;
            }
            set
            {
                mOuterMarginSize = value;
            }
        }

        public Thickness OuterMarginSizeThickness { get { return new Thickness(OuterMarginSize); } }

        public int WindowRadius
        {
            get
            {
                return mWindow.WindowState == WindowState.Maximized ? 0 : mWindowRadius;
            }
            set
            {
                mWindowRadius = value;
            }
        }

        public CornerRadius WindowCornerRadius { get { return new CornerRadius(WindowRadius); } }

        public int TitleHeight { get; set; } = 35;

        public GridLength TitleHeightGridLength { get { return new GridLength(TitleHeight+ResizeBorder); } }

        #endregion
    }
}
