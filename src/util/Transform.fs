module App.Transform

open Feliz
open Fable.Core

let transform (userData: UserData) (mapFile: DataMapMappingFileInfo) (transformConfig: TransformState) =

    if transformConfig.SelectedOrganism.IsNone then
        failwith "No organism selected"
    if transformConfig.SelectedSourceIdentifier.IsNone then
        failwith "No source identifier selected"
    if transformConfig.TargetAnnotations.IsEmpty then
        failwith "No target annotations selected"
    if transformConfig.SelectedColumn.IsNone then
        failwith "No column selected"

    let annotationFiles =
        transformConfig.TargetAnnotations
        |> List.map (fun annotation ->
            mapFile.GetFile(
                transformConfig.SelectedOrganism.Value,
                transformConfig.SelectedSourceIdentifier.Value,
                annotation
            )
        )

    let userDataIds =
        userData.Data
        |> Array.map (fun row ->
            match transformConfig.SelectedColumn with
            | Some column -> Array.singleton row.[column]
            | None -> failwith "No column selected"
        )
        |> fun x -> if transformConfig.SkipFirstRow then x.[1..] else x

    let mutable df = Danfojs.dfd.DataFrame( userDataIds, {|columns = [|transformConfig.SelectedSourceIdentifier.Value|]|})

    promise {
        for file in annotationFiles do
            let url = mapFile.GetFetchUrl file
            let! csv = Danfojs.dfd.readCSV(
                url,
                {|
                    delimiter = "\t"
                |}
            )
            // csv.print()
            csv.rename(Fable.Core.JsInterop.createObj [
                csv.columns.[file.SourceIdentifierColIndex - 1], transformConfig.SelectedSourceIdentifier.Value
            ])
            df <- Danfojs.dfd.merge(df, csv, [|transformConfig.SelectedSourceIdentifier.Value|], "left")
        df.print()
        return df
    }