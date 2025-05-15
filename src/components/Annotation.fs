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
            prop.className "btn btn-primary btn-wide"
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

    static member AnnotatedFileTable(file: IDataFrame) =
        Html.div [
            prop.className "overflow-auto grow"
            prop.children [
                Html.table [
                    prop.className "table"
                    prop.children [
                        Html.thead [ Html.tr [
                            for name in file.columns do
                                Html.th [
                                    prop.text name
                                ]
                        ] ]
                        Html.tbody [
                            for row in file.values.[1..] do
                                Html.tr [
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

    [<ReactComponent>]
    static member Main() =
        let transformedDataCtx = React.useContext(App.ReactContext.TransformedData)
        match transformedDataCtx.data with
        | None ->
            Html.div "No transformed data available"
        | Some transformedData ->
            Html.div [
                prop.className "flex min-h-0 flex-col md:flex-row gap-2 lg:gap-4"
                prop.children [
                    Annotation.AnnotatedFileTable transformedData
                    Html.div [
                        prop.className "self-end flex items-end grow-0 min-w-3xs"
                        prop.children [
                            Annotation.DownloadButton transformedData
                        ]
                    ]

                ]
            ]