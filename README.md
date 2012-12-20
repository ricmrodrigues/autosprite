Autosprite
==========

CSS Sprite generator for ASP.NET websites bundle style

This project uses an existing one, that I converted into a Class Library:
http://spritegenerator.codeplex.com/

The CSS/Sprite generation is fully done by this project, I just wired it up into the ASP.NET application lifecycle

The way you can use this utility to select which files you want to create the sprite for, is:
AutoSprite.SpriteConfig.AddImage("~/Content/*.jpg");
 
The generated files are autosprite.css and autosprite.png

Sample CSS:
.sprite { background-image: url('autosprite.png'); background-color: transparent; background-repeat: no-repeat; }
.google-windows-8 { width: 620px; height: 332px; background-position: 0px -398px; }
.download { width: 276px; height: 183px; background-position: 0px -159px; }
.vista { width: 211px; height: 239px; background-position: -276px -159px; }
.image { width: 316px; height: 159px; background-position: 0px 0px; }

sprite class is the "base" class.
the rest are one class per image file included in the sprite, what you use is both
<div class="sprite download"></div> 
for instance.

You can see real usage on the TestApplication project



