﻿using Nuke.Common.Git;
using Nuke.Common.Tools.DotNet;
using Nuke.Common.Utilities.Collections;
using Tools;
using static Nuke.Common.Tools.DotNet.DotNetTasks;
using static Tools.DotNetExtendedTasks;

partial class Build
{
    const string NugetApiUrl = "https://api.nuget.org/v3/index.json";
    [Secret] [Parameter] string NugetApiKey;

    Target NuGetPush => _ => _
        .Requires(() => NugetApiKey)
        .OnlyWhenStatic(() => GitRepository.IsOnMainOrMasterBranch())
        .OnlyWhenStatic(() => IsLocalBuild)
        .Executes(() =>
        {
            ArtifactsDirectory.GlobFiles("*.nupkg")
                .ForEach(package =>
                {
                    DotNetNuGetPush(settings => settings
                        .SetTargetPath(package)
                        .SetSource(NugetApiUrl)
                        .SetApiKey(NugetApiKey));
                });
        });

    Target NuGetDelete => _ => _
        .Requires(() => NugetApiKey)
        .OnlyWhenStatic(() => GitRepository.IsOnMainOrMasterBranch())
        .OnlyWhenStatic(() => IsLocalBuild)
        .Executes(() =>
        {
            VersionMap.ForEach(map =>
            {
                DotNetNuGetDelete(settings => settings
                    .SetPackage("Nice3point.Revit.Extensions")
                    .SetVersion(map.Key)
                    .SetSource(NugetApiUrl)
                    .SetApiKey(NugetApiKey));
            });
        });
}