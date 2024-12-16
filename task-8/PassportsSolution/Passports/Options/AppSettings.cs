namespace Passports.Options
{
    public class AppSettings
    {
        public string ReadingCsvTime { get; set; } = string.Empty;
        public string GmtOffset { get; set; } = string.Empty;
        public string BatchSize { get; set; } = string.Empty;
        public string YandexDiskToken { get; set; } = string.Empty;
    }
}
