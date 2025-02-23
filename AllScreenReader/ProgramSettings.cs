namespace AllScreenReader
{
    public class ProgramSettings
    {
        public List<ComicSite>? ComicSites { get; set; }
        public string? StartingSite { get; set; }
        public double? DefaultCustomScale { get; set; }
        public OverrideScreenSize? OverriddenScreenSize { get; set; }
    }

    public class ComicSite
    {
        public string? SiteName { get; set; }
        public string? SiteURL { get; set; }
        public double? CustomSiteScale { get; set; }
    }

    public class OverrideScreenSize //Use this for testing/development
    {
        public bool EnableOverrideScreenSize { get; set; }
        public int CustomWidth { get; set; }
        public int CustomHeight { get; set; }
    }
}
