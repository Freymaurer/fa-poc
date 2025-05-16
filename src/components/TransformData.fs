namespace App

open Feliz
open Fable.Core
open Fable.Core.JsInterop

type TransformData =

    static member private UserDataTable(userData: string [] [], state: TransformState, setState: TransformState -> unit) =
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
            |],
            "min-w-0 md:w-[80%]"
        )

    static member private InputConfig(mapFile: DataMapMappingFileInfo, state: TransformState, setState: TransformState -> unit) =
        Basic.FieldSet(
            "Input Config",
            state.SelectedSourceIdentifier.IsSome,
            [|
                "Skip first row",
                Html.label [
                    prop.className "label select-none"
                    prop.children [
                        Html.input [
                            prop.type'.checkbox
                            prop.className "checkbox"
                            prop.isChecked state.SkipFirstRow
                            prop.onChange (fun (isChecked: bool) ->
                                setState {state with SkipFirstRow = isChecked}
                            )
                        ]
                        Html.text "Skip first row"
                    ]
                ];
                "Select organism",
                Html.select [
                    prop.className "select"
                    prop.title "Select organism"
                    prop.defaultValue "none"
                    prop.onChange (fun (e: string) ->
                        if e = "none" then
                            setState {state with SelectedOrganism = None}
                        else
                            setState {state with SelectedOrganism = Some e}
                    )
                    prop.children [
                        Html.option [
                            prop.value "none"
                            prop.disabled true
                            prop.text "Select organism"
                        ]
                        for org in mapFile.Organisms do
                            Html.option [
                                prop.value org
                                prop.text org
                            ]
                    ]
                ]
                "Select id column type",
                Html.select [
                    prop.className "select"
                    prop.title "Select id column type"
                    prop.defaultValue "none"
                    prop.disabled (state.SelectedOrganism.IsNone)
                    prop.onChange (fun (e: string) ->
                        if e = "none" then
                            setState {state with SelectedSourceIdentifier = None}
                        else
                            setState {state with SelectedSourceIdentifier = Some e}
                    )
                    prop.children [
                        Html.option [
                            prop.value "none"
                            prop.disabled true
                            prop.text "Select id column type"
                        ]
                        match state.SelectedOrganism with
                        | Some org ->
                            for name in mapFile.SourceIdentifiersByOrganism org do
                                Html.option [
                                    prop.value name
                                    prop.text name
                                ]
                        | None ->
                            Html.none
                    ]
                ]
            |]
        )

    [<ReactComponentAttribute>]
    static member private TransformConfig(mapFile: DataMapMappingFileInfo, state: TransformState, setState: TransformState -> unit) =
        Basic.FieldSet(
            "Transform Config",
            (not state.TargetAnnotations.IsEmpty),
            [|
                "Select annotation types",
                React.fragment [
                    match state with
                    | {SelectedOrganism = Some org; SelectedSourceIdentifier = Some ident } ->
                        let targets = mapFile.AnnotationTypesByOrganismAndSourceIdentifier(org, ident)
                        Html.label [
                            prop.className "label select-none"
                            prop.children [
                                Html.input [
                                    prop.type'.checkbox
                                    prop.className "checkbox"
                                    prop.isChecked (List.ofArray targets = state.TargetAnnotations)
                                    prop.onChange (fun (isChecked: bool) ->
                                        if isChecked then
                                            setState {state with TargetAnnotations = List.ofArray targets}
                                        else
                                            setState {state with TargetAnnotations = []}
                                    )
                                ]
                                Html.text "Select all"
                            ]
                        ]
                        for target in targets do
                            Html.label [
                                prop.className "label select-none"
                                prop.children [
                                    Html.input [
                                        prop.type'.checkbox
                                        prop.className "checkbox"
                                        prop.isChecked (List.contains target state.TargetAnnotations)
                                        prop.onChange (fun (isChecked: bool) ->
                                            if isChecked then
                                                setState {state with TargetAnnotations = target :: state.TargetAnnotations}
                                            else
                                                setState {state with TargetAnnotations = List.filter ((<>) target) state.TargetAnnotations}
                                        )
                                    ]
                                    Html.text target
                                ]
                            ]
                    | _ ->
                        Html.div [
                            prop.className "label select-none"
                            prop.children [
                                Html.text "Select organism and id column type first!"
                            ]
                        ]
                ]
            |]
        )

    // [<ReactComponent>]
    static member private Confirm(state) =
        let loading, setLoading = React.useState(false)
        let disabledMsgs =
            let ra = ResizeArray()
            if state.SelectedColumn.IsNone then
                ra.Add "Select ID column in your data!"
            if state.SelectedSourceIdentifier.IsNone then
                ra.Add "Select id column type!"
            if state.TargetAnnotations.IsEmpty then
                ra.Add "Select annotation types!"
            ra.ToArray()
        let isDisabled =
            disabledMsgs.Length > 0
        let userDataCtx = React.useContext(App.ReactContext.UserData)
        let mapFileCtx = React.useContext(App.ReactContext.DataMapMappingFileInfo)
        let transformedDataCtx = React.useContext(App.ReactContext.TransformedData)
        let pageCtx = React.useContext(App.ReactContext.Pages)

        let transform = fun () ->
            promise {
                if userDataCtx.data.IsNone then
                    failwith "Transform: Error: No user data available!"
                if mapFileCtx.data.IsNone then
                    failwith "Transform: Error: No map file available!"
                if isDisabled then
                    failwith "Transform: Error: Invalid transform config!"
                setLoading true
                let! transformedData=
                    Transform.transform
                        userDataCtx.data.Value
                        mapFileCtx.data.Value
                        state
                transformedDataCtx.setData (Some transformedData)
                setLoading false
                pageCtx.setData Pages.Annotation
            }
            |> Promise.catch (fun ex ->
                Browser.Dom.window.alert "Error transforming data. See console for details."
                console.error ex
                setLoading false
            )


        React.fragment [
            if loading then
                Basic.LoadingModal("Transforming data...")
            Html.div [
                prop.className "mx-auto h-min flex flex-col justify-center shrink gap-2"
                prop.children [
                    Html.button [
                        prop.className "btn btn-wide btn-primary min-w-3xs"
                        prop.onClick (fun _ ->
                            if not isDisabled then
                                transform()
                                |> Promise.start
                        )
                        prop.disabled isDisabled
                        prop.text "Confirm"
                    ]
                    if isDisabled then
                        Html.div [
                            prop.role "alert"
                            prop.className "p-2 alert alert-error alert-soft"
                            prop.children [
                                Icons.Error
                                Html.ul [
                                    prop.children [
                                        for msg in disabledMsgs do
                                            Html.li [
                                                prop.children [
                                                    Html.small msg
                                                ]
                                            ]
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
        let mapFile = React.useContext(App.ReactContext.DataMapMappingFileInfo)
        let state, setState = React.useState(TransformState.init)
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
        match truncatedData, mapFile with
        | [||], _ ->
            Html.h1 "No data loaded"
        | _, { data = None } ->
            Html.h1 "No mapping file loaded"
        | userData, { data = Some mapFile } ->
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
                                        state
                                    )
                                ]
                            ]
                        ]
                    ]
                ]
            ]