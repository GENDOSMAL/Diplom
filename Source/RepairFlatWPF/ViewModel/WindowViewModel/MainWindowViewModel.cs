using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace RepairFlatWPF.ViewModel
{
    class MainWindowViewModel: BaseViewModel
    {
        private Window mWindow;

        private int mOuterMarginSize = 10;
        private int mWindowRadius = 10;

        public MainWindowViewModel()
        {

        }
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
            MinimazeCommand = new RelayCommand(() => mWindow.WindowState = WindowState.Minimized);
            MaximazeCommand = new RelayCommand(() => mWindow.WindowState ^= WindowState.Maximized);
            CloseCommand = new RelayCommand(() => mWindow.Close());
            MenuCommand = new RelayCommand(() => SystemCommands.ShowSystemMenu(mWindow, GetPosition()));
        }

        /// <summary>
        /// Получение позиции курсора при нажатии на фото
        /// </summary>
        /// <returns></returns>
        private Point GetPosition()
        {
            var position = Mouse.GetPosition(mWindow);
            return new Point(position.X + mWindow.Left, position.Y + mWindow.Top);
        }

        

        #region PublicMember

        public double WindowMinimalWidth { get; set; } = 800;

        public double WindowMinimalHeight { get; set; } = 600;

        public int ResizeBorder { get; set; } = 5;

        public Thickness ResizeBorderThickness { get { return new Thickness(ResizeBorder+OuterMarginSize); } }

        public Thickness InnerConntentPadding { get { return new Thickness(ResizeBorder); } }

        public int OuterMarginSize
        {
            get
            {
                return mWindow.WindowState == WindowState.Maximized ? 4: mOuterMarginSize ;
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

        #region Commands

        public ICommand MinimazeCommand { get; set; }

        public ICommand MaximazeCommand { get; set; }

        public ICommand CloseCommand { get; set; }

        public ICommand MenuCommand { get; set; }
        #endregion
    }
}
