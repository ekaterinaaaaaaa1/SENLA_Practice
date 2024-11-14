using Passports.Exceptions;
using System.IO.Compression;
using YandexDisk.Client.Clients;
using YandexDisk.Client.Http;
using YandexDisk.Client.Protocol;

namespace Passports.Converter
{
    public class YandexDiskService
    {
        private Resource? _data1Folder;
        private DiskHttpApi? _api;
        private readonly string _sectionName = "YandexDiskToken";

        public YandexDiskService(IConfiguration configuration)
        {
            try
            {
                string? token = configuration.GetSection(_sectionName).Value;
                if (string.IsNullOrWhiteSpace(token))
                {
                    throw new EmptyConfigurationSectionException(_sectionName);
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
                Console.WriteLine("An exception with Yandex Disk");
            }
        }
    }
}
