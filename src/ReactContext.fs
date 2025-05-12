module App.ReactContext

open Feliz
open Fable.Core.JsInterop

// importDefault "react"

let UserData = React.createContext<Context<UserData option>> ()

let MapFile = React.createContext<MapFile>()

let Pages = React.createContext<Context<Pages>> ()

let TransformedData =
    React.createContext<Context<AnnotatedFile option>> ()