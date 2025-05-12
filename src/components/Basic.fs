namespace App

open Feliz

type Basic =
    [<ReactComponent>]
    static member FieldSet(legend: string, isActive: bool, childpairs: (string * ReactElement) [], ?containerClasses: string) =
        Html.fieldSet [
            prop.className [
                "fieldset rounded-box border-2 px-2 shadow-sm min-h-0 overflow-hidden flex flex-col"
                if not isActive then
                    "border-base-300"
                if containerClasses.IsSome then
                    containerClasses.Value
            ]
            prop.children [
                Html.legend [
                    prop.className "fieldset-legend"
                    prop.text legend
                ]
                for (label, child) in childpairs do
                    Html.label [
                        prop.className "label"
                        prop.text label
                    ]
                    child
            ]
        ]

    [<ReactComponent>]
    static member OverflowContainer(children: ReactElement) =
        Html.div [
            prop.className "flex grow justify-center min-h-0"
            prop.children [
                Html.div [
                    prop.className "container mx-auto bg-base-100 h-[90%] rounded-lg m-2 md:m-4 p-4 xl:p-8 shadow-lg overflow-hidden flex flex-col gap-2"
                    prop.children children
                ]
            ]
        ]

    [<ReactComponent>]
    static member LoadingModal(message: string) =
        ReactDOM.createPortal(
            Html.div [
                prop.className "modal modal-open"
                prop.children [
                    Html.div [
                        prop.className "modal-box"
                        prop.children [
                            Html.h2 [
                                prop.className "text-lg font-bold"
                                prop.text message
                            ]
                            Html.progress [
                                prop.className "progress progress-primary"
                            ]
                        ]
                    ]
                ]
            ],
            Browser.Dom.document.body
        )