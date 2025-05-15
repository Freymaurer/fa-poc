module App.ReactContext

open Feliz
open Fable.Core.JsInterop

// importDefault "react"
let UserData = React.createContext<Context<UserData option>> ()

let DataMapMappingFileInfo = React.createContext<DataMapMappingFileInfo option>()

let Pages = React.createContext<Context<Pages>> ()

let TransformedData = React.createContext<Context<IDataFrame option>> ()