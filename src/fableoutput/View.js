import { class_type } from "./fable_modules/fable-library-js.4.19.0/Reflection.js";
import { createElement } from "react";
import { createObj } from "./fable_modules/fable-library-js.4.19.0/Util.js";
import { Interop_reactApi } from "./fable_modules/Feliz.2.6.0/Interop.fs.js";
import { ofArray } from "./fable_modules/fable-library-js.4.19.0/List.js";

export class View {
    constructor() {
    }
}

export function View_$reflection() {
    return class_type("App.View", undefined, View);
}

export function View_Navbar() {
    let elems_1, elems;
    return createElement("div", createObj(ofArray([["className", "navbar bg-base-100 shadow-sm"], (elems_1 = [createElement("div", createObj(ofArray([["className", "flex-1"], (elems = [createElement("a", {
        className: "btn btn-ghost text-xl",
        children: "fa-poc",
        href: ".",
    })], ["children", Interop_reactApi.Children.toArray(Array.from(elems))])])))], ["children", Interop_reactApi.Children.toArray(Array.from(elems_1))])])));
}

export function View_Hero() {
    let elems_2, elems_1, elems, value_12;
    return createElement("div", createObj(ofArray([["className", "hero grow"], (elems_2 = [createElement("div", createObj(ofArray([["className", "hero-content text-center"], (elems_1 = [createElement("div", createObj(ofArray([["className", "max-w-md"], (elems = [createElement("h1", {
        className: "text-5xl font-bold",
        children: "Hello there!",
    }), createElement("p", createObj(ofArray([["className", "py-6"], (value_12 = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Quisque ut erat nec nisi efficitur facilisis.", ["children", value_12])]))), createElement("button", {
        className: "btn btn-primary",
        children: "Get started",
    })], ["children", Interop_reactApi.Children.toArray(Array.from(elems))])])))], ["children", Interop_reactApi.Children.toArray(Array.from(elems_1))])])))], ["children", Interop_reactApi.Children.toArray(Array.from(elems_2))])])));
}

export function View_Main() {
    let elems;
    return createElement("div", createObj(ofArray([["className", "min-h-screen bg-base-200 flex flex-col"], (elems = [View_Navbar(), View_Hero()], ["children", Interop_reactApi.Children.toArray(Array.from(elems))])])));
}

