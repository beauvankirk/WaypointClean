// --------------------------------------------------------------------------------------
// FAKE build script
// --------------------------------------------------------------------------------------
#r "paket: groupref build //"

#load ".fake/build.fsx/intellisense.fsx"
#r "netstandard"

open System
open Paket
open Fake.Core
open Fake.DotNet
open Fake.Tools
open Fake.IO
open Fake.IO.FileSystemOperators
open Fake.IO.Globbing.Operators
open Fake.Core.TargetOperators
open Fake.Api

// --------------------------------------------------------------------------------------
// Project Selections
// --------------------------------------------------------------------------------------
let testProjects =
    seq { @"tests\WaypointClean.UnitTests\WaypointClean.UnitTests.fsproj" }

// --------------------------------------------------------------------------------------
// Information about the project to be used at NuGet and in AssemblyInfo files
// --------------------------------------------------------------------------------------

let project = "WaypointClean"

let summary = "TODO: Add Summary"
let authors = "TODO: Add Authors"
let tags = "TODO: Add package tags"
let copyright = "TODO: Add copyright"

let gitOwner = "TODO: ADD_AUTHOR"
let gitName = "WaypointClean"
let gitHome = "https://github.com/" + gitOwner
let gitUrl = gitHome + "/" + gitName

// --------------------------------------------------------------------------------------
// Build variables
// --------------------------------------------------------------------------------------

let buildDir = "./build/"
let nugetDir = "./out/"


System.Environment.CurrentDirectory <- __SOURCE_DIRECTORY__
let changelogFilename = "CHANGELOG.md"
let changelog = Changelog.load changelogFilename
let latestEntry = changelog.LatestEntry

// Helper function to remove blank lines
let isEmptyChange =
    function
    | Changelog.Change.Added s
    | Changelog.Change.Changed s
    | Changelog.Change.Deprecated s
    | Changelog.Change.Fixed s
    | Changelog.Change.Removed s
    | Changelog.Change.Security s
    | Changelog.Change.Custom (_, s) -> String.isNullOrWhiteSpace s.CleanedText

let nugetVersion = latestEntry.NuGetVersion

let packageReleaseNotes =
    sprintf "%s/blob/v%s/CHANGELOG.md" gitUrl latestEntry.NuGetVersion

let releaseNotes =
    latestEntry.Changes
    |> List.filter (isEmptyChange >> not)
    |> List.map (fun c -> " * " + c.ToString())
    |> String.concat "\n"

// --------------------------------------------------------------------------------------
// Helpers
// --------------------------------------------------------------------------------------
let isNullOrWhiteSpace = System.String.IsNullOrWhiteSpace

let exec cmd args dir =
    let proc =
        CreateProcess.fromRawCommandLine cmd args
        |> CreateProcess.ensureExitCodeWithMessage (sprintf "Error while running '%s' with args: %s" cmd args)

    (if isNullOrWhiteSpace dir
     then proc
     else proc |> CreateProcess.withWorkingDirectory dir)
    |> Proc.run
    |> ignore

let getBuildParam = Environment.environVar
let DoNothing = ignore

let seqProduct<'A, 'B> (seqA: 'A seq) (seqB: 'B seq) =
    seq {
        for a in seqA do
            for b in seqB -> (a, b)

    }

let joinStringList (sl: string list) =
    let rec recHelper (rsl: string list) =
        match rsl with
        | head :: tail -> " " + head + (recHelper tail)
        | [] -> ""

    match sl with
    | head :: tail -> head + (recHelper tail)
    | [] -> ""

let targetFrameworks (projectPath: string) =
    let project = Paket.ProjectFile.loadFromFile projectPath

    let singularTargetFramework =
        Paket.ProjectFile.getTargetFramework project

    match singularTargetFramework with
    | Some f -> [ f ]
    | None ->
        project
        |> ProjectFile.getTargetFrameworksParsed
let matrixDotnetRun (projectPaths: string seq) =
    projectPaths
    |> Seq.toList
    |> sprintf "Running matrixDotnetRun with projectPaths: %A"
    |> Trace.trace

    let projectAndTargetTuples =
        seq {
            for p in projectPaths do
                for f in (targetFrameworks p) -> (p, f)
        }

    projectAndTargetTuples
    |> Seq.toList
    |> sprintf "Resolved project-target tuples: %A"
    |> Trace.trace

    projectAndTargetTuples
    |> Seq.iter (fun (p: string, f: string) ->
        let dotnetRunArgs = [ "--project"; p; "--framework"; f ]
        "run"
        :: dotnetRunArgs
        |> joinStringList
        |> exec "dotnet"
        <| "."
        // "dotnet"  (@"run --project .\tests\FsScratch.UnitTests\FsScratch.UnitTests.fsproj --framework " + f) "."
        )

