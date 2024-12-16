using Microsoft.Extensions.Options;
using Passports.Exceptions;
using Passports.Options;
using System.IO.Compression;
using YandexDisk.Client.Clients;
using YandexDisk.Client.Http;
using YandexDisk.Client.Protocol;

namespace Passports.Converter
{
    /// <summary>
    /// Represents Yandex Disk service.
    /// </summary>
    public class YandexDiskService
    {
        private Resource? _data1Folder;
        private DiskHttpApi? _api;
        private AppSettings? _appSettings;

        /// <summary>
        /// YandexDiskService constructor.
        /// </summary>
        /// <param name="options">AppSettings section values.</param>
        public YandexDiskService(IOptions<AppSettings> options)
        {
            try
            {
                _appSettings = options.Value;
                string? token = _appSettings.YandexDiskToken;
                if (string.IsNullOrWhiteSpace(token))
                {
                    throw new EmptyConfigurationSectionException(_appSettings.YandexDiskToken.GetType().Name);
                }
                _api = new DiskHttpApi(token);
                _data1Folder = _api.MetaInfo.GetInfoAsync(new ResourceRequest
                {
                    Path = "/data1/"
                }).Result;
            }
            catch (EmptyConfigurationSectionException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        /// <summary>
        /// Downloads a file from Yandex Disk.
        /// </summary>
        /// <param name="destinationDirectory">The path to the downloaded file directory.</param>
        /// <returns></returns>
        public async Task DownloadFileAsync(string destinationDirectory)
        {
            string zipDirectory = Path.Combine(destinationDirectory, "zip");

            if (Directory.Exists(destinationDirectory))
            {
                Directory.Delete(destinationDirectory, true);
            }

            Directory.CreateDirectory(zipDirectory);

            try
            {
                if (_data1Folder == null || _api == null)
                {
                    throw new YandexDiskException();
                }

                Resource data1 = _data1Folder.Embedded.Items[0];
                string zipFile = Path.Combine(zipDirectory, data1.Name);
                await _api.Files.DownloadFileAsync(data1.Path, zipFile);

                ZipFile.ExtractToDirectory(zipFile, destinationDirectory);
            }
            catch (YandexDiskException)
            {
                Console.WriteLine("Cannot get file from Yandex Disk");
            }
        }
    }
}
