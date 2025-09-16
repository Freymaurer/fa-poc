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

    let userDataHead, userDataBody = userData.Data |> Array.splitAt 1
    let userDataHead = userDataHead.[0]
    let userDataIdColumnName = userDataHead.[transformConfig.SelectedColumn.Value]

    let mutable df = Danfojs.dfd.DataFrame( userDataBody, {|columns = userDataHead|})

    promise {
        for file in annotationFiles do
            let url = mapFile.GetFetchUrl file
            let! csv = Danfojs.dfd.readCSV(
                url,
                {|
                    delimiter = "\t"
                |}
            )
            csv.print()
            if not (csv.columns.[file.SourceIdentifierColIndex - 1] = userDataIdColumnName) then
                // rename the column to match the user data
                // this is a bit of a hack, but it works for now
                csv.rename(Fable.Core.JsInterop.createObj [
                    csv.columns.[file.SourceIdentifierColIndex - 1], userDataIdColumnName
                ])
            df.print()
            df <- Danfojs.dfd.merge(df, csv, [|userDataIdColumnName|], MergeHow.Left)
        df.print()
        return df
    }