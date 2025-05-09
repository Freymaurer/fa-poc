namespace App

open Feliz
open Feliz.Router

type View =


    // /// <summary>
    // /// A React component that uses Feliz.Router
    // /// to determine what to show based on the current URL
    // /// </summary>
    // [<ReactComponent>]
    // static member Router() =
    //     let (currentUrl, updateUrl) = React.useState(Router.currentUrl())
    //     React.router [
    //         router.onUrlChanged updateUrl
    //         router.children [
    //             match currentUrl with
    //             | [ ] -> Html.h1 "Index"
    //             | [ "hello" ] -> Components.HelloWorld()
    //             | [ "counter" ] -> Components.Counter()
    //             | otherwise -> Html.h1 "Not found"
    //         ]
    //     ]

    static member Navbar() =
        Html.div [
            prop.className "navbar bg-base-100 shadow-sm"
            prop.children [
                Html.div [
                    prop.className "flex-1"
                    prop.children [ Html.a [ prop.className "btn btn-ghost text-xl"; prop.text "fa-poc"; prop.href "." ] ]
                ]
                // Html.div [
                //     prop.className "flex-none"
                //     prop.children [
                //         Html.a [
                //             prop.className "btn btn-square btn-ghost text-xl"
                //             prop.dangerouslySetInnerHTML
                //                 """ <svg xmlns="http://www.w3.org/2000/svg" fill="none" viewBox="0 0 24 24" class="inline-block h-5 w-5 stroke-current"> <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M5 12h.01M12 12h.01M19 12h.01M6 12a1 1 0 11-2 0 1 1 0 012 0zm7 0a1 1 0 11-2 0 1 1 0 012 0zm7 0a1 1 0 11-2 0 1 1 0 012 0z"></path> </svg>"""
                //         ]
                //     ]
                // ]
            ]
        ]

    static member Hero() =
        Html.div [
            prop.className "hero grow"
            prop.children [
                Html.div [
                    prop.className "hero-content text-center"
                    prop.children [
                        Html.div [
                            prop.className "max-w-md"
                            prop.children [
                                Html.h1 [ prop.className "text-5xl font-bold"; prop.text "Hello there!" ]
                                Html.p [
                                    prop.className "py-6"
                                    prop.text
                                        "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Quisque ut erat nec nisi efficitur facilisis."
                                ]
                                Html.button [ prop.className "btn btn-primary"; prop.text "Get started" ]
                            ]
                        ]
                    ]
                ]
            ]
        ]


    static member Main() =
        Html.div [
            prop.className "min-h-screen bg-base-200 flex flex-col"
            prop.children [
                View.Navbar()
                View.Hero()
            ]
        ]