using CefSharp;
using CefSharp.WinForms;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Text.Json;
using System.Security.Policy;

namespace AllScreenReader
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        ChromiumWebBrowser? chrome;
        ProgramSettings? AppSettings;

        private double ScaleSize = 1.31;

        private string SettingPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\AllScreenReader";
        private string SettingFile = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\AllScreenReader\AppSettings.json";

        private void Form1_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.None;

            if (!LoadSettings())
            {
                return;
            }

            if (this.AppSettings == null) // Failed to load settings or load default die.
            {
                MessageBox.Show("Error loading settings, or getting defaults", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }

            //Span across all screens
            Rectangle r = new Rectangle();

            if (this.AppSettings.OverriddenScreenSize == null || this.AppSettings.OverriddenScreenSize.EnableOverrideScreenSize == false)
            {

                foreach (Screen s in Screen.AllScreens)
                {
                    if (s != Screen.FromControl(this)) // Blackout only the secondary screens
                        r = Rectangle.Union(r, s.Bounds);
                }
            }
            else
            {
                //Don't compute screen size if overridden in settings
                r = new Rectangle(0, 0, AppSettings.OverriddenScreenSize.CustomWidth - 1, AppSettings.OverriddenScreenSize.CustomHeight - 1);
            }

            // Resize the main form across all screens
            this.Top = 0; // r.Top; -- This is a fix to prevent one screen that's taller than the placing the top outside of visbility on all the screens.
            this.Left = r.Left;
            this.Width = r.Width;
            this.Height = r.Height + r.Top;

            //Place the single page shift button to the right screen rather than centered to make it more easily clickable
            btnSinglePage.Location = new Point((this.Width / 2) + 13, 0);

            //Override default 1.31 scaling if settings had something else.
            if (this.AppSettings.DefaultCustomScale != null)
            {
                this.ScaleSize = (double)this.AppSettings.DefaultCustomScale;
            }

            // Adjust browser vertical height to allow comic pages side by side. The needed area seems to be 2x(width) x height = ~1.3 so going with 1.31 for rounding buffer
            ResizeViewToScaling(this.ScaleSize);

            AddSiteToMenu();

            string homePage = "";
            
            foreach(ComicSiteToolStripMenuItem curSite in sitesToolStripMenuItem.DropDownItems)
            {
                if (curSite.Text == this.AppSettings.StartingSite)
                {
                    if (curSite.Tag != null)
                    {
                        homePage = (string)curSite.Tag;
                        break;
                    }
                }
            }
            
            CefSettings settings = new CefSettings();
            settings.CachePath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\AllScreenReader\CEF"; // Cache directory to retain cookies
            Cef.Initialize(settings);
            this.chrome = new ChromiumWebBrowser(homePage);
            this.pContainer.Controls.Add(chrome);
            this.chrome.Dock = DockStyle.Fill;
        }

        private void btnMenu_Click(object sender, EventArgs e)
        {
            systemMenu.Show(Cursor.Position.X, Cursor.Position.Y);
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Cef.Shutdown();
        }

        private void tsClose_Click(object sender, EventArgs e)
        {
            systemMenu.Close();
        }

        private void tsExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ComicSite_Click(object? sender, EventArgs e)
        {
            ToolStripMenuItem? clickedSite = sender as ToolStripMenuItem;

            if (clickedSite != null && clickedSite.Tag is string url)
            {
                if (this.chrome != null)
                {
                    this.chrome.LoadUrl(clickedSite.Tag.ToString());
                }
            }
        }

        private void btnSinglePage_Click(object sender, EventArgs e)
        {
            //Kavita and maybe others don't play well with shrinking the browser display, so just move it so the image is on the right display, and move it back without resizing.
            if (this.pContainer.Location.X == 0)
            {
                this.pContainer.Location = new Point(0 - (this.Width / 4), this.pContainer.Location.Y);
            }
            else
            {
                this.pContainer.Location = new Point(0, this.pContainer.Location.Y);
            }
        }

        private void SaveSettings()
        {
            if (this.AppSettings != null)
            {
                if (!Directory.Exists(SettingPath))
                {
                    Directory.CreateDirectory(SettingPath);
                }
                
                string jsonString = JsonSerializer.Serialize(this.AppSettings, new JsonSerializerOptions { WriteIndented = true } );
                File.WriteAllText(this.SettingFile, jsonString);
            }
        }

        private bool LoadSettings()
        {
            if (File.Exists(this.SettingFile))
            {
                try
                {
                    string jsonString = File.ReadAllText(this.SettingFile);
                    this.AppSettings = JsonSerializer.Deserialize<ProgramSettings>(jsonString);
                    return true;
                }
                catch
                {
                    // Failed to load settings rename existing files and regenerate
                    if (File.Exists(this.SettingFile + ".old"))
                    {
                        File.Delete(this.SettingFile + ".old");
                    }

                    File.Move(this.SettingFile, this.SettingFile + ".old");
                    LoadSettings(); //Call again to generate a fresh settings file
                    MessageBox.Show("Error While Loading Settings, existing file renamed to AppSettings.json.old and a new file was generated.\n\nPlease verify and fix the problem.", "Error Loading Settings", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    this.Close();
                    Application.Exit();
                    return false;
                }
            }
            else // We didn't find a settings file. So generate a new settings object
            {
                this.AppSettings = new ProgramSettings();

                this.AppSettings.ComicSites = new List<ComicSite>();
                this.AppSettings.ComicSites.Add(new ComicSite { SiteName = "Comixology", SiteURL = "https://read.amazon.com/kindle-library" } );
                this.AppSettings.ComicSites.Add(new ComicSite { SiteName = "Dark Horse", SiteURL = "https://digital.darkhorse.com/bookshelf" } );

                this.AppSettings.StartingSite = "Comixology";

                this.AppSettings.OverriddenScreenSize = new OverrideScreenSize { EnableOverrideScreenSize = false, CustomWidth = 1920, CustomHeight = 1080 };

                SaveSettings();

                return true;
            }
        }

        private void ResizeViewToScaling(double scalingSize)
        {
            // Adjust browser vertical height to allow comic pages side by side. The needed area seems to be 2x(width) x height = ~1.3 so going with 1.31 for rounding buffer
            if (this.Width / (this.Height + this.Top) < scalingSize)
            {
                int heightResize = (int)Double.Round((this.Height + this.Top) - (this.Width / scalingSize), 0);
                this.pContainer.Location = new Point(this.pContainer.Location.X, heightResize / 2);
                this.pContainer.Height -= heightResize;
            }
        }

        private void AddSiteToMenu()
        {
            if (this.AppSettings == null || this.AppSettings.ComicSites == null)
            {
                return;
            }
            
            foreach(ComicSite curSite in this.AppSettings.ComicSites)
            {
                ComicSiteToolStripMenuItem comicSite = new ComicSiteToolStripMenuItem();
                comicSite.Text = curSite.SiteName;
                comicSite.Tag = curSite.SiteURL;

                if (curSite.CustomSiteScale != null)
                {
                    comicSite.CustomSiteScale = curSite.CustomSiteScale;
                }

                comicSite.Click += ComicSite_Click;

                sitesToolStripMenuItem.DropDownItems.Add(comicSite);
            }
        }
    }
}
