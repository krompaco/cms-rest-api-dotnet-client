# A console app for calling the Optimizely CMS REST API

A [Spectre.Console.Cli](https://spectreconsole.net/best-practices) app that can be used for exploring a SaaS CMS instance or to help with source code controlling the various definitions.

## Initial setup

Install the dotnet 9.0 SDK for your operating system.

### For Windows

Configure Windows Terminal to work better with Unicode and emojis.

I assume that you have VS Code installed.

```
powershell
code $PROFILE
# Add this line to your profile.ps1 file that opened and save the file
[console]::InputEncoding = [console]::OutputEncoding = [System.Text.UTF8Encoding]::new()
```

For cmd.exe, use this instruction, also from https://spectreconsole.net/best-practices 

1. Run intl.cpl.
2. Click the Administrative tab
3. Click the Change system locale button.
4. Check the "Use Unicode UTF-8 for worldwide language support" checkbox.
5. Reboot.

### Setup up the CLI locally

Clone, fork or download this repository.

Go into your CMS instance and under Settings, API Client: Add a name and client secret, allow to impersonate can be left unchecked.

Copy `appsettings.json` or Save as `src\CmsRestApiClientCli\appsettings.Development.json` and set BaseUrl to your instance URL without a trailing slash. The values that you just added in the CMS Settings goes into the other two properties (ClientId/ClientSecret).

You should never add `appsettings.Development.json` to the Git repository.

Locally you could also use dotnet user-secrets or command line parameters but I prefer the Git-ignored file. In pipelines I would recommend environment variables for the config or if the pipeline service is good at hiding sensitive variables you could also use command line parameters.

Finally adjust the path for PlainJsonFiles__FolderToProcess to match a path on your machine.

This folder is a sort of _Content Type Package_ and when you run the CLI's `upsert all` command it will create or replace definitions in the CMS instance.

### Build and run the CLI locally

Just open SLN in Visual Studio and Build or if you prefer by dotnet CLI:


```
cd src\CmsRestApiClientCli
dotnet build
```

For easy test access after building:

```
cd bin\Debug\net9.0
# Show --help
.\CmsRestApiClientCli -h
# Check your API client config
.\CmsRestApiClientCli list contenttypes
```

This should show a table of your types:

![Table of your types](docs/images/contenttypes-table.jpg)

If you add `-j` you will get a syntax colored JSON output shown:

![JSON of your types](docs/images/contenttypes-json.jpg)

The CLI currently supports these commands:

```
list <contenttypes|propertyformats|propertygroups|displaytemplates>
upsert <all|propertygroups|contenttypes|displaytemplates>
delete <propertygroups|contenttypes|displaytemplates> <KEY>
```

Be careful and look through all files in your FolderToProcess before running any upsert command. The upserts is most likely what you want to run from a pipeline when deploying your head app.

The commands list and delete are more manual and probably used for exploring and manually cleaning up your instance.