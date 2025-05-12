import { Record } from "./fable_modules/fable-library-js.4.19.0/Types.js";
import { class_type, record_type, string_type, option_type, int32_type } from "./fable_modules/fable-library-js.4.19.0/Reflection.js";
import { createElement } from "react";
import React from "react";
import { useReact_useMemo_10C6A43C, useReact_useState_FCFD9EF, useReact_useContext_37FA55CF, useFeliz_React__React_useState_Static_1505 } from "./fable_modules/Feliz.2.6.0/React.fs.js";
import { MapFile, Pages, UserData } from "./ReactContext.js";
import { defaultOf, equals, createObj } from "./fable_modules/fable-library-js.4.19.0/Util.js";
import { append, map, collect, singleton, delay, toList } from "./fable_modules/fable-library-js.4.19.0/Seq.js";
import { Interop_reactApi } from "./fable_modules/Feliz.2.6.0/Interop.fs.js";
import { PromiseBuilder__Delay_62FBFDE1, PromiseBuilder__Run_212F1D4B } from "./fable_modules/Fable.Promise.3.2.0/Promise.fs.js";
import { promise } from "./fable_modules/Fable.Promise.3.2.0/PromiseImpl.fs.js";
import { UserData as UserData_1 } from "./MockData.js";
import { MapFile__get_ColumnNames, Pages as Pages_1 } from "./Types.js";
import { some } from "./fable_modules/fable-library-js.4.19.0/Option.js";
import { ofArray } from "./fable_modules/fable-library-js.4.19.0/List.js";
import { rangeDouble } from "./fable_modules/fable-library-js.4.19.0/Range.js";
import { defaultOf as defaultOf_1 } from "./fable_modules/fable-library-js.4.19.0/Util.js";
import { equalsWith, truncate, item } from "./fable_modules/fable-library-js.4.19.0/Array.js";

class State extends Record {
    constructor(SelectedColumn, SelectedColumnType) {
        super();
        this.SelectedColumn = SelectedColumn;
        this.SelectedColumnType = SelectedColumnType;
    }
}

function State_$reflection() {
    return record_type("App.State", [], State, () => [["SelectedColumn", option_type(int32_type)], ["SelectedColumnType", option_type(string_type)]]);
}

function State_init() {
    return new State(undefined, undefined);
}

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

function Components_UserDataTable_7C456071(userData, state, setState) {
    let elems_1, elems, children, children_4;
    return createElement("div", createObj(ofArray([["className", "overflow-auto w-full border-2 border-base-300 rounded"], (elems_1 = [createElement("table", createObj(ofArray([["className", "table cursor-pointer min-w-max"], ["onClick", (e) => {
        const matchValue = ((value_4) => value_4)(e.target.dataset.columnid);
        if (equals(matchValue, defaultOf())) {
        }
        else {
            const cid = matchValue;
            const cid_1 = cid | 0;
            if (equals(state.SelectedColumn, cid_1)) {
                setState(new State(undefined, state.SelectedColumnType));
            }
            else {
                setState(new State(cid_1, state.SelectedColumnType));
            }
        }
    }], (elems = [(children = toList(delay(() => {
        if (state.SelectedColumn != null) {
            const cid_2 = state.SelectedColumn | 0;
            return collect((i) => ((i === cid_2) ? singleton(createElement("col", {
                className: "bg-base-200",
            })) : singleton(createElement("col", {}))), rangeDouble(0, 1, userData.length - 1));
        }
        else {
            return singleton(defaultOf_1());
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

function Components_MapFileColNameSelect_Z5C406B66(mapFile, state, setState) {
    let elems;
    const names = MapFile__get_ColumnNames(mapFile);
    return createElement("select", createObj(ofArray([["className", "select"], ["title", "Select a column type"], (elems = toList(delay(() => append(singleton(createElement("option", {
        value: "none",
        disabled: true,
        selected: state.SelectedColumnType == null,
        children: "Select a column type",
    })), delay(() => map((name_2) => createElement("option", {
        value: name_2,
        selected: equals(state.SelectedColumnType, name_2),
        children: name_2,
    }), names))))), ["children", Interop_reactApi.Children.toArray(Array.from(elems))])])));
}

export function Components_SelectIdColFromUserData() {
    let elems_1, elems;
    const userData = useReact_useContext_37FA55CF(UserData);
    const mapFile = useReact_useContext_37FA55CF(MapFile);
    const patternInput = useReact_useState_FCFD9EF(State_init);
    const state = patternInput[0];
    const setState = patternInput[1];
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
    if (!equalsWith((x, y) => equalsWith((x_1, y_1) => (x_1 === y_1), x, y), truncatedData, defaultOf()) && (truncatedData.length === 0)) {
        return createElement("h1", {
            children: ["No data loaded"],
        });
    }
    else {
        const userData_1 = truncatedData;
        return createElement("div", createObj(ofArray([["className", "flex lg:flex-row flex-col-reverse min-h-0 gap-2"], (elems_1 = [Components_UserDataTable_7C456071(userData_1, state, setState), createElement("div", createObj(ofArray([["className", "grow-0"], (elems = [Components_MapFileColNameSelect_Z5C406B66(mapFile, state, setState)], ["children", Interop_reactApi.Children.toArray(Array.from(elems))])])))], ["children", Interop_reactApi.Children.toArray(Array.from(elems_1))])])));
    }
}

