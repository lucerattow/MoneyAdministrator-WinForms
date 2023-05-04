using Svg;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MoneyAdministrator.CustomControls.Files
{
    internal static class Images
    {
        public static Image empty
        {
            get
            {
                var bmp = new Bitmap(1, 1);
                using (Graphics g = Graphics.FromImage(bmp))
                {
                    g.Clear(Color.Transparent);
                }
                return bmp;
            }
        }

        public static Image expand_collapse_gray
        {
            get
            {
                //Cargo el archivo
                Assembly assembly = Assembly.GetExecutingAssembly();
                string resourceName = "MoneyAdministrator.CustomControls.Files.Images.expand_collapse_gray.png";
                Stream stream = assembly.GetManifestResourceStream(resourceName);

                return Image.FromStream(stream);
            }
        }

        public static Image expand_expanded_gray
        {
            get
            {
                //Cargo el archivo
                Assembly assembly = Assembly.GetExecutingAssembly();
                string resourceName = "MoneyAdministrator.CustomControls.Files.Images.expand_expanded_gray.png";
                Stream stream = assembly.GetManifestResourceStream(resourceName);

                return Image.FromStream(stream);
            }
        }

        public static Image check_box
        {
            get
            {
                //Cargo el archivo
                Assembly assembly = Assembly.GetExecutingAssembly();
                string resourceName = "MoneyAdministrator.CustomControls.Files.Images.check_box.png";
                Stream stream = assembly.GetManifestResourceStream(resourceName);

                return Image.FromStream(stream);
            }
        }

        public static Image check_box_checked
        {
            get
            {
                //Cargo el archivo
                Assembly assembly = Assembly.GetExecutingAssembly();
                string resourceName = "MoneyAdministrator.CustomControls.Files.Images.check_box_checked.png";
                Stream stream = assembly.GetManifestResourceStream(resourceName);

                return Image.FromStream(stream);
            }
        }

        public static Image check_box_checked_green
        {
            get
            {
                //Cargo el archivo
                Assembly assembly = Assembly.GetExecutingAssembly();
                string resourceName = "MoneyAdministrator.CustomControls.Files.Images.check_box_checked_green.png";
                Stream stream = assembly.GetManifestResourceStream(resourceName);

                return Image.FromStream(stream);
            }
        }
    }
}
