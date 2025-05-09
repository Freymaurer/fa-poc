import { class_type } from "./fable_modules/fable-library-js.4.19.0/Reflection.js";
import { createElement } from "react";
import React from "react";
import { createObj } from "./fable_modules/fable-library-js.4.19.0/Util.js";
import { Interop_reactApi } from "./fable_modules/Feliz.2.6.0/Interop.fs.js";
import { singleton as singleton_1, ofArray } from "./fable_modules/fable-library-js.4.19.0/List.js";
import { React_contextProvider_138D2F56, useFeliz_React__React_useState_Static_1505, useReact_useMemo_10C6A43C, useReact_useContext_37FA55CF } from "./fable_modules/Feliz.2.6.0/React.fs.js";
import { MapFile as MapFile_1, UserData } from "./ReactContext.js";
import { singleton, delay, toList } from "./fable_modules/fable-library-js.4.19.0/Seq.js";
import { Components_LoadData, Components_SelectIdColFromUserData } from "./Components.js";
import { console_log_4E60E31B } from "./Extensions.js";
import { MapFile } from "./MockData.js";
import { unwrap } from "./fable_modules/fable-library-js.4.19.0/Option.js";

export class View {
    constructor() {
    }
}

export function View_$reflection() {
    return class_type("App.View", undefined, View);
}

export function View_Navbar() {
    let elems_2, elems, elems_1, content;
    return createElement("div", createObj(ofArray([["className", "navbar bg-base-100 shadow-sm"], (elems_2 = [createElement("div", createObj(ofArray([["className", "flex-1"], (elems = [createElement("a", {
        className: "btn btn-ghost text-xl",
        children: "fa-poc",
        href: ".",
    })], ["children", Interop_reactApi.Children.toArray(Array.from(elems))])]))), createElement("div", createObj(ofArray([["className", "flex-none"], (elems_1 = [createElement("a", createObj(ofArray([["href", "https://github.com/Freymaurer/fa-poc"], ["target", "_blank"], ["className", "btn btn-square btn-ghost text-xl"], (content = "<svg xmlns=\"http://www.w3.org/2000/svg\" width=\"24\" height=\"24\" viewBox=\"0 0 24 24\">\n\t<rect width=\"24\" height=\"24\" fill=\"none\" />\n\t<path fill=\"#fff\" d=\"M12 2A10 10 0 0 0 2 12c0 4.42 2.87 8.17 6.84 9.5c.5.08.66-.23.66-.5v-1.69c-2.77.6-3.36-1.34-3.36-1.34c-.46-1.16-1.11-1.47-1.11-1.47c-.91-.62.07-.6.07-.6c1 .07 1.53 1.03 1.53 1.03c.87 1.52 2.34 1.07 2.91.83c.09-.65.35-1.09.63-1.34c-2.22-.25-4.55-1.11-4.55-4.92c0-1.11.38-2 1.03-2.71c-.1-.25-.45-1.29.1-2.64c0 0 .84-.27 2.75 1.02c.79-.22 1.65-.33 2.5-.33s1.71.11 2.5.33c1.91-1.29 2.75-1.02 2.75-1.02c.55 1.35.2 2.39.1 2.64c.65.71 1.03 1.6 1.03 2.71c0 3.82-2.34 4.66-4.57 4.91c.36.31.69.92.69 1.85V21c0 .27.16.59.67.5C19.14 20.16 22 16.42 22 12A10 10 0 0 0 12 2\" />\n</svg>", ["dangerouslySetInnerHTML", {
        __html: content,
    }])])))], ["children", Interop_reactApi.Children.toArray(Array.from(elems_1))])])))], ["children", Interop_reactApi.Children.toArray(Array.from(elems_2))])])));
}

export function View_Hero() {
    let elems_2, elems_1;
    const userData = useReact_useContext_37FA55CF(UserData);
    return createElement("div", createObj(ofArray([["className", "hero grow max-h-full overflow-hidden"], (elems_2 = [createElement("div", createObj(ofArray([["className", "hero-content text-center overflow-hidden max-h-full"], (elems_1 = toList(delay(() => {
        let elems;
        return (userData.data != null) ? singleton(createElement(Components_SelectIdColFromUserData, null)) : singleton(createElement("div", createObj(ofArray([["className", "max-w-md"], (elems = [createElement("h1", {
            className: "text-5xl font-bold",
            children: "Hello there!",
        }), createElement("p", {
            className: "py-6",
            children: "Currently file upload is not supported. You can use provided mock data to explore beta features.",
        }), createElement(Components_LoadData, null)], ["children", Interop_reactApi.Children.toArray(Array.from(elems))])]))));
    })), ["children", Interop_reactApi.Children.toArray(Array.from(elems_1))])])))], ["children", Interop_reactApi.Children.toArray(Array.from(elems_2))])])));
}

export function View_Main() {
    let elems;
    const mapFile = useReact_useMemo_10C6A43C(() => {
        console_log_4E60E31B("Loading mock data");
        return MapFile;
    }, []);
    const patternInput = useFeliz_React__React_useState_Static_1505(undefined);
    const userData = patternInput[0];
    const setUserData = patternInput[1];
    return React_contextProvider_138D2F56(MapFile_1, mapFile, singleton_1(React_contextProvider_138D2F56(UserData, {
        data: unwrap(userData),
        setData: setUserData,
    }, singleton_1(createElement("div", createObj(ofArray([["className", "h-screen bg-base-200 flex flex-col overflow-hidden"], (elems = [View_Navbar(), createElement(View_Hero, null)], ["children", Interop_reactApi.Children.toArray(Array.from(elems))])])))))));
}

