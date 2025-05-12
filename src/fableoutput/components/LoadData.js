import { class_type } from "../fable_modules/fable-library-js.4.19.0/Reflection.js";
import { createElement } from "react";
import React from "react";
import { useReact_useContext_37FA55CF, useFeliz_React__React_useState_Static_1505 } from "../fable_modules/Feliz.2.6.0/React.fs.js";
import { Pages, UserData } from "../ReactContext.js";
import { createObj } from "../fable_modules/fable-library-js.4.19.0/Util.js";
import { singleton, delay, toList } from "../fable_modules/fable-library-js.4.19.0/Seq.js";
import { Interop_reactApi } from "../fable_modules/Feliz.2.6.0/Interop.fs.js";
import { PromiseBuilder__Delay_62FBFDE1, PromiseBuilder__Run_212F1D4B } from "../fable_modules/Fable.Promise.3.2.0/Promise.fs.js";
import { promise } from "../fable_modules/Fable.Promise.3.2.0/PromiseImpl.fs.js";
import { UserData as UserData_1 } from "../MockData.js";
import { Pages as Pages_1 } from "../Types.js";
import { some } from "../fable_modules/fable-library-js.4.19.0/Option.js";
import { ofArray } from "../fable_modules/fable-library-js.4.19.0/List.js";

export class LoadData {
    constructor() {
    }
}

export function LoadData_$reflection() {
    return class_type("App.LoadData", undefined, LoadData);
}

export function LoadData_Main() {
    let elems;
    const patternInput = useFeliz_React__React_useState_Static_1505(false);
    const setLoading = patternInput[1];
    const loading = patternInput[0];
    const dataCtx = useReact_useContext_37FA55CF(UserData);
    const pageCtx = useReact_useContext_37FA55CF(Pages);
    return createElement("button", createObj(ofArray([["className", "btn btn-primary"], (elems = toList(delay(() => (loading ? singleton(createElement("div", {
        className: "loading loading-spinner",
        style: {
            width: 20,
            height: 20,
        },
    })) : singleton("Load mock data")))), ["children", Interop_reactApi.Children.toArray(Array.from(elems))]), ["onClick", (_arg) => {
        let pr_1;
        const pr = PromiseBuilder__Run_212F1D4B(promise, PromiseBuilder__Delay_62FBFDE1(promise, () => {
            setLoading(true);
            return (new Promise(resolve => setTimeout(resolve, 1000))).then(() => {
                dataCtx.setData(UserData_1);
                setLoading(false);
                pageCtx.setData(new Pages_1(1, []));
                return Promise.resolve();
            });
        }));
        pr_1 = (pr.catch((ex) => {
            console.error(some(ex));
            setLoading(false);
        }));
        void pr_1;
    }]])));
}

