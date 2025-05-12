namespace App

open Feliz

module private AnnotationUtil =

    let download(filename, bytes:byte []) = bytes.SaveFileAs(filename)

    let downloadFromString(filename, content:string) =
        let bytes = System.Text.Encoding.UTF8.GetBytes(content)
        bytes.SaveFileAs(filename)

type Annotation =

    static member private DownloadButton(file: AnnotatedFile) =
        Html.button [
            prop.className "btn btn-primary btn-wide"
            prop.onClick (fun _ ->
                let tsv = file.ToTsv()
                let filename =
                    file.InputColumn
                    + "_"
                    + (file.ColumnNames |> String.concat "_")
                    + ".tsv"
                AnnotationUtil.downloadFromString(filename, tsv)
            )
            prop.children [
                Icons.Download
                Html.span "Download"
            ]
        ]

    static member AnnotatedFileTable(file: AnnotatedFile) =
        Html.div [
            prop.className "overflow-auto grow"
            prop.children [
                Html.table [
                    prop.className "table"
                    prop.children [
                        Html.thead [ Html.tr [
                            Html.th [
                                prop.text file.InputColumn
                            ]
                            for name in file.ColumnNames do
                                Html.th [
                                    prop.text name
                                ]
                        ] ]
                        Html.tbody [
                            for row in file.AnnotatedValues do
                                Html.tr [
                                    Html.td [
                                        prop.text row.Input
                                    ]
                                    for value in row.AnnotatedValues do
                                        Html.td [
                                            match value with
                                            | Some v ->
                                                prop.text v
                                            | None ->
                                                prop.text "None"
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