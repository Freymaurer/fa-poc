namespace App

type Context<'T> = {data: 'T; setData: 'T -> unit}

[<RequireQualifiedAccess>]
type Pages =
    | LoadData
    | SelectIdCol
    | Annotation

type AnnotatedValue =
    {
        Input: string
        FoundInMapFile: bool
        AnnotatedValues: string option []
    }

type MapFileColumn =
    {
        Name: string
        Description: string
        Values: string []
    }

type MapFile =
    {
        Columns: MapFileColumn []
    }

    /// Length of all column values must be the same
    member this.IsValid =
        if this.Columns.Length = 0 then
            false
        else
            let length = this.Columns.[0].Values.Length
            this.Columns
            |> Array.forall (fun c -> c.Values.Length = length)

    member this.ColumnNames =
        this.Columns
        |> Array.map (fun c -> c.Name)

    member this.tryFindIndex(columnName: string, key: string) =
        this.Columns
        |> Array.find(fun c -> c.Name = columnName)
        |> _.Values
        |> Array.tryFindIndex (fun v -> v = key)

    member this.tryFindIndices(columnName: string, keys: string []) =
        let values =
            this.Columns
            |> Array.find(fun c -> c.Name = columnName)
            |> _.Values

        keys
        |> Array.map (fun key ->
            values
            |> Array.tryFindIndex (fun v -> v = key)
        )


    member this.annotateValues(inputColumnName: string, inputIds: string [], ?columnNames: string list) =
        let columnNames =
            match columnNames with
            | Some names -> names
            | None -> this.ColumnNames |> List.ofArray

        let columns =
            this.Columns
            |> Array.filter (fun c -> List.contains c.Name columnNames)

        let values =
            inputIds
            |> Array.map (fun userId ->
                let indexOpt = this.tryFindIndex(inputColumnName, userId)
                {
                    Input = userId
                    FoundInMapFile = indexOpt.IsSome
                    AnnotatedValues =
                        match indexOpt with
                        | Some index ->
                            columns
                            |> Array.map (fun column -> column.Values.[index].Trim() |> Some)
                        | None ->
                            Array.init columns.Length (fun _ ->
                                None
                            )
                }
            )
        columns |> Array.map _.Name, values

type UserData =
    {
        Data: string [] []
    } with
    static member init () =
        {
            Data = [||]
        }

type AnnotatedFile =
    {
        InputColumn: string
        ColumnNames: string []
        AnnotatedValues: AnnotatedValue []
    }

    member this.ToTsv() =

        let ra = ResizeArray()

        this.ColumnNames
        |> Array.map (fun c -> c.Trim())
        |> String.concat "\t"
        |> fun x -> $"{this.InputColumn}\t{x}"
        |> ra.Add

        this.AnnotatedValues
        |> Array.iter (fun v ->

            ra.Add ($"{v.Input}\t")

            v.AnnotatedValues
            |> Array.map (fun a -> a |> Option.map _.Trim() |> Option.defaultValue "None")
            |> String.concat "\t"
            |> ra.Add
        )

        ra
        |> String.concat "\n"