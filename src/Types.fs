namespace App


[<RequireQualifiedAccess>]
type Pages =
    | LoadData
    | SelectIdCol
    | Annotation

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

    member this.getValuesByIndex(index: int, ?columnNames: string []) =
        let columnNames =
            match columnNames with
            | Some names -> names
            | None -> this.ColumnNames

        this.Columns
        |> Array.filter (fun c -> Array.contains c.Name columnNames)
        |> Array.map (fun c ->
            {|
                name = c.Name;
                values =
                    c.Values.[index].Trim()
                    |> _.Split("; ") // can be manyOf relationships
            |}
        )

type UserData =
    {
        Data: string [] []
    } with
    static member init () =
        {
            Data = [||]
        }