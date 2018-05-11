# BeatSaberVersionChecker
Implement the `IVerCheckPlugin` interface instead of `IPlugin`

`IVerCheckPlugin` can be found in `BeatSaberVersionChecker.Interfaces`

Implement the `IPlugin` fields as usual, with the `IVerCheckPlugin` fields being similar to
```cs
    public string GithubAuthor => "artman41";
    public string GithubProjName => "BeatSaberVersionChecker";
```

If you're having difficulty working out what these should be, look at your github url
> https://github.com/artman41/BeatSaberVersionChecker

---
**Requires BeatSaberLogger**
> https://github.com/artman41/BeatSaberLogger/releases