let safePath (p: string) =
    let fullPath = Path.combine __SOURCE_DIRECTORY__ p
    match Path.isFile (fullPath) with
    | true -> Some fullPath
    | false ->
        "Warning, specified path: '"
        + p
        + "', which resolved to: '"
        + fullPath
        + "' was not found"
        |> Trace.traceError
        None

let verifiedFilePaths (ps: string seq) =
    seq {
        for p in ps do
            let verified = safePath p
            match verified with
            | Some vp -> yield vp
            | None -> ()
    }
// --------------------------------------------------------------------------------------
// Build Targets
// --------------------------------------------------------------------------------------

Target.create "Clean" (fun _ -> Shell.cleanDirs [ buildDir; nugetDir ])

Target.create "Build" (fun _ -> DotNet.build id "")

Target.create "Debug" (fun _ ->
    "Resolved __SOURCE_DIRECTORY__: "
    + __SOURCE_DIRECTORY__
    |> Trace.trace
    testProjects
    |> Seq.toList
    |> sprintf "Specified testProjects: %A"
    |> Trace.trace
    verifiedFilePaths testProjects
    |> Seq.toList
    |> sprintf "Verified Test project paths: %A"
    |> Trace.trace)

Target.create "Test" (fun _ -> verifiedFilePaths testProjects |> matrixDotnetRun
    // let testProjectPaths =
    //     testProjects
    //     |> Seq.map (Path.combine __SOURCE_DIRECTORY__)
    //     |> Seq.map
    // let testProject = Paket.ProjectFile.loadFromFile testProjectPath
    // let targetFrameworks = ProjectFile.getTargetFrameworksParsed testProject
    // targetFrameworks
    // |> List.iter (
    //     fun (f:string) -> exec "dotnet"  (@"run --project .\tests\FsScratch.UnitTests\FsScratch.UnitTests.fsproj --framework " + f) "."
    // )
    // exec "dotnet"  @"run --project .\tests\WaypointClean.UnitTests\WaypointClean.UnitTests.fsproj" "."
    )

Target.create "Docs" (fun _ -> exec "dotnet" @"fornax build" "docs")
Target.create "BuildDocs" (fun _ ->
    DotNet.build id ".\docs\WaypointClean.Docs.fsproj")


// --------------------------------------------------------------------------------------
// Release Targets
// --------------------------------------------------------------------------------------
Target.create "BuildRelease" (fun _ ->
    DotNet.build (fun p ->
        { p with
              Configuration = DotNet.BuildConfiguration.Release
              // OutputPath = Some buildDir
              MSBuildParams =
                  { p.MSBuildParams with
                        Properties =
                            [ ("Version", nugetVersion)
                              ("PackageReleaseNotes", packageReleaseNotes) ] } }) "WaypointClean.sln")

Target.create "Publish" (fun _ ->
    !! "src/**/*.??proj"
    |> Seq.iter (fun n ->
        let projectTargetFrameworks = targetFrameworks n
        projectTargetFrameworks
        |> Seq.ofList
        |> Seq.iter (fun tf ->
            DotNet.publish (fun c ->
                {c with
                    OutputPath = Some (Path.combine buildDir tf)
                    Configuration = DotNet.BuildConfiguration.Release
                    Framework = Some tf
                    MSBuildParams = 
                       { c.MSBuildParams with
                            Properties =
                                [ ("Version", nugetVersion)
                                  ("PackageReleaseNotes", packageReleaseNotes) ] } 
                }
            ) n
        )
    )
)

Target.create "PackWithPaket" (fun _ ->
    let properties =
        [ ("Version", nugetVersion)
          ("Authors", authors)
          ("PackageProjectUrl", gitUrl)
          ("PackageTags", tags)
          ("RepositoryType", "git")
          ("RepositoryUrl", gitUrl)
          ("PackageLicenseUrl", gitUrl + "/LICENSE")
          ("Copyright", copyright)
          ("PackageReleaseNotes", packageReleaseNotes)
          ("PackageDescription", summary)
          ("EnableSourceLink", "true") ]
    // type PacketPackParams =
    //      { ToolPath = findPaketExecutable ""
    //         ToolType = ToolType.Create ()
    //         TimeOut = TimeSpan.FromMinutes 5.
    //         Version = null
    //         SpecificVersions = []
    //         LockDependencies = false
    //         ReleaseNotes = null
    //         BuildConfig = null
    //         BuildPlatform = null
    //         TemplateFile = null
    //         ProjectUrl = null
    //         ExcludedTemplates = []
    //         WorkingDir = "."
    //         OutputPath = "./temp"
    //         Symbols = false
    //         IncludeReferencedProjects = false
    //         MinimumFromLockFile = false
    //         PinProjectReferences = false }

    Paket.pack (fun p ->
        { p with
              // Configuration = DotNet.BuildConfiguration.Release
              // OutputPath = Some nugetDir
              OutputPath = nugetDir
        // MSBuildParams = { p.MSBuildParams with Properties = properties}
        // NoRestore = true
        }))

