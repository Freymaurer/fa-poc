namespace App

open Feliz
open Fable.Core.JsInterop

type LoadData =
    [<ReactComponent>]
    static member Main() =
        let loading, setLoading = React.useState(false)
        let dataCtx = React.useContext(App.ReactContext.UserData)
        let pageCtx = React.useContext(App.ReactContext.Pages)
        let uploadRef = React.useInputRef()

        let setData = fun (fileName: string) (tsvValues: string[][]) ->
            promise {
                setLoading true
                let userData: UserData = {
                    FileName = fileName
                    Data = tsvValues
                }
                userData |> Some |> dataCtx.setData
                setLoading false
                pageCtx.setData Pages.SelectIdCol
            }
            |> Promise.catch (fun ex ->
                Browser.Dom.console.error ex
                setLoading false
            )


        React.fragment [
            Html.div [
                prop.className "flex flex-col md:flex-row gap-2 justify-center items-center"
                prop.children [
                    Html.input [
                        prop.ref uploadRef
                        prop.className "file-input file-input-primary"
                        prop.type'.file
                        prop.onChange(fun (file: Browser.Types.File) ->
                            if isNull file then
                                ()
                            else
                                promise {
                                    let! (text: string) = file?text()
                                    let values =
                                        text.Split([| "\r\n"; "\n" |], System.StringSplitOptions.None)
                                        |> Array.map (fun line ->
                                            line.Split([| "\t" |], System.StringSplitOptions.None)
                                            |> Array.map (fun value -> value.Trim())
                                        )
                                    do! setData file.name values
                                }
                                |> Promise.catch (fun ex ->
                                    Browser.Dom.console.error ex
                                    setLoading false
                                )
                                |> Promise.start
                        )
                    ]
                    Html.button [
                        prop.className "btn btn-outline";
                        prop.children [
                            Html.text "Load mock data"
                        ]
                        prop.onClick (fun _ ->
                            setData MockData.UserData.FileName MockData.UserData.Data
                            |> Promise.start
                        )
                    ]

                ]
            ]
            if loading then
                Basic.LoadingModal("Loading data...")
        ]