namespace App

open Feliz

type Icons =
    static member Download =
        Html.i [
            prop.dangerouslySetInnerHTML """<svg xmlns="http://www.w3.org/2000/svg" width="24" height="24" viewBox="0 0 24 24">
	<rect width="24" height="24" fill="none" />
	<path fill="currentColor" d="m12 16l-5-5l1.4-1.45l2.6 2.6V4h2v8.15l2.6-2.6L17 11zm-6 4q-.825 0-1.412-.587T4 18v-3h2v3h12v-3h2v3q0 .825-.587 1.413T18 20z" />
</svg>"""
        ]

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