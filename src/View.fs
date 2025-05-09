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
                Html.div [
                    prop.className "flex-none"
                    prop.children [
                        Html.a [
                            prop.href Constants.URL.GitHub
                            prop.target "_blank"
                            prop.className "btn btn-square btn-ghost text-xl"
                            prop.dangerouslySetInnerHTML // https://icon-sets.iconify.design/?query=github
                                """<svg xmlns="http://www.w3.org/2000/svg" width="24" height="24" viewBox="0 0 24 24">
	<rect width="24" height="24" fill="none" />
	<path fill="#000" d="M12 2A10 10 0 0 0 2 12c0 4.42 2.87 8.17 6.84 9.5c.5.08.66-.23.66-.5v-1.69c-2.77.6-3.36-1.34-3.36-1.34c-.46-1.16-1.11-1.47-1.11-1.47c-.91-.62.07-.6.07-.6c1 .07 1.53 1.03 1.53 1.03c.87 1.52 2.34 1.07 2.91.83c.09-.65.35-1.09.63-1.34c-2.22-.25-4.55-1.11-4.55-4.92c0-1.11.38-2 1.03-2.71c-.1-.25-.45-1.29.1-2.64c0 0 .84-.27 2.75 1.02c.79-.22 1.65-.33 2.5-.33s1.71.11 2.5.33c1.91-1.29 2.75-1.02 2.75-1.02c.55 1.35.2 2.39.1 2.64c.65.71 1.03 1.6 1.03 2.71c0 3.82-2.34 4.66-4.57 4.91c.36.31.69.92.69 1.85V21c0 .27.16.59.67.5C19.14 20.16 22 16.42 22 12A10 10 0 0 0 12 2" />
</svg>"""
                        ]
                    ]
                ]
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