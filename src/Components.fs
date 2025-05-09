namespace App

open Feliz
open Fable.Core
open Fable.Core.JsInterop

type Components =
    [<ReactComponent>]
    static member LoadData() =
        let loading, setLoading = React.useState(false)
        let dataCtx = React.useContext(App.ReactContext.UserData)
        Html.button [
            prop.className "btn btn-primary";
            prop.children [
                if loading then
                    Html.div [
                        prop.className "loading loading-spinner"
                        prop.style [ style.width 20; style.height 20 ]
                    ]
                else
                    Html.text "Load mock data"
            ]
            prop.onClick (fun _ ->
                promise {
                    setLoading true
                    do! Promise.sleep 1000
                    MockData.UserData |> Some |> dataCtx.setData
                    setLoading false
                }
                |> Promise.catch (fun ex ->
                    Browser.Dom.console.error ex
                    setLoading false
                )
                |> Promise.start
            )
        ]

    [<ReactComponent>]
    static member SelectIdColFromUserData() =
        let userData = React.useContext(App.ReactContext.UserData)
        let mapFile = React.useContext(App.ReactContext.MapFile)
        let selectedColumn, setSelectedColumn = React.useState None
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
        Html.div [
            prop.className "shadow-lg bg-base-100 rounded-lg p-8 m-8"
            prop.children [
                Html.div [
                    prop.className "overflow-auto max-h-[600px]"
                    prop.children [
                        match truncatedData with
                        | [||] ->
                            Html.h1 "No data loaded"
                        | userData ->
                            Html.table [
                                prop.className "table cursor-pointer"
                                prop.onClick (fun e ->
                                    match e.target?dataset?columnid |> unbox int with
                                    | null -> ()
                                    | cid ->
                                        let cid = unbox<int> cid
                                        if selectedColumn = Some cid then
                                            setSelectedColumn None
                                        else
                                            setSelectedColumn (Some cid)
                                )
                                prop.children [
                                    Html.colgroup [
                                        match selectedColumn with
                                        | Some cid ->
                                            for i in 0 .. (userData.Length - 1) do
                                                if i = cid then
                                                    Html.col [
                                                        prop.className "bg-base-200"
                                                    ]
                                                else
                                                    Html.col [ ]
                                        | None ->
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
            ]
        ]