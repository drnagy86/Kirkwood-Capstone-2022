using DataObjects;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace WPFPresentation
{
    internal class ImageHelperDevelopment : IImageHelper
    {
        /// <summary>
        /// Derrick Nagy
        /// Created: 2022/03/03
        /// 
        /// Description:
        /// Saves an image and returns a new image name. Takes the image file and makes a copy at the correct address.
        /// 
        /// Update:
        /// Derrick Nagy
        /// Updated: 2022/03/06
        /// 
        /// Description:
        /// Added check to see if the path exists, and if not, create it
        /// 
        /// 
        /// </summary>
        /// <param name="fileName">The name of the file and extension</param>
        /// <param name="sourceFile">The full path of the file to be saved</param>
        /// <returns>A new image name as a string</returns>
        public string SaveImageReturnsNewImageName(string fileName, string sourceFile)
        {
            // sources
            // https://wpf-tutorial.com/dialogs/the-openfiledialog/
            // https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/file-system/how-to-copy-delete-and-move-files-and-folders
            // https://stackoverflow.com/questions/9065598/if-a-folder-does-not-exist-create-it

            string newFileName = createNameForImage(fileName);
            string targetFile = pathToSaveImage() + "\\" + newFileName;

            try
            {
                bool exists = System.IO.Directory.Exists(pathToSaveImage());

                if (!exists)
                {
                    System.IO.Directory.CreateDirectory(pathToSaveImage());
                }

                System.IO.File.Copy(sourceFile, targetFile);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return newFileName;
        }

        /// <summary>
        /// Derrick Nagy
        /// Created: 2022/03/06
        /// 
        /// Description:
        /// Given the image name, this finds the file path and returns the image.
        /// If none found, returns an empty BitmapImage object
        /// 
        /// </summary>
        /// <param name="imageName">The name of the file</param>
        /// <returns>An image</returns>
        public BitmapImage ReturnBitMapImage(string imageName)
        {
            BitmapImage image = null;
            Uri source = findImageSource(imageName);

            if (source == null)
            {
                image = new BitmapImage();
            }
            else
            {
                try
                {
                    image = new BitmapImage(source);
                }
                catch (Exception)
                {

                }
            }

            return image;
        }

        /// <summary>
        /// Derrick Nagy
        /// Created: 2022/03/03
        /// 
        /// Description:
        /// Path to save the image
        /// 
        /// Update:
        /// Derrick Nagy
        /// Updated: 2022/03/06
        /// 
        /// Description:
        /// Changed to private
        /// </summary>
        /// <returns></returns>
        private string pathToSaveImage()
        {
            return AppData.DataPath + @"\";
        }

        /// <summary>
        /// Derrick Nagy
        /// Created: 2022/03/03
        /// 
        /// Description:
        /// Creates a unique name for the image based on the time, a random 4 digit number, and the image name.
        /// Strips whitespace and special characters
        /// 
        /// Update:
        /// Derrick Nagy
        /// Updated: 2022/03/06
        /// 
        /// Description:
        /// Changed to private
        /// 
        /// </summary>
        /// <param name="imageName">The original name of the image with file extension</param>
        /// <returns></returns>
        private string createNameForImage(string imageName)
        {
            string nameToReturn = "";
            Random random = new Random();

            string dateString = Regex.Replace(DateTime.Now.ToString(), "[^a-zA-Z0-9_.]+", "");
            string strippedImageName = Regex.Replace(imageName, "[^a-zA-Z0-9_.]+", "");

            nameToReturn = dateString + "-" + random.Next(0, 9999) + "-" + strippedImageName;

            return nameToReturn;
        }

        /// <summary>
        /// Derrick Nagy
        /// Created: 2022/03/03
        /// 
        /// Description:
        /// Path to the image
        /// 
        /// Update:
        /// Derrick Nagy
        /// Updated: 2022/03/06
        /// 
        /// Description:
        /// Added try catch for null source. Change to private.
        /// 
        /// </summary>
        /// <param name="imageName">Image Name stored in the database</param>
        /// <returns></returns>
        private Uri findImageSource(string imageName)
        {
            Uri source = null;

            try
            {
                source = new Uri(pathToSaveImage() + imageName, UriKind.Absolute);
            }
            catch (Exception)
            {
                
            }

            return source;
        }

        /// <summary>
        /// Derrick Nagy
        /// Created: 2022/03/08
        /// 
        /// Description:
        /// Deletes an image from the folder that it is currently in
        /// Work in progress. It needs to make sure that the image is not being using first.
        /// 
        /// </summary>
        /// <param name="imageName">The name of the image</param>
        /// <returns>If deleting the image was successfull</returns>
        public bool DeleteImage(string imageName)
        {
            bool result = false;

            try
            {
                File.Delete(pathToSaveImage() + imageName);
                result = true;
            }
            catch (Exception ex)
            {

                throw ex;
            }


            return result;


        }
    }
}
