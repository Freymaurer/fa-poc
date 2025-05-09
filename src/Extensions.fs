[<AutoOpen>]
module Extensions

open System
open Fable.Core
open Fable.Core.JsInterop
open Feliz

type prop with
    static member inline dataColumnId (id: int) =
        Interop.mkAttr "data-columnid" id

type console =
    static member log (message: obj) = Browser.Dom.console.log message

[<RequireQualifiedAccess>]
module StaticFile =

    /// Function that imports a static file by it's relative path.
    let inline import (path: string) : string = importDefault<string> path
