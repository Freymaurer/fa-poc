import { class_type } from "../fable_modules/fable-library-js.4.19.0/Reflection.js";
import { createElement } from "react";
import React from "react";
import { createObj } from "../fable_modules/fable-library-js.4.19.0/Util.js";
import { join } from "../fable_modules/fable-library-js.4.19.0/String.js";
import { collect, empty, singleton, append, delay, toList } from "../fable_modules/fable-library-js.4.19.0/Seq.js";
import { Interop_reactApi } from "../fable_modules/Feliz.2.6.0/Interop.fs.js";
import { ofArray } from "../fable_modules/fable-library-js.4.19.0/List.js";

export class Basic {
    constructor() {
    }
}

export function Basic_$reflection() {
    return class_type("App.Basic", undefined, Basic);
}

export function Basic_FieldSet_Z67080547(basic_FieldSet_Z67080547InputProps) {
    let elems;
    const childpairs = basic_FieldSet_Z67080547InputProps.childpairs;
    const isActive = basic_FieldSet_Z67080547InputProps.isActive;
    const legend = basic_FieldSet_Z67080547InputProps.legend;
    return createElement("fieldset", createObj(ofArray([["className", join(" ", toList(delay(() => append(singleton("fieldset rounded-box border-2 px-2 shadow-sm min-h-0 overflow-hidden flex flex-col"), delay(() => (!isActive ? singleton("border-base-300") : empty()))))))], (elems = toList(delay(() => append(singleton(createElement("legend", {
        className: "fieldset-legend",
        children: legend,
    })), delay(() => collect((matchValue) => {
        const label = matchValue[0];
        const child = matchValue[1];
        return append(singleton(createElement("label", {
            className: "label",
            children: label,
        })), delay(() => singleton(child)));
    }, childpairs))))), ["children", Interop_reactApi.Children.toArray(Array.from(elems))])])));
}

