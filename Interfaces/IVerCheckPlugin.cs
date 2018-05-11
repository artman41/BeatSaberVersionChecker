using IllusionPlugin;

namespace BeatSaberVersionChecker.Interfaces {
    public interface IVerCheckPlugin : IPlugin{
        string GithubAuthor { get; }
        string GithubProjName { get; }
    }
}