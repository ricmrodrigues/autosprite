using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using SpriteGenerator;

namespace AutoSprite
{
    public static class SpriteConfig
    {
        internal static LayoutProperties SpriteSettings = null;

        public static void AddImage(string path)
        {
            if (string.IsNullOrEmpty(path)) throw new ArgumentNullException("path");

            string directory = Path.GetDirectoryName(path);
            string file = Path.GetFileName(path);
            string absoluteDir = HttpContext.Current.Server.MapPath(directory);
            string[] filePaths = Directory.GetFiles(absoluteDir, file);

            if (SpriteSettings == null)
            {
                SpriteSettings = new LayoutProperties
                {
                    distanceBetweenImages = 0,
                    inputFilePaths = filePaths,
                    outputCssFilePath = HttpContext.Current.Server.MapPath("~/autosprite.css"),
                    outputSpriteFilePath = HttpContext.Current.Server.MapPath("~/autosprite.png"),
                    layout = "Automatic"
                };                
            }
            else
            {
                List<string> inputFilePaths = SpriteSettings.inputFilePaths.ToList();
                inputFilePaths.AddRange(filePaths);
                SpriteSettings.inputFilePaths = inputFilePaths.ToArray();
            }
        }
    }
}
