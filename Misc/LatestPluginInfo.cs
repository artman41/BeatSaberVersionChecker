using BeatSaberVersionChecker.Interfaces;
using BeatSaberVersionChecker.Misc.Github;

namespace BeatSaberVersionChecker.Misc {
    public struct LatestPluginInfo {
            public IVerCheckPlugin Plugin { get; set; }
            public bool IsLatestVersion { get; set; }

            public LatestPluginInfo(IVerCheckPlugin plugin, VCInterop interop) {
                Plugin = plugin;
                IsLatestVersion = false;
                interop.StartCoroutine(Util.GetGithubJson(interop, plugin, SetLatestVersion));
            }

        private void SetLatestVersion(GithubReleasePage o) {
            if (o != null) {
                var x = o.tag_name.StartsWith("v") ? o.tag_name.Substring(1) : o.tag_name;
                IsLatestVersion = x == Plugin.Version;
            }
            else {
                IsLatestVersion = true;
            }
        }
    }
}