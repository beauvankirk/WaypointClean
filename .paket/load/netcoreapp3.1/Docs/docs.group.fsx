namespace PaketLoadScripts

#r "C:\\Users\\beauv\\.nuget\\packages\\fsharp.formatting\\4.1.0\\lib\\netstandard2.0\\System.Security.Permissions.dll" 
#r "C:\\Users\\beauv\\.nuget\\packages\\fsharp.formatting\\4.1.0\\lib\\netstandard2.0\\Microsoft.CodeAnalysis.dll" 
#r "C:\\Users\\beauv\\.nuget\\packages\\fsharp.formatting\\4.1.0\\lib\\netstandard2.0\\Microsoft.CSharp.dll" 
#r "C:\\Users\\beauv\\.nuget\\packages\\fsharp.formatting\\4.1.0\\lib\\netstandard2.0\\Microsoft.AspNetCore.Razor.Language.dll" 
#r "C:\\Users\\beauv\\.nuget\\packages\\fsharp.formatting\\4.1.0\\lib\\netstandard2.0\\Microsoft.AspNetCore.Html.Abstractions.dll" 
#r "C:\\Users\\beauv\\.nuget\\packages\\fsharp.formatting\\4.1.0\\lib\\netstandard2.0\\CSharpFormat.dll" 
#r "C:\\Users\\beauv\\.nuget\\packages\\system.runtime.loader\\4.3.0\\lib\\netstandard1.5\\System.Runtime.Loader.dll" 
#r "C:\\Users\\beauv\\.nuget\\packages\\microsoft.build.framework\\16.8.0\\lib\\netstandard2.0\\Microsoft.Build.Framework.dll" 
#r "C:\\Users\\beauv\\.nuget\\packages\\microsoft.bcl.asyncinterfaces\\1.1.1\\lib\\netstandard2.1\\Microsoft.Bcl.AsyncInterfaces.dll" 
#r "C:\\Users\\beauv\\.nuget\\packages\\microsoft.win32.registry\\4.7.0\\lib\\netstandard2.0\\Microsoft.Win32.Registry.dll" 
#r "C:\\Users\\beauv\\.nuget\\packages\\system.collections.immutable\\1.7.1\\lib\\netstandard2.0\\System.Collections.Immutable.dll" 
#r "C:\\Users\\beauv\\.nuget\\packages\\system.diagnostics.diagnosticsource\\4.7.1\\lib\\netstandard1.3\\System.Diagnostics.DiagnosticSource.dll" 
#r "C:\\Users\\beauv\\.nuget\\packages\\system.formats.asn1\\5.0.0\\lib\\netstandard2.0\\System.Formats.Asn1.dll" 
#r "C:\\Users\\beauv\\.nuget\\packages\\system.io.pipelines\\4.7.3\\lib\\netcoreapp3.0\\System.IO.Pipelines.dll" 
#r "C:\\Users\\beauv\\.nuget\\packages\\system.net.http.winhttphandler\\4.7.2\\lib\\netstandard2.0\\System.Net.Http.WinHttpHandler.dll" 
#r "C:\\Users\\beauv\\.nuget\\packages\\system.resources.extensions\\4.7.1\\lib\\netstandard2.0\\System.Resources.Extensions.dll" 
#r "C:\\Users\\beauv\\.nuget\\packages\\system.runtime.interopservices.windowsruntime\\4.3.0\\lib\\netstandard1.3\\System.Runtime.InteropServices.WindowsRuntime.dll" 
#r "C:\\Users\\beauv\\.nuget\\packages\\system.security.cryptography.protecteddata\\4.7.0\\lib\\netstandard2.0\\System.Security.Cryptography.ProtectedData.dll" 
#r "C:\\Users\\beauv\\.nuget\\packages\\system.security.permissions\\4.7.0\\lib\\netcoreapp3.0\\System.Security.Permissions.dll" 
#r "C:\\Users\\beauv\\.nuget\\packages\\system.text.encodings.web\\4.7.1\\lib\\netstandard2.1\\System.Text.Encodings.Web.dll" 
#r "C:\\Users\\beauv\\.nuget\\packages\\nuget.common\\5.8.0\\lib\\netstandard2.0\\NuGet.Common.dll" 
#r "C:\\Users\\beauv\\.nuget\\packages\\system.security.accesscontrol\\4.7.0\\lib\\netstandard2.0\\System.Security.AccessControl.dll" 
#r "C:\\Users\\beauv\\.nuget\\packages\\microsoft.win32.systemevents\\4.7.0\\lib\\netstandard2.0\\Microsoft.Win32.SystemEvents.dll" 
#r "C:\\Users\\beauv\\.nuget\\packages\\newtonsoft.json\\12.0.3\\lib\\netstandard2.0\\Newtonsoft.Json.dll" 
#r "C:\\Users\\beauv\\.nuget\\packages\\nuget.frameworks\\5.8.0\\lib\\netstandard2.0\\NuGet.Frameworks.dll" 
#r "C:\\Users\\beauv\\.nuget\\packages\\nuget.versioning\\5.8.0\\lib\\netstandard2.0\\NuGet.Versioning.dll" 
#r "C:\\Users\\beauv\\.nuget\\packages\\system.buffers\\4.5.1\\lib\\netstandard2.0\\System.Buffers.dll" 
#r "C:\\Users\\beauv\\.nuget\\packages\\system.codedom\\4.7.0\\lib\\netstandard2.0\\System.CodeDom.dll" 
#r "C:\\Users\\beauv\\.nuget\\packages\\system.drawing.common\\4.7.1\\lib\\netstandard2.0\\System.Drawing.Common.dll" 
#r "C:\\Users\\beauv\\.nuget\\packages\\system.numerics.vectors\\4.5.0\\lib\\netstandard2.0\\System.Numerics.Vectors.dll" 
#r "C:\\Users\\beauv\\.nuget\\packages\\system.reflection.typeextensions\\4.7.0\\lib\\netstandard2.0\\System.Reflection.TypeExtensions.dll" 
#r "C:\\Users\\beauv\\.nuget\\packages\\system.runtime.compilerservices.unsafe\\4.7.1\\lib\\netstandard2.0\\System.Runtime.CompilerServices.Unsafe.dll" 
#r "C:\\Users\\beauv\\.nuget\\packages\\system.security.cryptography.cng\\5.0.0\\lib\\netcoreapp3.0\\System.Security.Cryptography.Cng.dll" 
#r "C:\\Users\\beauv\\.nuget\\packages\\system.security.cryptography.openssl\\4.7.0\\lib\\netcoreapp3.0\\System.Security.Cryptography.OpenSsl.dll" 
#r "C:\\Users\\beauv\\.nuget\\packages\\system.security.principal.windows\\4.7.0\\lib\\netstandard2.0\\System.Security.Principal.Windows.dll" 
#r "C:\\Users\\beauv\\.nuget\\packages\\system.threading.tasks.dataflow\\4.11.1\\lib\\netstandard2.0\\System.Threading.Tasks.Dataflow.dll" 
#r "C:\\Users\\beauv\\.nuget\\packages\\fsharp.formatting\\4.1.0\\lib\\netstandard2.0\\RazorEngine.NetCore.dll" 
#r "C:\\Users\\beauv\\.nuget\\packages\\fsharp.formatting\\4.1.0\\lib\\netstandard2.0\\Microsoft.CodeAnalysis.CSharp.dll" 
#r "C:\\Users\\beauv\\.nuget\\packages\\fsharp.formatting\\4.1.0\\lib\\netstandard2.0\\Microsoft.AspNetCore.Razor.dll" 
#r "C:\\Users\\beauv\\.nuget\\packages\\fsharp.formatting\\4.1.0\\lib\\netstandard2.0\\FSharp.Formatting.Common.dll" 
#r "C:\\Users\\beauv\\.nuget\\packages\\fsharp.formatting\\4.1.0\\lib\\netstandard2.0\\FSharp.Formatting.dll" 
#r "C:\\Users\\beauv\\.nuget\\packages\\microsoft.build.utilities.core\\16.8.0\\lib\\netstandard2.0\\Microsoft.Build.Utilities.Core.dll" 
#r "C:\\Users\\beauv\\.nuget\\packages\\system.security.cryptography.xml\\4.7.0\\lib\\netstandard2.0\\System.Security.Cryptography.Xml.dll" 
#r "C:\\Users\\beauv\\.nuget\\packages\\nuget.configuration\\5.8.0\\lib\\netstandard2.0\\NuGet.Configuration.dll" 
#r "C:\\Users\\beauv\\.nuget\\packages\\system.reflection.metadata\\1.8.1\\lib\\netstandard2.0\\System.Reflection.Metadata.dll" 
#r "C:\\Users\\beauv\\.nuget\\packages\\system.security.cryptography.pkcs\\5.0.0\\lib\\netcoreapp3.0\\System.Security.Cryptography.Pkcs.dll" 
#r "C:\\Users\\beauv\\.nuget\\packages\\system.text.json\\4.7.2\\lib\\netcoreapp3.0\\System.Text.Json.dll" 
#r "C:\\Users\\beauv\\.nuget\\packages\\system.text.encoding.codepages\\4.7.1\\lib\\netstandard2.0\\System.Text.Encoding.CodePages.dll" 
#r "C:\\Users\\beauv\\.nuget\\packages\\fsharp.formatting\\4.1.0\\lib\\netstandard2.0\\Microsoft.AspNetCore.Razor.Runtime.dll" 
#r "C:\\Users\\beauv\\.nuget\\packages\\fsharp.formatting\\4.1.0\\lib\\netstandard2.0\\FSharp.Markdown.dll" 
#r "C:\\Users\\beauv\\.nuget\\packages\\fsharp.formatting\\4.1.0\\lib\\netstandard2.0\\FSharp.CodeFormat.dll" 
#r "C:\\Users\\beauv\\.nuget\\packages\\fsharp.compiler.service\\37.0.0\\lib\\netstandard2.0\\FSharp.Compiler.Service.dll" 
#r "C:\\Users\\beauv\\.nuget\\packages\\microsoft.build.tasks.core\\16.8.0\\lib\\netstandard2.0\\Microsoft.Build.Tasks.Core.dll" 
#r "C:\\Users\\beauv\\.nuget\\packages\\nuget.packaging\\5.8.0\\lib\\netstandard2.0\\NuGet.Packaging.dll" 
#r "C:\\Users\\beauv\\.nuget\\packages\\fsharp.formatting\\4.1.0\\lib\\netstandard2.0\\FSharp.Literate.dll" 
#r "C:\\Users\\beauv\\.nuget\\packages\\nuget.protocol\\5.8.0\\lib\\netstandard2.0\\NuGet.Protocol.dll" 
#r "C:\\Users\\beauv\\.nuget\\packages\\fsharp.formatting\\4.1.0\\lib\\netstandard2.0\\FSharp.MetadataFormat.dll" 
#r "C:\\Users\\beauv\\.nuget\\packages\\fsharp.formatting\\4.1.0\\lib\\netstandard2.0\\FSharp.Formatting.Razor.dll" 