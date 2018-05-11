using System;
using System.Collections;
using System.Collections.Generic;
using BeatSaberVersionChecker.Interfaces;
using BeatSaberVersionChecker.Misc.Github;
using UnityEngine;
using UnityEngine.Networking;

namespace BeatSaberVersionChecker.Misc {
    public class VCInterop : MonoBehaviour {
        private static BeatSaberLogger.Logger Logger => Plugin.Logger;

        public List<GithubCoroutine> CoroutineResults = new List<GithubCoroutine>();

        public struct GithubCoroutine {
            public string author { get; private set; }
            public string projName { get; private set; }
            public GithubReleasePage Page { get; private set; }

            public GithubCoroutine(string author, string projName, GithubReleasePage page) {
                this.author = author;
                this.projName = projName;
                this.Page = page;
            }
        }
            
        void Start() {
            DontDestroyOnLoad(this);
        }

        internal IEnumerator GithubInterop(string author, string projName) {
            Logger.Log($"Given strings {author}, {projName}");
            var x = new CoroutineWithData(this, _GetGithubJson(author, projName));
            yield return x.coroutine;
            var page = x.result as GithubReleasePage;
            CoroutineResults.Add(new GithubCoroutine(author, projName, page));
            if (page != null) Logger.Log($"Result is {page?.name}");
        }

        IEnumerator _GetGithubJson(string author, string projName) {
            Logger.Log("Getting github release");
            //xyonico BeatSaberSongInjector
            using (var git = new WWW($"https://api.github.com/repos/{author}/{projName}/releases/latest")) {
                yield return git;
                // Show results as text
                var sanitisedJson = git.text.Replace("\\t", "");
                Logger.Log("string => " + Environment.NewLine + sanitisedJson);
                Debug.Log(sanitisedJson);
                GithubReleasePage page;
                try {
                    page = JsonUtility.FromJson<GithubReleasePage>(sanitisedJson);
                    Logger.Log("Got github release");
                    foreach (var field in page.GetType().GetFields()) {
                        Logger.Log($"{field.Name} {field.GetValue(page)}");
                    }
                }
                catch (Exception e) {
                    page = null;
                }

                yield return page;
            }
        }
    }
}