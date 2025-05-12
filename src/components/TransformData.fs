namespace App

open Feliz
open Fable.Core
open Fable.Core.JsInterop

type private State = {
    SelectedColumn: int option
    SelectedColumnType: string option
    TransformColumns: string list
} with
    static member init() =
        { SelectedColumn = None; SelectedColumnType = None; TransformColumns = [] }

type TransformData =

    static member private UserDataTable(userData: string [] [], state: State, setState: State -> unit) =
        Basic.FieldSet(
            "User Data",
            state.SelectedColumn.IsSome,
            [|
                "Select ID column",
                Html.div [
                    prop.className "overflow-auto w-full border-2 border-base-300 rounded bg-base-100"
                    prop.children [
                        Html.table [
                            prop.className "table cursor-pointer min-w-max"
                            prop.onClick (fun e ->
                                match e.target?dataset?columnid |> unbox int with
                                | null -> ()
                                | cid ->
                                    let cid = unbox<int> cid
                                    if state.SelectedColumn = Some cid then
                                        setState {state with SelectedColumn = None}
                                    else
                                        setState {state with SelectedColumn = Some cid}
                            )
                            prop.children [
                                Html.colgroup [
                                    match state with
                                    | {SelectedColumn = Some cid} ->
                                        for i in 0 .. (userData.Length - 1) do
                                            if i = cid then
                                                Html.col [
                                                    prop.className "bg-base-200"
                                                ]
                                            else
                                                Html.col [ ]
                                    | _ ->
                                        Html.none
                                ]
                                Html.tbody [
                                    for row in userData do
                                        Html.tr [
                                            for cid in 0 .. (row.Length - 1) do
                                                let cell = row.[cid]
                                                Html.td [
                                                    prop.text cell
                                                    prop.dataColumnId cid
                                                ]
                                        ]
                                ]
                            ]
                        ]
                    ]
                ]
            |]
        )

    static member private InputConfig(mapFile: MapFile, state: State, setState: State -> unit) =
        let names = mapFile.ColumnNames
        Basic.FieldSet(
            "Input Config",
            state.SelectedColumnType.IsSome,
            [|
                "Select id column type",
                Html.select [
                    prop.className "select"
                    prop.title "Select id column type"
                    prop.defaultValue "none"
                    prop.onChange (fun (e: string) ->
                        console.log e
                        if e = "none" then
                            setState {state with SelectedColumnType = None}
                        else
                            setState {state with SelectedColumnType = Some e}
                    )
                    prop.children [
                        Html.option [
                            prop.value "none"
                            prop.disabled true
                            prop.text "Select id column type"
                        ]
                        for name in names do
                            Html.option [
                                prop.value name
                                prop.text name
                            ]
                    ]
                ]
            |]
        )

    [<ReactComponentAttribute>]
    static member private TransformConfig(mapFile: MapFile, state: State, setState: State -> unit) =
        let names = mapFile.ColumnNames
        Basic.FieldSet(
            "Transform Config",
            (not state.TransformColumns.IsEmpty),
            [|
                "Select annotation types",
                React.fragment [
                    for name in names do
                        Html.label [
                            prop.className "label select-none"
                            prop.children [
                                Html.input [
                                    prop.type'.checkbox
                                    prop.className "checkbox"
                                    prop.isChecked (List.contains name state.TransformColumns)
                                    prop.onChange (fun (isChecked: bool) ->
                                        if isChecked then
                                            setState {state with TransformColumns = name :: state.TransformColumns}
                                        else
                                            setState {state with TransformColumns = List.filter ((<>) name) state.TransformColumns}
                                    )
                                ]
                                Html.text name
                            ]
                        ]
                ]
            |]
        )

    [<ReactComponent>]
    static member private Confirm(state, setState: State -> unit) =
        let disabledMsgs =
            let ra = ResizeArray()
            if state.SelectedColumn.IsNone then
                ra.Add "Select ID column in your data!"
            if state.SelectedColumnType.IsNone then
                ra.Add "Select id column type!"
            if state.TransformColumns.IsEmpty then
                ra.Add "Select annotation types!"
            ra.ToArray()
        let isDisabled =
            disabledMsgs.Length > 0
        Html.div [
            prop.className "mx-auto h-min flex flex-col justify-center shrink"
            prop.children [
                Html.button [
                    prop.className "btn btn-wide btn-error"
                    // prop.disabled (state.SelectedColumnType.IsNone || state.TransformColumns.IsEmpty)
                    prop.onClick (fun _ ->
                        console.log "Confirmed"
                    )
                    prop.disabled isDisabled
                    prop.text "Confirm"
                ]
                Html.div [
                    prop.className ""
                    prop.children [
                        Html.ul [
                            for msg in disabledMsgs do
                                Html.li [
                                    prop.className "text-error"
                                    prop.children [
                                        Html.small msg
                                    ]
                                ]
                        ]
                    ]
                ]
            ]
        ]

    [<ReactComponent>]
    static member Main() =
        let userData = React.useContext(App.ReactContext.UserData)
        let mapFile = React.useContext(App.ReactContext.MapFile)
        let state, setState = React.useState(State.init)
        let truncatedData =
            React.useMemo(
                (fun () ->
                    match userData.data with
                    | Some data ->
                        data.Data
                        |> Array.truncate 100
                    | None -> [||]
                ),
                [| userData.data; mapFile |]
            )
        match truncatedData with
        | [||] ->
            Html.h1 "No data loaded"
        | userData ->
            Html.div [
                prop.className "flex lg:flex-row flex-col min-h-0 gap-2"
                prop.children [
                    TransformData.UserDataTable(
                        userData,
                        state,
                        setState
                    )
                    Html.div [
                        prop.className "grow-0 flex flex-col gap-2"
                        prop.children [
                            TransformData.InputConfig(mapFile, state, setState)
                            TransformData.TransformConfig(
                                mapFile,
                                state,
                                setState
                            )
                            Html.div [
                                prop.className "self-end flex grow items-end"
                                prop.children [
                                    TransformData.Confirm(
                                        state,
                                        setState
                                    )
                                ]
                            ]
                        ]
                    ]
                ]
            ]