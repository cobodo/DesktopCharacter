-- premake5.lua

solution "DesktopCharacter"
    configurations { "Debug", "Release" }
    location ("./")
    
project "DesktopCharacter"
    location ("./DesktopCharacter")
    kind "WindowedApp"
    language "C#"
    
    framework ("4.5.2")
    
    defines { 
        "TRACE" 
    }
    
    configuration { "Debug*" }
        defines { "DEBUG" }
        flags   { "Symbols" }
    
    --configuration { "Release*" }
        --optimize "On"

    files {
         "./**.cs", 
         "./**.xaml", 
         "./**.config",
         "./**.png" 
    }
    
    excludes  { 
        "DesktopCharacter/obj/**.*",
        "DesktopCharacter/bin/**.*" 
    }
    
    links {"System"}
    links ("System.Data")
    links ("System.Core")
    links ("System.Drawing")
    links ("System.ComponentModel.DataAnnotations")
    links ("System.Data.DataSetExtensions")
    links ("System.Net.Http")
    links ("System.Xaml")
    links ("System.Xml")
    links ("System.Xml.Linq")
    links ("System.Runtime.Serialization")
    links ("System.Windows.Forms")
    links ("WindowsBase")
    links ("Microsoft.CSharp")
    links ("PresentationCore")
    links ("PresentationFramework")
    
    links ("packages/CoreTweet.0.6.2.277/lib/net45/CoreTweet.dll")
    links ("packages/EntityFramework.6.1.3/lib/net45/EntityFramework.dll")
    links ("packages/EntityFramework.6.1.3/lib/net45/EntityFramework.SqlServer.dll")
    links ("packages/EventSource4Net.0.4.0.0/lib/net45/EventSource4Net.dll")
    links ("packages/LivetCask.1.3.1.0/lib/net45/Livet.dll")
    links ("packages/LivetCask.1.3.1.0/lib/net45/Microsoft.Expression.Interactions.dll")
    links ("packages/LivetCask.1.3.1.0/lib/net45/System.Windows.Interactivity.dll")
    links ("packages/Newtonsoft.Json.8.0.3/lib/net45/Newtonsoft.Json.dll")
    links ("packages/ReactiveProperty.2.5/lib/net45/ReactiveProperty.dll")
    links ("packages/ReactiveProperty.2.5/lib/net45/ReactiveProperty.DataAnnotations.dll")
    links ("packages/ReactiveProperty.2.5/lib/net45/ReactiveProperty.NET45.dll")
    links ("packages/Rx-Core.2.2.5/lib/net45/System.Reactive.Core.dll")
    links ("packages/Rx-Interfaces.2.2.5/lib/net45/System.Reactive.Interfaces.dll")
    links ("packages/Rx-Linq.2.2.5/lib/net45/System.Reactive.Linq.dll")
    links ("packages/Rx-PlatformServices.2.2.5/lib/net45/System.Reactive.PlatformServices.dll")
    links ("packages/Rx-XAML.2.2.5/lib/net45/System.Reactive.Windows.Threading.dll")
    links ("packages/slf4net.0.1.32.1/lib/net35/slf4net.dll")
    links ("packages/SQLite.CodeFirst.1.1.11.0/lib/net45/SQLite.CodeFirst.dll")
    links ("packages/System.Data.SQLite.Core.1.0.99.0/lib/net451/System.Data.SQLite.dll")
    links ("packages/System.Data.SQLite.EF6.1.0.99.0/lib/net451/System.Data.SQLite.EF6.dll")
    links ("packages/System.Data.SQLite.Linq.1.0.99.0/lib/net451/System.Data.SQLite.Linq.dll")
    
    