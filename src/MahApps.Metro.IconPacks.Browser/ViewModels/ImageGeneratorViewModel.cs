using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace MahApps.Metro.IconPacks.Browser.ViewModels
{
    public class ImageGeneratorViewModel : ViewModelBase
    {
        private readonly BusyStack _busyStack;
        private readonly MainViewModel _mainViewModel;
        private readonly IPackIconDataFactory _pathDatafactory;
        private readonly IDictionary<ImageType, Func<BitmapEncoder>> _encoderLookup = new Dictionary<ImageType, Func<BitmapEncoder>>()
        {
            [ImageType.PNG] = new Func<BitmapEncoder>(() => new PngBitmapEncoder()),
            [ImageType.JPEG] = new Func<BitmapEncoder>(() => new JpegBitmapEncoder()),
            [ImageType.GIF] = new Func<BitmapEncoder>(() => new GifBitmapEncoder()),
            [ImageType.BMP] = new Func<BitmapEncoder>(() => new PngBitmapEncoder()),
            [ImageType.TIFF] = new Func<BitmapEncoder>(() => new TiffBitmapEncoder()),
        };

        private readonly IDictionary<ImageType, string> _extensionLookup = new Dictionary<ImageType, string>()
        {
            [ImageType.PNG] = ".png",
            [ImageType.JPEG] = ".jpeg",
            [ImageType.GIF] = ".gif",
            [ImageType.BMP] = ".bmp",
            [ImageType.TIFF] = ".tiff",
        };

        private ICommand _generateImageCommand;
        private ICommand _openFolderCommand;
        private ImageTypeViewModel _selectedType;
        private string _targetImageDirectory;
        private int _imageSize;
        private int _canvarSize;
        private bool _isBusy;

        public ICommand GenerateImageCommand
        {
            get { return this._generateImageCommand; }
            set
            {
                if (Equals(value, this._generateImageCommand)) return;
                this._generateImageCommand = value;
                this.OnPropertyChanged();
            }
        }

        public ICommand OpenFolderCommand
        {
            get { return this._openFolderCommand; }
            set
            {
                if (Equals(value, this._openFolderCommand)) return;
                this._openFolderCommand = value;
                this.OnPropertyChanged();
            }
        }

        public string TargetImageDirectory
        {
            get { return this._targetImageDirectory; }
            set
            {
                if (value == this._targetImageDirectory) return;
                this._targetImageDirectory = value;
                this.OnPropertyChanged();
            }
        }

        public ImageTypeViewModel SelectedType
        {
            get { return this._selectedType; }
            set
            {
                if (value == this._selectedType) return;
                this._selectedType = value;
                this.OnPropertyChanged();
            }
        }

        public int ImageSize
        {
            get { return this._imageSize; }
            set
            {
                if (value == this._imageSize) return;
                this._imageSize = value;
                this.OnPropertyChanged();
            }
        }

        public int CanvasSize
        {
            get { return this._canvarSize; }
            set
            {
                if (value == this._canvarSize) return;
                this._canvarSize = value;
                this.OnPropertyChanged();
            }
        }

        public bool IsBusy
        {
            get { return this._isBusy; }
            set
            {
                if (value == this._isBusy) return;
                this._isBusy = value;
                this.OnPropertyChanged();
            }
        }

        public ObservableCollection<ImageTypeViewModel> ImageTypes { get; }

        public ImageGeneratorViewModel(MainViewModel mainViewModel)
        {
            _mainViewModel = mainViewModel;
            _pathDatafactory = new PackIconDataFactory();
            _busyStack = new BusyStack();
            _busyStack.OnChanged += (hasItems) => IsBusy = hasItems;

            this.GenerateImageCommand = new RelayCommand<object>(CreateImageFromIcon, CanCreateImageFromIcon);
            this.OpenFolderCommand = new RelayCommand(() => OpenFolder(TargetImageDirectory));
            this.TargetImageDirectory = Path.Combine(Path.GetFullPath("."), "Images");
            this.ImageTypes = new ObservableCollection<ImageTypeViewModel>(GetIcons());
            this.SelectedType = this.ImageTypes[0];
            this.ImageSize = 128;
            this.CanvasSize = 256;
        }

        private IEnumerable<ImageTypeViewModel> GetIcons()
        {
            return new ObservableCollection<ImageTypeViewModel>(
                Enum.GetValues(typeof(ImageType))
                    .OfType<ImageType>()
                    .Select(p => GetImageTypeViewModel(p))
                    .OrderBy(m => m.DisplayName, StringComparer.InvariantCultureIgnoreCase));
        }

        private ImageTypeViewModel GetImageTypeViewModel(ImageType type)
        {
            return new ImageTypeViewModel()
            {
                DisplayName = type.ToString(),
                EncoderFactory = _encoderLookup[type],
                Type = type
            };
        }

        private void CreateImageFromIcon(object icon)
        {
            if (icon is IIconViewModel viewModel)
            {
                using (_busyStack.GetToken())
                    CreateImageInternal(viewModel, _mainViewModel.SelectedBrush);
            }
        }

        private void CreateImageInternal(IIconViewModel icon, Brush brush)
        {
            var pathData = string.Empty;
            if (!_pathDatafactory.GetData(icon.IconType, icon.Name, out pathData))
                return;

            var geometry = Geometry.Parse(pathData);
            var filePath = Path.Combine(TargetImageDirectory, icon.Name + _extensionLookup[SelectedType.Type]);

            Directory.CreateDirectory(TargetImageDirectory);

            var canvas = new Canvas
            {
                Width = ImageSize,
                Height = ImageSize,
                Background = new SolidColorBrush(Colors.Transparent)
            };

            var path = new System.Windows.Shapes.Path()
            {
                Data = geometry,
                Stretch = Stretch.Uniform,
                Fill = brush,
                Width = ImageSize,
                Height = ImageSize,
            };

            canvas.Children.Add(path);

            var size = new Size(ImageSize, ImageSize);
            canvas.Measure(size);
            canvas.Arrange(new Rect(size));

            var rtb = new RenderTargetBitmap((int)size.Width, (int)size.Height, 96, 96, PixelFormats.Pbgra32);
            rtb.Render(canvas);

            var encoder = SelectedType.EncoderFactory();
            encoder.Frames.Add(BitmapFrame.Create(rtb));

            using (var stream = new FileStream(filePath, FileMode.Create))
                encoder.Save(stream);
        }

        private bool CanCreateImageFromIcon(object icon)
        {
            return icon is IIconViewModel
                && !IsBusy
                && SelectedType != null
                && ImageSize > 15
                && ImageSize <= CanvasSize
                && ImageSize <= 256;
        }

        private void OpenFolder(string path)
        {
            Directory.CreateDirectory(path);
            var args = string.Format("/e, /select, \"{0}\"", path);
            var info = new ProcessStartInfo()
            {
                FileName = "explorer",
                Arguments = args
            };

            using (var process = Process.Start(info))
            {

            }
        }
    }
}