Target.create "PackWithDotnet" (fun _ ->
    let properties =
        [ ("Version", nugetVersion)
          ("Authors", authors)
          ("PackageProjectUrl", gitUrl)
          ("PackageTags", tags)
          ("RepositoryType", "git")
          ("RepositoryUrl", gitUrl)
          ("PackageLicenseUrl", gitUrl + "/LICENSE")
          ("Copyright", copyright)
          ("PackageReleaseNotes", packageReleaseNotes)
          ("PackageDescription", summary)
          ("EnableSourceLink", "true") ]


    DotNet.pack (fun p ->
        { p with
              Configuration = DotNet.BuildConfiguration.Release
              OutputPath = Some nugetDir
              MSBuildParams =
                  { p.MSBuildParams with
                        Properties = properties }
              NoRestore = true }) "WaypointClean.sln")

Target.create "ReleaseGitHub" (fun _ ->
    let remote =
        Git.CommandHelper.getGitResult "" "remote -v"
        |> Seq.filter (fun (s: string) -> s.EndsWith("(push)"))
        |> Seq.tryFind (fun (s: string) -> s.Contains(gitOwner + "/" + gitName))
        |> function
        | None -> gitHome + "/" + gitName
        | Some (s: string) -> s.Split().[0]

    Git.Staging.stageAll ""
    Git.Commit.exec "" (sprintf "Bump version to %s" nugetVersion)
    Git.Branches.pushBranch "" remote (Git.Information.getBranchName "")


    Git.Branches.tag "" nugetVersion
    Git.Branches.pushTag "" remote nugetVersion

    let client =
        let user =
            match getBuildParam "github-user" with
            | s when not (isNullOrWhiteSpace s) -> s
            | _ -> Fake.Core.UserInput.getUserInput "Username: "

        let pw =
            match getBuildParam "github-pw" with
            | s when not (isNullOrWhiteSpace s) -> s
            | _ -> Fake.Core.UserInput.getUserPassword "Password: "

        // Git.createClient user pw
        GitHub.createClient user pw

    let files = !!(nugetDir </> "*.nupkg")



    // release on github
    let cl =
        client
        |> GitHub.draftNewRelease gitOwner gitName nugetVersion (latestEntry.SemVer.PreRelease <> None) [ releaseNotes ]

    (cl, files)
    ||> Seq.fold (fun acc e -> acc |> GitHub.uploadFile e)
    |> GitHub.publishDraft //releaseDraft
    |> Async.RunSynchronously)

Target.create "Push" (fun _ ->
    let key =
        match getBuildParam "nuget-key" with
        | s when not (isNullOrWhiteSpace s) -> s
        | _ -> UserInput.getUserPassword "NuGet Key: "

    Paket.push (fun p ->
        { p with
              WorkingDir = nugetDir
              ApiKey = key
              ToolType = ToolType.CreateLocalTool() }))

// --------------------------------------------------------------------------------------
// Build order
// --------------------------------------------------------------------------------------
Target.create "DoNothing" DoNothing
Target.create "Default" DoNothing
Target.create "Release" DoNothing
Target.create "Rebuild" DoNothing
Target.create "Pack" DoNothing
Target.create "All" DoNothing
Target.create "CleanAll" DoNothing



// "Clean" ==> "Rebuild"
// "Build" ==> "Rebuild"

// "Build" ==> "Test"

// "BuildRelease" ==> "Docs"

// "Build" ==> "Pack"
// "PackWithDotnet" ==> "Pack"

// "Rebuild" ==> "All"
// "Test" ==> "All"
// "Pack" ==> "All"
// "Docs" ==> "All"

// "Build" ==> "Default"

// "Clean"
//   ==> "Build"
//   ==> "Test"
//   ==> "Default"

// "Clean"
//  ==> "BuildRelease"
//  ==> "Docs"

// "Default"
//   ==> "Pack"
//   ==> "ReleaseGitHub"
//   ==> "Push"
//   ==> "Release"

// "Clean"
//   ==> "Build"
//   ==> "Test"
//   ==> "Pack"
//   ==> "BuildRelease"
//   ==> "Docs"
//   ==> "CleanAll"

"Clean"
// ==> "BuildRelease"
==> "Publish"
==> "Docs"
==> "CleanAll"

// "Clean"
//   ?=> "Build"
//   ?=> "Rebuild"
//   ?=> "Test"
//   ?=> "PackWithDotnet"
//   ?=> "PackWithPaket"
//   ?=> "BuildRelease"
//   ?=> "Docs"
//   ?=> "ReleaseGitHub"
//   ?=> "Push"
//   ?=> "Release"

Target.runOrDefault "Default"
