namespace App

open Feliz

module private AnnotationUtil =

    let download(filename, bytes:byte []) = bytes.SaveFileAs(filename)

    let downloadFromString(filename, content:string) =
        let bytes = System.Text.Encoding.UTF8.GetBytes(content)
        bytes.SaveFileAs(filename)

type Annotation =

    [<ReactComponent>]
    static member private DownloadButton(file: IDataFrame) =
        let userDataCtx = React.useContext(App.ReactContext.UserData)
        Html.button [
            prop.className "btn btn-primary md:btn-wide"
            prop.onClick (fun _ ->
                promise {
                    let fileName =
                        let index = userDataCtx.data.Value.FileName.LastIndexOf "."
                        userDataCtx.data.Value.FileName.Substring(0, index) + "_annotated"
                    do! Danfojs.dfd.toCSV(file, {| sep = "\t"; fileName = fileName; download = true |})
                }
                |> Promise.start
            )
            prop.children [
                Icons.Download
                Html.span "Download"
            ]
        ]

    [<ReactComponent>]
    static member AnnotatedFileTable(file: IDataFrame) =

        let currentBin, setCurrentBin = React.useState 0

        let BinSize = 100

        let TotalBins = React.useMemo((fun () -> System.Math.Max(file.values.Length / BinSize, 1)), [| file |])

        let values = React.useMemo(
            (fun () ->
                let start = currentBin * BinSize
                file.fillNa("null").values.[1..]
                |> Array.skip start
                |> Array.truncate BinSize
            ),
            [| file; currentBin |]
        )
        Html.div [
            prop.className "min-h-0 grow flex flex-col gap-2"
            prop.children [
                Html.div [
                    prop.className "overflow-auto grow border border-base-content/5"
                    prop.children [
                        Html.table [
                            prop.className "table table-xs"
                            prop.children [
                                Html.thead [ Html.tr [
                                    Html.th [
                                        prop.text "Index"
                                    ]
                                    for name in file.columns do
                                        Html.th [
                                            prop.text name
                                        ]
                                ] ]
                                Html.tbody [
                                    for i in 0 .. values.Length - 1 do
                                        let row = values.[i]
                                        let index = (i + 1) + (currentBin * BinSize)
                                        Html.tr [
                                            Html.td index
                                            for value in row do
                                                Html.td [
                                                    prop.text (unbox<string> value)
                                                ]
                                        ]
                                ]
                            ]
                        ]
                    ]
                ]
                Basic.Pagination(
                    currentPage = currentBin,
                    totalPages = TotalBins,
                    updatePage = (fun page ->
                        setCurrentBin page
                    )
                )
                Html.div [
                    prop.className "self-end flex items-end grow-0"
                    prop.children [
                        Annotation.DownloadButton file
                    ]
                ]
            ]
        ]

    [<ReactComponent>]
    static member Main() =
        let transformedDataCtx = React.useContext(App.ReactContext.TransformedData)
        match transformedDataCtx.data with
        | None ->
            Html.div "No transformed data available"
        | Some transformedData ->
            Annotation.AnnotatedFileTable transformedData