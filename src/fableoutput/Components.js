import { class_type } from "./fable_modules/fable-library-js.4.19.0/Reflection.js";
import { createElement } from "react";
import React from "react";
import { useReact_useMemo_10C6A43C, useReact_useContext_37FA55CF, useFeliz_React__React_useState_Static_1505 } from "./fable_modules/Feliz.2.6.0/React.fs.js";
import { MapFile, UserData } from "./ReactContext.js";
import { defaultOf, equals, createObj } from "./fable_modules/fable-library-js.4.19.0/Util.js";
import { append, map, collect, singleton, delay, toList } from "./fable_modules/fable-library-js.4.19.0/Seq.js";
import { Interop_reactApi } from "./fable_modules/Feliz.2.6.0/Interop.fs.js";
import { PromiseBuilder__Delay_62FBFDE1, PromiseBuilder__Run_212F1D4B } from "./fable_modules/Fable.Promise.3.2.0/Promise.fs.js";
import { promise } from "./fable_modules/Fable.Promise.3.2.0/PromiseImpl.fs.js";
import { UserData as UserData_1 } from "./MockData.js";
import { some } from "./fable_modules/fable-library-js.4.19.0/Option.js";
import { singleton as singleton_1, ofArray } from "./fable_modules/fable-library-js.4.19.0/List.js";
import { defaultOf as defaultOf_1 } from "./fable_modules/fable-library-js.4.19.0/Util.js";
import { rangeDouble } from "./fable_modules/fable-library-js.4.19.0/Range.js";
import { equalsWith, truncate, item } from "./fable_modules/fable-library-js.4.19.0/Array.js";
import { MapFile__get_ColumnNames } from "./Types.js";
import { join } from "./fable_modules/fable-library-js.4.19.0/String.js";
import { console_log_4E60E31B } from "./Extensions.js";

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

export function Components_UserDataTable_19BA8CB1(userData, selectedColumn, setSelectedColumn) {
    let elems_1, elems, children, children_4;
    return createElement("div", createObj(ofArray([["className", "overflow-auto max-h-[600px] max-w-[600px]"], (elems_1 = [createElement("table", createObj(ofArray([["className", "table cursor-pointer"], ["onClick", (e) => {
        const matchValue = ((value_4) => value_4)(e.target.dataset.columnid);
        if (equals(matchValue, defaultOf())) {
        }
        else {
            const cid = matchValue;
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
            })) : singleton(createElement("col", {}))), rangeDouble(0, 1, userData.length - 1));
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
    }, userData))), createElement("tbody", {
        children: Interop_reactApi.Children.toArray(Array.from(children_4)),
    }))], ["children", Interop_reactApi.Children.toArray(Array.from(elems))])])))], ["children", Interop_reactApi.Children.toArray(Array.from(elems_1))])])));
}

export function Components_MapFileColNameDropdown_Z737135FF(mapFile, setSelected) {
    let elems_1, elems;
    const names = MapFile__get_ColumnNames(mapFile);
    return createElement("div", createObj(ofArray([["className", "dropdown"], (elems_1 = [createElement("div", {
        className: "btn m-1",
        tabIndex: 0,
        role: join(" ", ["button"]),
        children: "Select data id type",
    }), createElement("ul", createObj(ofArray([["tabIndex", 0], ["className", "dropdown-content menu p-2 shadow bg-base-300 rounded-box w-52"], (elems = toList(delay(() => map((name_2) => {
        const children = singleton_1(createElement("a", {
            onClick: (e) => {
                e.preventDefault();
                setSelected(name_2);
            },
            children: name_2,
        }));
        return createElement("li", {
            children: Interop_reactApi.Children.toArray(Array.from(children)),
        });
    }, names))), ["children", Interop_reactApi.Children.toArray(Array.from(elems))])])))], ["children", Interop_reactApi.Children.toArray(Array.from(elems_1))])])));
}

export function Components_SelectIdColFromUserData() {
    let elems_2;
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
    return createElement("div", createObj(ofArray([["className", "shadow-lg bg-base-100 rounded-lg p-8 m-8"], (elems_2 = toList(delay(() => append(singleton(createElement("div", {
        className: "prose w-full",
        children: "Select ID column from your data!",
    })), delay(() => {
        let elems_1, elems;
        const matchValue_1 = truncatedData;
        if (!equalsWith((x, y) => equalsWith((x_1, y_1) => (x_1 === y_1), x, y), matchValue_1, defaultOf()) && (matchValue_1.length === 0)) {
            return singleton(createElement("h1", {
                children: ["No data loaded"],
            }));
        }
        else {
            const userData_1 = matchValue_1;
            return singleton(createElement("div", createObj(ofArray([["className", "flex lg:flex-row flex-col-reverse gap-4"], (elems_1 = [Components_UserDataTable_19BA8CB1(userData_1, selectedColumn, setSelectedColumn), createElement("div", createObj(ofArray([["className", "grow-0"], (elems = [Components_MapFileColNameDropdown_Z737135FF(mapFile, (name_4) => {
                console_log_4E60E31B(name_4);
            })], ["children", Interop_reactApi.Children.toArray(Array.from(elems))])])))], ["children", Interop_reactApi.Children.toArray(Array.from(elems_1))])]))));
        }
    })))), ["children", Interop_reactApi.Children.toArray(Array.from(elems_2))])])));
}

