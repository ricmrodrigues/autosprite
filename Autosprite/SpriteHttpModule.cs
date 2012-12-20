using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using SpriteGenerator;

namespace AutoSprite
{
    internal class SpriteHttpModule : IHttpModule
    {
        private const string CssFileName = "autosprite.css";
        private const string PngFileName = "autosprite.png";
        private const string HashFileName = "autosprite.hash";
        private string _cssPath;
        private string _pngPath;
        private string _hashPath;

        public void Dispose()
        {
            var fileList = new List<string>(new[] { _cssPath, _pngPath, _hashPath });
            fileList.ForEach(x =>
                             {
                                 if (File.Exists(x)) File.Delete(x);
                             });
        }

        public void Init(HttpApplication context)
        {
            context.BeginRequest += OnBeginRequest;
            _cssPath = HttpContext.Current.Server.MapPath("~/" + CssFileName);
            _pngPath = HttpContext.Current.Server.MapPath("~/" + PngFileName);
            _hashPath = HttpContext.Current.Server.MapPath("~/" + HashFileName);    
        }

        void OnBeginRequest(object sender, EventArgs e)
        {
            GenerateIfNotExists();
        }

        /// <summary>
        /// Files the name matches the autosprite css/png
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <returns></returns>
        private bool FileNameMatches(out string fileName)
        {
            bool match = false;
            string absolutePath = HttpContext.Current.Request.Url.AbsolutePath;
            fileName = string.Empty;

            if (absolutePath.EndsWith(CssFileName, StringComparison.InvariantCultureIgnoreCase))
            {
                fileName = CssFileName;
                match = true;
            }
            else if (absolutePath.EndsWith(PngFileName, StringComparison.InvariantCultureIgnoreCase))
            {
                fileName = PngFileName;
                match = true;
            }

            return match;
        }

        /// <summary>
        /// Generates css/png file if not exist or sprite sources changed
        /// </summary>
        private void GenerateIfNotExists()
        {
            string fileName;
            if (FileNameMatches(out fileName))
            {
                var response = HttpContext.Current.Response;
                string filePath = HttpContext.Current.Server.MapPath("~/" + fileName);

                string currentHash = SpriteConfig.SpriteSettings.inputFilePaths.GetHashCode().ToString();
                string generatedHashPath = _hashPath;
                string generatedHash = null;
                if (File.Exists(generatedHashPath))
                {
                    generatedHash = File.ReadAllText(generatedHashPath);
                }

                //if there is nothing generated yet, or what is generated shouldn't change
                if (!File.Exists(filePath) || (!string.IsNullOrEmpty(generatedHash) && currentHash != generatedHash))
                {
                    var sprite = new Sprite(SpriteConfig.SpriteSettings);
                    sprite.Create();
                    File.WriteAllText(generatedHashPath, currentHash);
                }
            }            
        }
    }
}
