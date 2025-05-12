module App.Transform

open Feliz
open Fable.Core

let transform (userData: UserData) (mapFile: MapFile) (selectedColumn: int) (inputColumnName: string) (skipFirstRow: bool) (targetColumnNames: string list): AnnotatedFile =

    let targetColumnNames =
        List.filter (fun name -> name <> inputColumnName) targetColumnNames

    let skipRows =
        if skipFirstRow then
            1
        else
            0

    let userDataIds =
        userData.Data
        |> Array.map (fun row -> row.[selectedColumn])
        |> Array.skip skipRows

    let columnNames, annotation = mapFile.annotateValues(
        inputColumnName,
        userDataIds,
        targetColumnNames
    )

    {
        InputColumn = inputColumnName
        ColumnNames = columnNames
        AnnotatedValues = annotation
    }