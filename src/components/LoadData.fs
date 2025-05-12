namespace App

open Feliz

type LoadData =
    [<ReactComponent>]
    static member Main() =
        let loading, setLoading = React.useState(false)
        let dataCtx = React.useContext(App.ReactContext.UserData)
        let pageCtx = React.useContext(App.ReactContext.Pages)
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
                    pageCtx.setData Pages.SelectIdCol
                }
                |> Promise.catch (fun ex ->
                    Browser.Dom.console.error ex
                    setLoading false
                )
                |> Promise.start
            )
        ]