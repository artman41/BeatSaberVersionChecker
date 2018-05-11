using System;
using System.Collections.Generic;
using System.Linq;
using BeatSaberVersionChecker.Interfaces;
using BeatSaberVersionChecker.Misc;
using UnityEngine;
using Logger = BeatSaberLogger.Logger;

namespace BeatSaberVersionChecker {
  public class Plugin : IVerCheckPlugin {
    public string Name => "Version Checker";
    public string Version => "1.0";
    public string GithubAuthor => "artman41";
    public string GithubProjName => "BeatSaberVersionChecker";

    private IEnumerable<IVerCheckPlugin> Plugins => IllusionInjector.PluginManager.Plugins.Where(o => o is IVerCheckPlugin).Cast<IVerCheckPlugin>();
    private List<LatestPluginInfo> PluginInfos = new List<LatestPluginInfo>();

    internal static Logger Logger;

    internal VCInterop VersionCheckerInterop = null;
    
    public void OnApplicationStart() {
    }

    public void OnApplicationQuit() {
      Logger.Stop();
    }

    public void OnLevelWasLoaded(int level) {
      if (VersionCheckerInterop == null) {
        var x = new GameObject();
        VersionCheckerInterop = x.AddComponent<VCInterop>();
      }
        if (Logger == null) {
          Logger = new Logger(this);
          Logger.Log($"VCInterop is Null {VersionCheckerInterop == null}");
          foreach (var plugin in Plugins) {
            Logger.Log($"{plugin.Name}, {plugin.Version}");
            PluginInfos.Add(new LatestPluginInfo(plugin, VersionCheckerInterop));
          }
          PluginInfos.ForEach(o => {
            Logger.Log($"{o.Plugin.Name} is{(o.IsLatestVersion ? " " : " not ")}up to date");
          });
        }
    }

    public void OnLevelWasInitialized(int level) {
      //
    }

    public void OnUpdate() {
      //
    }

    public void OnFixedUpdate() {
      //
    }
  }
}