import { class_type } from "./fable_modules/fable-library-js.4.19.0/Reflection.js";
import { createElement } from "react";
import React from "react";
import { useReact_useMemo_10C6A43C, useReact_useContext_37FA55CF, useFeliz_React__React_useState_Static_1505 } from "./fable_modules/Feliz.2.6.0/React.fs.js";
import { MapFile, UserData } from "./ReactContext.js";
import { equals, defaultOf, createObj } from "./fable_modules/fable-library-js.4.19.0/Util.js";
import { map, collect, singleton, delay, toList } from "./fable_modules/fable-library-js.4.19.0/Seq.js";
import { Interop_reactApi } from "./fable_modules/Feliz.2.6.0/Interop.fs.js";
import { PromiseBuilder__Delay_62FBFDE1, PromiseBuilder__Run_212F1D4B } from "./fable_modules/Fable.Promise.3.2.0/Promise.fs.js";
import { promise } from "./fable_modules/Fable.Promise.3.2.0/PromiseImpl.fs.js";
import { UserData as UserData_1 } from "./MockData.js";
import { some } from "./fable_modules/fable-library-js.4.19.0/Option.js";
import { ofArray } from "./fable_modules/fable-library-js.4.19.0/List.js";
import { item, equalsWith, truncate } from "./fable_modules/fable-library-js.4.19.0/Array.js";
import { defaultOf as defaultOf_1 } from "./fable_modules/fable-library-js.4.19.0/Util.js";
import { rangeDouble } from "./fable_modules/fable-library-js.4.19.0/Range.js";

export class Components {
    constructor() {
    }
}

export function Components_$reflection() {
    return class_type("App.Components", undefined, Components);
}

export function Components_LoadData() {
    let elems;
    const patternInput = useFeliz_React__React_useState_Static_1505(false);
    const setLoading = patternInput[1];
    const loading = patternInput[0];
    const dataCtx = useReact_useContext_37FA55CF(UserData);
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

export function Components_SelectIdColFromUserData() {
    let elems_2, elems_1;
    const userData = useReact_useContext_37FA55CF(UserData);
    const mapFile = useReact_useContext_37FA55CF(MapFile);
    const patternInput = useFeliz_React__React_useState_Static_1505(undefined);
    const setSelectedColumn = patternInput[1];
    const selectedColumn = patternInput[0];
    const truncatedData = useReact_useMemo_10C6A43C(() => {
        const matchValue = userData.data;
        if (matchValue == null) {
            return [];
        }
        else {
            const data = matchValue;
            return truncate(100, data.Data);
        }
    }, [userData.data, mapFile]);
    return createElement("div", createObj(ofArray([["className", "shadow-lg bg-base-100 rounded-lg p-8 m-8"], (elems_2 = [createElement("div", createObj(ofArray([["className", "overflow-auto max-h-[600px]"], (elems_1 = toList(delay(() => {
        let elems, children, children_4;
        if (!equalsWith((x, y) => equalsWith((x_1, y_1) => (x_1 === y_1), x, y), truncatedData, defaultOf()) && (truncatedData.length === 0)) {
            return singleton(createElement("h1", {
                children: ["No data loaded"],
            }));
        }
        else {
            const userData_1 = truncatedData;
            return singleton(createElement("table", createObj(ofArray([["className", "table cursor-pointer"], ["onClick", (e) => {
                const matchValue_1 = ((value_7) => value_7)(e.target.dataset.columnid);
                if (equals(matchValue_1, defaultOf())) {
                }
                else {
                    const cid = matchValue_1;
                    const cid_1 = cid | 0;
                    if (equals(selectedColumn, cid_1)) {
                        setSelectedColumn(undefined);
                    }
                    else {
                        setSelectedColumn(cid_1);
                    }
                }
            }], (elems = [(children = toList(delay(() => {
                if (selectedColumn == null) {
                    return singleton(defaultOf_1());
                }
                else {
                    const cid_2 = selectedColumn | 0;
                    return collect((i) => ((i === cid_2) ? singleton(createElement("col", {
                        className: "bg-base-200",
                    })) : singleton(createElement("col", {}))), rangeDouble(0, 1, userData_1.length - 1));
                }
            })), createElement("colgroup", {
                children: Interop_reactApi.Children.toArray(Array.from(children)),
            })), (children_4 = toList(delay(() => map((row) => {
                const children_2 = toList(delay(() => collect((cid_3) => {
                    const cell = item(cid_3, row);
                    return singleton(createElement("td", {
                        children: cell,
                        "data-columnid": cid_3,
                    }));
                }, rangeDouble(0, 1, row.length - 1))));
                return createElement("tr", {
                    children: Interop_reactApi.Children.toArray(Array.from(children_2)),
                });
            }, userData_1))), createElement("tbody", {
                children: Interop_reactApi.Children.toArray(Array.from(children_4)),
            }))], ["children", Interop_reactApi.Children.toArray(Array.from(elems))])]))));
        }
    })), ["children", Interop_reactApi.Children.toArray(Array.from(elems_1))])])))], ["children", Interop_reactApi.Children.toArray(Array.from(elems_2))])])));
}

