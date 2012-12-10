using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Github
{
    public class FileSystem : IFileSystem
    {
        private readonly string _owner;
        private readonly string _repository;
        private const string ApiBaseUrl = "https://api.github.com";

        private FileSystem(string owner, string repository)
        {
            _owner = owner;
            _repository = repository;
        }

        public static IFileSystem Open(string owner, string repository)
        {
            return new FileSystem(owner, repository);
        }

        public bool FileExists(string filename)
        {
            var url = string.Format("{0}/repos/{1}/{2}/contents/{3}", ApiBaseUrl, _owner, _repository, filename);
            var httpClient = new HttpClient();

            var result = httpClient.GetAsync(url).Result;
            if (result.StatusCode == HttpStatusCode.OK)
            {
                try
                {
                    var response = JsonConvert.DeserializeObject<Contents>(result.Content.ReadAsStringAsync().Result);
                    return response.Type == "file";
                }
                catch (Exception)
                {
                    return false;
                }
            }

            return false;
        }

        public bool DirectoryExists(string directory)
        {
            var url = string.Format("{0}/repos/{1}/{2}/contents/{3}", ApiBaseUrl, _owner, _repository, directory);
            var httpClient = new HttpClient();

            var result = httpClient.GetAsync(url).Result;
            if (result.StatusCode == HttpStatusCode.OK)
            {
                var value = result.Content.ReadAsStringAsync().Result;
                try
                {
                    var response = JsonConvert.DeserializeObject<IList<Contents>>(value);
                    return response.Any();
                }
                catch (Exception)
                {
                    return false;
                }
            }

            return false;
        }

        public Contents OpenPath(string path)
        {
            var url = string.Format("{0}/repos/{1}/{2}/contents/{3}", ApiBaseUrl, _owner, _repository, path);
            var httpClient = new HttpClient();
            var result = httpClient.GetAsync(url).Result;
            if (result.StatusCode == HttpStatusCode.OK)
            {
                try
                {
                    var response = JsonConvert.DeserializeObject<Contents>(result.Content.ReadAsStringAsync().Result);
                    return response;
                }
                catch (Exception)
                {
                    return null;
                }
            }

            return null;
        }

        public Stat Stat(string filename)
        {
            throw new NotImplementedException();
        }
    }
}
