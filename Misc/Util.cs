using System;
using System.Collections;
using System.Linq;
using System.Net;
using BeatSaberVersionChecker.Interfaces;
using BeatSaberVersionChecker.Misc.Github;
using UnityEngine;
using UnityEngine.Networking;
using Logger = BeatSaberLogger.Logger;
using Object = UnityEngine.Object;

namespace BeatSaberVersionChecker.Misc {
    public static class Util {
        private static Logger Logger => Plugin.Logger;

        public static IEnumerator GetGithubJson(VCInterop interop, IVerCheckPlugin plugin,
            Action<GithubReleasePage> method) {
            yield return interop.StartCoroutine(interop.GithubInterop(plugin.GithubAuthor, plugin.GithubProjName));
                var page = interop.CoroutineResults.FirstOrDefault(o =>
                    o.author == plugin.GithubAuthor && o.projName == plugin.GithubProjName).Page;
                Logger.Log($"Pages is Null? {page == null}");
                method.Invoke(page);
        }

    }
}