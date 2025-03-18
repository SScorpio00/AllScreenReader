# All Screen Reader

[Gallery showcasing Comixology](https://imgur.com/a/2T9nQPM)

This program creates a window that spans all screens of a computer, and creates a Chromium web browser across them.

My personal use case is that I bought an *inexpensive* dual screen laptop off AliExpress to use to read digital comics and simulate a book with two pages side by side on each screen. But I ran into issues. The Intel graphics driver sees one of the screens as the internal laptop monitor, and the other as and external monitor. Unfortunately while the Intel driver lets you combine mutiple monitors to be "seen" as a single screen. This only works on external display. So I can't combine both monitors and simply maximize a full screen web browser across both screens.

So this program was born. It automatically calculates the total screen size and creates a borderless window that fills them in. It uses the Cef.NET embedded Chromium web browser and allows customizing of the site list via editing a settings JSON file. The embedded browser uses cookies that are separate from your other installed browsers; however, they are saved so you don't need to login to sites each time if you do save your settings.

The settings file is: %LOCALAPPDATA%\AllScreenReader\AppSettings.json
Cookies are stored in: %LOCALAPPDATA%\AllScreenReader\CEF

%LOCALAPPDATA%\AllScreenReader is the only place that is read/written to, simply delete it to clear out the program.

A default settings file will be created during the first run. Add and edit your down entries to ComicSites to add your personal comic repo, or any sites that also work. So far Comixology and Dark Horses' digital site are the only legal sites I've found that do the two page side by side view, if you know of others, please let me know.

The only confusing setting is the Custom Scale option. On my laptop with both screen set to Portrait, they end up a little taller than wide. So I was only seeing a single page. I found that regular comics are about a 1.30 scale meaning the height is about 30% more than the legth of the width. So I have it set to resize the internal browser window to be 1.31 which allows two pages to display. Other sites could be differ, so this can be set per site. But it defaults to that 1.31 if you don't override it in settings.
