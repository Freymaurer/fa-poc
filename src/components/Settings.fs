namespace App

open Feliz

type Settings =


    static member private Version() =
        React.fragment [
            Html.label [
                prop.className "label"
                prop.text "Version"
            ]
            Html.p [
                Html.b Constants.__VERSION__
            ]
        ]

    [<ReactComponent>]
    static member private DatamapUrl() =
        let datamapUrl, setDatamapUrl = React.useLocalStorage(Constants.LocalStorage.Keys.__DATAMAP_URL__, Constants.URL.DEFAULT_DATAMAP)
        let datamapInfoCtx = React.useContext(App.ReactContext.DataMapMappingFileInfo)
        let tempDatamapUrl, setTempDatamapUrl = React.useState (datamapUrl)
        let loading, setLoading = React.useState false
        let inputRef = React.useInputRef()

        let tryLoadDataMap =
            fun (url: string) ->
                promise {
                    let! datamap = FetchHelper.fetchDataMap url
                    let info = DataMapMappingFileInfo.fromDataMap(url, datamap)
                    datamapInfoCtx.setData (Some info)
                    return Ok info
                }
                |> Promise.catch(fun e ->
                    Error e
                )

        React.fragment [

            if loading then
                Basic.LoadingModal("Loading mapping file...")

            Html.label [
                prop.className "label"
                prop.text "Datamap URL"
            ]
            Html.div [
                prop.className "join"
                prop.children [
                    Html.input [
                        prop.ref inputRef
                        prop.className [
                            "input join-item w-full"
                            if tempDatamapUrl <> Constants.URL.DEFAULT_DATAMAP then
                                "input-primary"
                        ]
                        prop.title datamapUrl
                        prop.type'.text
                        prop.defaultValue datamapUrl
                        prop.onChange (fun (e: string) ->
                            setTempDatamapUrl e
                        )
                    ]
                    Html.button [
                        prop.className "btn join-item"
                        prop.onClick (fun _ ->
                            promise {
                                setLoading true
                                let! response = tryLoadDataMap tempDatamapUrl
                                match response with
                                | Ok _ ->
                                    setLoading false
                                    setDatamapUrl tempDatamapUrl
                                | Error e ->
                                    setLoading false
                                    Browser.Dom.window.alert "Error loading mapping file. See console for details."
                                    console.error e
                            }
                            |> Promise.start
                        )
                        prop.text "Load"
                    ]
                ]
            ]

            Html.label [
                prop.className "label"
                prop.text "Reset to default"
            ]

            Html.button [
                prop.className "btn w-min"
                prop.disabled <| (datamapUrl = Constants.URL.DEFAULT_DATAMAP && tempDatamapUrl = Constants.URL.DEFAULT_DATAMAP)
                prop.onClick (fun _ ->
                    inputRef.current.Value.value <- Constants.URL.DEFAULT_DATAMAP
                    setTempDatamapUrl Constants.URL.DEFAULT_DATAMAP
                    setDatamapUrl Constants.URL.DEFAULT_DATAMAP
                )
                prop.text "Reset"
            ]
        ]

    [<ReactComponent>]

    static member DividerHorizontal() =
        Html.div [
            prop.className "divider"
        ]


    [<ReactComponent>]
    static member Main() =

        Html.div [

            Html.h1 [
                prop.className "text-2xl font-bold mb-4"
                prop.text "Settings"
            ]

            Html.fieldSet [
                prop.className "fieldset"
                prop.children [
                    Settings.DividerHorizontal()

                    Settings.Version()

                    Settings.DividerHorizontal()

                    Settings.DatamapUrl()
                ]
            ]

        ]
