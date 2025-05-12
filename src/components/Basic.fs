namespace App

open Feliz

type Basic =
    [<ReactComponent>]
    static member FieldSet(legend: string, isActive: bool, childpairs: (string * ReactElement) []) =
        Html.fieldSet [
            prop.className [
                "fieldset rounded-box border-2 px-2 shadow-sm min-h-0 overflow-hidden flex flex-col"
                if not isActive then
                    "border-base-300"
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