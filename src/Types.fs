namespace App

type Context<'T> = {data: 'T; setData: 'T -> unit}

[<RequireQualifiedAccess>]
type Pages =
    | LoadData
    | SelectIdCol
    | Annotation
type DataMapMappingFile =
    {
        AnnotationFilePath: string
        SourceIdentifierColIndex: int
        SourceIdentifier: string
        SourceOrganism: string
        AnnotationType: string
    }

type DataMapMappingFileInfo =
    {
        DataMapUrl: string
        AnnotationFiles: DataMapMappingFile []
    }

    static member make (url: string) (annotationFiles: DataMapMappingFile []) =
        {
            DataMapUrl = url
            AnnotationFiles = annotationFiles
        }

    static member private findFromComments(comments: ResizeArray<ARCtrl.Comment>, key: string): string =
        comments.ToArray()
        |> Array.tryFind (fun c -> c.Name.IsSome && c.Name.Value.ToLower() = key.ToLower())
        |> Option.bind (fun c -> c.Value)
        |> function
            | None -> failwith $"Key {key} not found in comments"
            | Some v -> v

    member this.Organisms =
        this.AnnotationFiles
        |> Array.map (fun f -> f.SourceOrganism)
        |> Array.distinct

    member this.SourceIdentifiersByOrganism(organism: string) =
        this.AnnotationFiles
        |> Array.filter (fun f -> f.SourceOrganism = organism)
        |> Array.map (fun f -> f.SourceIdentifier)
        |> Array.distinct

    member this.AnnotationTypesByOrganismAndSourceIdentifier(organism: string, sourceIdentifer: string) =
        this.AnnotationFiles
        |> Array.filter (fun f -> f.SourceOrganism = organism && f.SourceIdentifier = sourceIdentifer)
        |> Array.map (fun f -> f.AnnotationType)
        |> Array.distinct

    static member fromDataMap(dataMapUrl: string, dataMap: ARCtrl.DataMap) =
        dataMap.DataContexts
        |> Array.ofSeq
        |> Array.filter (fun x ->
            let ft = DataMapMappingFileInfo.findFromComments (x.Comments, "file type")
            ft.ToLower() = "annotation file"
        )
        |> Array.map (fun context ->
            {
                AnnotationFilePath = context.Name.Value
                SourceIdentifierColIndex = context.Name.Value.Split("#col=") |> Array.last |> int
                SourceIdentifier = context.Explication.Value.NameText
                SourceOrganism = DataMapMappingFileInfo.findFromComments (context.Comments, "organism")
                AnnotationType = DataMapMappingFileInfo.findFromComments (context.Comments, "annotation type")
            }
        )
        |> Array.distinctBy (fun f ->
            f.SourceIdentifier,
            f.SourceOrganism,
            f.AnnotationType
        )
        |> DataMapMappingFileInfo.make dataMapUrl

    member this.GetFetchUrl(file: DataMapMappingFile) : string =
        let baseUrl = this.DataMapUrl
        let truncatedFilePath =
            let trimmed = file.AnnotationFilePath.TrimStart([|'.'|])
            let removeSelector = trimmed.Remove(trimmed.IndexOf("#"))
            removeSelector
        let firstFragment = truncatedFilePath.Split([|"/"|], System.StringSplitOptions.RemoveEmptyEntries).[0]
        let index = baseUrl.IndexOf(firstFragment)
        let baseUrl = baseUrl.Remove(index)
        let fullPath = baseUrl + truncatedFilePath
        fullPath

    member this.GetFetchUrl(sourceOrganism: string, sourceIdent: string, annotation: string) : string =
        let file = this.AnnotationFiles |> Array.tryFind (fun f ->
            f.SourceOrganism = sourceOrganism &&
            f.SourceIdentifier = sourceIdent &&
            f.AnnotationType = annotation
        )
        match file with
        | None -> failwith $"File not found for {sourceOrganism}, {sourceIdent}, {annotation}"
        | Some file ->
            this.GetFetchUrl file


    member this.GetFile(sourceOrganism: string, sourceIdent: string, annotation: string) : DataMapMappingFile =
        let file = this.AnnotationFiles |> Array.tryFind (fun f ->
            f.SourceOrganism = sourceOrganism &&
            f.SourceIdentifier = sourceIdent &&
            f.AnnotationType = annotation
        )
        match file with
        | None -> failwith $"File not found for {sourceOrganism}, {sourceIdent}, {annotation}"
        | Some file -> file

type TransformState = {
    SkipFirstRow: bool
    SelectedColumn: int option
    SelectedOrganism: string option
    SelectedSourceIdentifier: string option
    TargetAnnotations: string list
} with
    static member init() =
        { SelectedColumn = None; SelectedOrganism = None; SelectedSourceIdentifier = None; TargetAnnotations = []; SkipFirstRow = true }

type UserData =
    {
        FileName: string
        Data: string [] []
    } with
    static member init () =
        {
            FileName = ""
            Data = [||]
        }