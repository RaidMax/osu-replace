## osu!replace

### Introduction

*osu!replace* is a Windows application for osu! designed to replace all beatmap images with a solid color or a single image. This application provides a more uniform experience for players who are not fans of anime or potentially explicit beatmap backgrounds.

### Requirements

- [.NET 8 Desktop Runtime](https://dotnet.microsoft.com/en-us/download/dotnet/thank-you/runtime-desktop-8.0.7-windows-x64-installer)

### Download

You can download the latest compiled version of osu!replace from the releases tab.

### Usage

1. **Run osu!replace.exe as administrator.**
2. Click "**osu! folder...**" and navigate to your osu! directory.
3. Choose your replacement:
    - Click "**color...**" to select a solid color.
    - Click "**image...**" to select a replacement background image.
4. Click "**apply**" to replace the beatmap images. This process may take a while depending on your library size.
5. **Optional:** Check "**restore**" to revert to the original beatmap images.

### FAQ

- **Will osu!replace delete my beatmap images?**

  No, existing beatmap images are renamed, not deleted.

- **Will osu!replace use up disk space?**

  Only enough to save the new image once. Existing beatmap images are turned into symbolic links, which don't inherently use additional disk space.

- **Can I restore my old beatmap images?**

  Yes, check the "restore" option as described in the usage instructions.

- **Does osu!replace replace storyboards?**

  Not at this time. 
