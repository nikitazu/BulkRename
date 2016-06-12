using System;
using System.IO;
using System.Net;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BulkRename.Components.Net
{
    public class UpdatesComponent
    {
        private const string _checkUrl = "https://github.com/nikitazu/BulkRename/releases";
        private readonly Regex _versionLineRegex = new Regex(@"/nikitazu/BulkRename/tree/(v[\w|\.|\-]+)""", RegexOptions.Compiled);
        private readonly Regex _versionValueRegex = new Regex(@"v(\d{1,3})\.(\d{1,4})(\-\w+)?", RegexOptions.Compiled);

        public bool ContainsVersionData(string input)
        {
            return _versionLineRegex.IsMatch(input);
        }

        public string ParseVersion(string input)
        {
            var match = _versionLineRegex.Match(input);
            return match.Success ? match.Groups[1].Value : null;
        }

        public async Task<string> CheckForUpdates(Action<string> onResponseLine)
        {
            var request = WebRequest.Create(_checkUrl);
            request.Method = "GET";
            using (var response = await request.GetResponseAsync())
            using (var stream = response.GetResponseStream())
            using (var reader = new StreamReader(stream))
            {
                while (!reader.EndOfStream)
                {
                    onResponseLine(reader.ReadLine());
                }
            }
            return string.Empty;
        }

        public long VersionToNumber(string version)
        {
            var match = _versionValueRegex.Match(version);
            if (match.Success)
            {
                long major, minor;
                long.TryParse(match.Groups[1].Value, out major);
                long.TryParse(match.Groups[2].Value, out minor);
                return major * 10000 + minor;
            }
            else
            {
                return -1L;
            }
        }

        public string MakeGetVersionUrl(string version) =>
            $"https://github.com/nikitazu/BulkRename/tree/{version}";

        public string CurrentVersion =>
            "v" + Assembly.GetEntryAssembly().GetName().Version.ToString();
    }
}
