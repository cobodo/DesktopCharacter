-- premake5.lua
location "build"

solution "DesktopCharacter"
do
    configurations { "Debug", "Release" }
    platforms { "AnyCPU" }
end

project "DesktopCharacter"
do
    kind "WindowedApp"
    language "C#"

    files {
         "*.cs", "*.xaml", "*.config", "*.png" 
     }
    
    links ("System")
    links ("System.ComponentModel.DataAnnotations")
    links ("System.Data")
    links ("../packages/CoreTweet.0.6.2.277/lib/net45/CoreTweet.dll")
    links ("../packages/EntityFramework.6.1.3/lib/net45/EntityFramework.dll")
    links ("../packages/EntityFramework.6.1.3/lib/net45/EntityFramework.SqlServer.dll")
    links ("../packages/EventSource4Net.0.4.0.0/lib/net45/EventSource4Net.dll")
    links ("../packages/LivetCask.1.3.1.0/lib/net45/Livet.dll")
    links ("../packages/LivetCask.1.3.1.0/lib/net45/Microsoft.Expression.Interactions.dll")
    links ("../packages/Newtonsoft.Json.8.0.3/lib/net45/Newtonsoft.Json.dll")
    links ("../packages/ReactiveProperty.2.5/lib/net45/ReactiveProperty.dll")
    links ("../packages/ReactiveProperty.2.5/lib/net45/ReactiveProperty.DataAnnotations.dll")
    links ("../packages/ReactiveProperty.2.5/lib/net45/ReactiveProperty.NET45.dll")
    links ("../packages/slf4net.0.1.32.1/lib/net35/slf4net.dll")
    links ("../packages/SQLite.CodeFirst.1.1.11.0/lib/net45/SQLite.CodeFirst.dll")
    links ("../packages/System.Data.SQLite.Core.1.0.99.0/lib/net451/System.Data.SQLite.dll")
end