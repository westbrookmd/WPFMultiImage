﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel;
using System.IO;
using System.Reflection;
using System.Resources;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace WPFMultiImage
{
    public class ImagesClass : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        ResourceManager rm = new ResourceManager("WPFMultiImage.Properties.Resources", Assembly.GetExecutingAssembly());

        private FileInfo _fileinfo;
        public FileInfo FileInfo
        {
            get
            {
                return _fileinfo;
            }
            set
            {
                if (value != _fileinfo)
                {
                    _fileinfo = value;
                    NotifyPropertyChanged();

                }
            }
        }

        private string _filetype;
        public string FileType
        {
            get
            {
                return _filetype;
            }
            set
            {
                if (value != _filetype)
                {
                    _filetype = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private int _savefilterindex;
        public int SaveFilterIndex
        {
            get
            {
                return _savefilterindex;
            }
            set
            {
                if (value != _savefilterindex)
                {
                    _savefilterindex = value;
                    NotifyPropertyChanged();
                }
            }
        }
        private BitmapImage _bitmapimage;
        public BitmapImage BitmapImage
        {
            get
            {
                return _bitmapimage;
            }
            set
            {
                if (value != _bitmapimage)
                {
                    _bitmapimage = value;
                    NotifyPropertyChanged();
                }
            }
        }

        //Every image does not support a palette or metadata
        //So these have to be nullable

        private BitmapPalette? _bitmappalette;
        public BitmapPalette? BitmapPalette
        {
            get
            {
                return _bitmappalette;
            }
            set
            {
                if (value != _bitmappalette)
                {
                    _bitmappalette = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private BitmapMetadata? _bitmapmetadata;
        public BitmapMetadata? BitmapMetadata
        {
            get
            {
                return _bitmapmetadata;
            }
            set
            {
                if (value != _bitmapmetadata)
                {
                    _bitmapmetadata = value;
                    NotifyPropertyChanged();
                }
            }
        }

        //==================================================================================================================================

        public string PixelSize()
        {
            return this.BitmapImage.PixelWidth.ToString() + " " + rm.GetString("x") + " " + this.BitmapImage.PixelHeight.ToString();
        }

        public string InchSize()
        {
            return (this.BitmapImage.Width / 96).ToString() + " " + rm.GetString("x") + " " + (this.BitmapImage.Height / 96).ToString();
        }

        public string DPISize()
        {
            return this.BitmapImage.DpiX.ToString() + " " + rm.GetString("x") + " " + this.BitmapImage.DpiY.ToString();
        }

        public int BPP()
        {
            return this.BitmapImage.Format.BitsPerPixel;
        }

        public string Masks()
        {
            // from Microsoft PixelFormat Struct Reference

            String stringOfValues = "";

            IList<PixelFormatChannelMask> myChannelMaskCollection = this.BitmapImage.Format.Masks;

            foreach (PixelFormatChannelMask myMask in myChannelMaskCollection)
            {
                IList<byte> myBytesCollection = myMask.Mask;
                foreach (byte myByte in myBytesCollection)
                {
                    stringOfValues = stringOfValues + myByte.ToString() + ",";
                }
            }

            return stringOfValues.Substring(0, stringOfValues.Length - 1); //nuke last comma
        }

        public string ApplicationName()
        {
            try
            {
                return this.BitmapMetadata.ApplicationName;
            }
            catch (Exception ex)
            {
                if (ex is NotSupportedException)
                    return rm.GetString("NotSupported");
                else
                    throw;
            }
        }

        public string Author()
        {
            try
            {
                if (BitmapMetadata.Author is null)
                    return "";
                else
                {
                    string a = "";

                    foreach (string s in BitmapMetadata.Author)
                        a = a + s + ",";

                    return a.Substring(0, a.Length - 1); //nuke last comma
                }
            }
            catch (Exception ex)
            {
                if (ex is NotSupportedException)
                    return rm.GetString("NotSupported");
                else
                    throw;
            }
        }

        public string CameraManufacturer()
        {
            try
            {
                return this.BitmapMetadata.CameraManufacturer;
            }
            catch (Exception ex)
            {
                if (ex is NotSupportedException)
                    return rm.GetString("NotSupported");
                else
                    throw;
            }
        }

        public string CameraModel()
        {
            try
            {
                return this.BitmapMetadata.CameraModel;
            }
            catch (Exception ex)
            {
                if (ex is NotSupportedException)
                    return rm.GetString("NotSupported");
                else
                    throw;
            }
        }

        public string Comment()
        {
            try
            {
                return this.BitmapMetadata.Comment;
            }
            catch (Exception ex)
            {
                if (ex is NotSupportedException)
                    return rm.GetString("NotSupported");
                else
                    throw;
            }
        }

        public string Copyright()
        {
            try
            {
                return this.BitmapMetadata.Copyright;
            }
            catch (Exception ex)
            {
                if (ex is NotSupportedException)
                    return rm.GetString("NotSupported");
                else
                    throw;
            }
        }

        public string DateTaken()
        {
            try
            {
                return this.BitmapMetadata.DateTaken;
            }
            catch (Exception ex)
            {
                if (ex is NotSupportedException)
                    return rm.GetString("NotSupported");
                else
                    throw;
            }
        }

        public string Format()
        {
            try
            {
                return this.BitmapMetadata.Format;
            }
            catch (Exception ex)
            {
                if (ex is NotSupportedException)
                    return rm.GetString("NotSupported");
                else
                    throw;
            }
        }

        public string Keywords()
        {
            try
            {
                if (BitmapMetadata.Keywords is null)
                    return "";
                else
                {
                    string k = "";
                    foreach (string s in BitmapMetadata.Keywords)
                        k = k + s + ",";

                    return k.Substring(0, k.Length - 1); //nuke last comma
                }
            }
            catch (Exception ex)
            {
                if (ex is NotSupportedException)
                    return rm.GetString("NotSupported");
                else
                    throw;
            }
        }

        public string Location()
        {
            try
            {
                return this.BitmapMetadata.Location;
            }
            catch (Exception ex)
            {
                if (ex is NotSupportedException)
                    return rm.GetString("NotSupported");
                else
                    throw;
            }
        }

        public int Rating()
        {
            try
            {
                return this.BitmapMetadata.Rating;
            }
            catch (Exception ex)
            {
                if (ex is NotSupportedException)
                    return -1; //"<not supported>"
                else
                    throw;
            }
        }

        public string Subject()
        {
            try
            {
                return this.BitmapMetadata.Subject;
            }
            catch (Exception ex)
            {
                if (ex is NotSupportedException)
                    return rm.GetString("NotSupported");
                else
                    throw;
            }
        }

        public string metadataTitle()
        {
            try
            {
                return this.BitmapMetadata.Title;
            }
            catch (Exception ex)
            {
                if (ex is NotSupportedException)
                    return rm.GetString("NotSupported");
                else
                    throw;
            }
        }

        //===================================================================================================

        public ImagesClass LoadImage(string file)
        {
            FileInfo = GetFileInfo(file);
            FileType = GetFileType(this.FileInfo.Extension);
            SaveFilterIndex = GetSaveFilterIndex(this.FileInfo.Extension);
            BitmapImage = GetBitmapImage(this.FileInfo);
            BitmapPalette = GetBitmapPalette(this.BitmapImage, this.FileType);
            BitmapMetadata = GetBitmapMetadata(this.BitmapImage, this.FileType);

            return this;

            FileInfo GetFileInfo(string fn)
            {
                FileInfo fi = new FileInfo(fn); return fi;
            }//GetFileInfo

            string GetFileType(string ext)
            {
                string ft = "";

                switch (ext)
                {
                    case ".bmp":
                        ft = "BMP"; break;
                    case ".gif":
                        ft = "GIF"; break;
                    case ".ico":
                        ft = "ICO"; break;
                    case ".jpg":
                    case ".jpeg":
                        ft = "JPG"; break;
                    case ".png":
                        ft = "PNG"; break;
                    case ".tif":
                    case ".tiff":
                        ft = "GIF"; break;
                    default:
                        ft = ""; //This is a programmer error!
                        break;
                }

                return ft;
            }//GetFileType

            int GetSaveFilterIndex(string ext)
            {
                //.ico has no encoder?

                int sfi = 1;

                switch (ext)
                {
                    case ".bmp":
                        sfi = 1; break;
                    case ".gif":
                        sfi = 2; break;
                    case ".jpg":
                    case ".jpeg":
                        sfi = 3; break;
                    case ".png":
                        sfi = 4; break;
                    case ".tif":
                    case ".tiff":
                        sfi = 5; break;
                    default:
                        sfi = -1; //This is a programmer error!
                        break;
                }

                return sfi;
            }//GetSaveFilterType

            BitmapImage GetBitmapImage(FileInfo fi)
            {
                BitmapImage bi = new BitmapImage(new Uri(fi.FullName));

                return bi;
            }

            BitmapPalette? GetBitmapPalette(BitmapImage bi, string ft)
            {
                BitmapPalette? bp = null; //Not every image has a palette

                if ((ft == "GIF") || (ft == "TIF"))
                {
                    bp = new BitmapPalette(bi, 256); //What should second arg be??
                }

                return bp;
            }

            BitmapMetadata? GetBitmapMetadata(BitmapImage bi, string ft)
            {
                BitmapMetadata? bmd = null;

                List<string> HasMetaData = new List<String>(new string[] { "GIF", "JPG", "PNG", "TIF" });
                if (HasMetaData.Contains(ft))
                {
                    try
                    {
                        bmd = new(FileType);
                    }
                    catch //do nothing if codec does not support metadata properly
                    { }
                }

                return bmd;

            }//GetBitmapMetadata
        }//LoadImage

        public List<ColorInfo> ColorList; //= new List<ColorInfo>();

        public void GetColorList()
        {
            ColorList = new List<ColorInfo>();

            if (!(BitmapPalette is null))
            {
                for (int i = 0; i < BitmapPalette.Colors.Count; i++)
                {
                    ColorInfo ci = new ColorInfo(BitmapPalette.Colors[i]);
                    ColorList.Add(ci);
                }
            }
        }

        public void SaveImage(ImagesClass img)
        {
            if (FileDialogImage.SaveFileDialogImage(img) == true)
            {
                switch (FileDialogImage.ChosenExt)
                {
                    case "BMP":
                        {
                            SaveBitmaps.BMPSave(FileDialogImage.ChosenFile, img);
                            break;
                        }
                    case "GIF":
                        {
                            SaveBitmaps.GIFSave(FileDialogImage.ChosenFile, img);
                            break;
                        }
                    case "JPG":
                        {
                            SaveBitmaps.JPGSave(FileDialogImage.ChosenFile, img);
                            break;
                        }
                    case "PNG":
                        {
                            SaveBitmaps.PNGSave(FileDialogImage.ChosenFile, img);
                            break;
                        }
                    case "TIF":
                        {
                            SaveBitmaps.TIFSave(FileDialogImage.ChosenFile, img);
                            break;
                        }
                    default:
                        break;
                }//switch
            }//if savedialog is OK
        }//SaveImage

        public void SaveMultiImage(Dictionary<string, ImagesClass> dict)
        {
            if (FileDialogImage.SaveFileDialogMulti() == true)
            {
                SaveBitmaps.MultiTIFSave(FileDialogImage.ChosenFile, dict);
            }

            }//SaveMultiImage

    }//ImagesClass
    //== For palette list box
    public class ColorInfo
    {
        public Color Color { get; set; }

        public SolidColorBrush SampleBrush
        {
            get { return new SolidColorBrush(Color); }
        }

        public string HexValue
        {
            get { return Color.ToString(); }
        }

        public ColorInfo(Color color)
        {
            Color = color;
        }

    }//ColorInfo

}//namespace
