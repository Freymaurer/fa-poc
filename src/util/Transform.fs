module App.Transform

open Feliz
open Fable.Core

let transform (userData: UserData) (mapFile: MapFile) (selectedColumn: int) (inputColumnName: string) (targetColumnNames: string list): AnnotatedFile =
    {
        Columns = [||]
    }