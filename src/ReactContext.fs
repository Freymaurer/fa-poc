module App.ReactContext

open Feliz
open Fable.Core.JsInterop

// importDefault "react"

let UserData = React.createContext<{|data: UserData option; setData: UserData option -> unit|}> ()

let MapFile = React.createContext<MapFile>()

let Pages = React.createContext<{|data: Pages; setData: Pages -> unit|}> ()