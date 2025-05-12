import { Record } from "../fable_modules/fable-library-js.4.19.0/Types.js";
import { class_type, record_type, list_type, string_type, option_type, int32_type } from "../fable_modules/fable-library-js.4.19.0/Reflection.js";
import { filter, cons, contains, isEmpty, ofArray, empty } from "../fable_modules/fable-library-js.4.19.0/List.js";
import { createElement } from "react";
import React from "react";
import * as react from "react";
import { Basic_FieldSet_Z67080547 } from "./Basic.js";
import { stringHash, defaultOf, equals, createObj } from "../fable_modules/fable-library-js.4.19.0/Util.js";
import { append, map, singleton, collect, delay, toList } from "../fable_modules/fable-library-js.4.19.0/Seq.js";
import { rangeDouble } from "../fable_modules/fable-library-js.4.19.0/Range.js";
import { defaultOf as defaultOf_1 } from "../fable_modules/fable-library-js.4.19.0/Util.js";
import { Interop_reactApi } from "../fable_modules/Feliz.2.6.0/Interop.fs.js";
import { equalsWith, truncate, item } from "../fable_modules/fable-library-js.4.19.0/Array.js";
import { MapFile__get_ColumnNames } from "../Types.js";
import { console_log_4E60E31B } from "../Extensions.js";
import { useReact_useMemo_10C6A43C, useReact_useState_FCFD9EF, useReact_useContext_37FA55CF } from "../fable_modules/Feliz.2.6.0/React.fs.js";
import { MapFile, UserData } from "../ReactContext.js";

class State extends Record {
    constructor(SelectedColumn, SelectedColumnType, TransformColumns) {
        super();
        this.SelectedColumn = SelectedColumn;
        this.SelectedColumnType = SelectedColumnType;
        this.TransformColumns = TransformColumns;
    }
}

function State_$reflection() {
    return record_type("App.State", [], State, () => [["SelectedColumn", option_type(int32_type)], ["SelectedColumnType", option_type(string_type)], ["TransformColumns", list_type(string_type)]]);
}

function State_init() {
    return new State(undefined, undefined, empty());
}

export class TransformData {
    constructor() {
    }
}

export function TransformData_$reflection() {
    return class_type("App.TransformData", undefined, TransformData);
}

function TransformData_UserDataTable_7C456071(userData, state, setState) {
    let elems_1, elems, children, children_4;
    return createElement(Basic_FieldSet_Z67080547, {
        legend: "User Data",
        isActive: state.SelectedColumn != null,
        childpairs: [["Select ID column", createElement("div", createObj(ofArray([["className", "overflow-auto w-full border-2 border-base-300 rounded bg-base-100"], (elems_1 = [createElement("table", createObj(ofArray([["className", "table cursor-pointer min-w-max"], ["onClick", (e) => {
            const matchValue = ((value_4) => value_4)(e.target.dataset.columnid);
            if (equals(matchValue, defaultOf())) {
            }
            else {
                const cid = matchValue;
                const cid_1 = cid | 0;
                if (equals(state.SelectedColumn, cid_1)) {
                    setState(new State(undefined, state.SelectedColumnType, state.TransformColumns));
                }
                else {
                    setState(new State(cid_1, state.SelectedColumnType, state.TransformColumns));
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
        }))], ["children", Interop_reactApi.Children.toArray(Array.from(elems))])])))], ["children", Interop_reactApi.Children.toArray(Array.from(elems_1))])])))]],
    });
}

function TransformData_InputConfig_Z5C406B66(mapFile, state, setState) {
    let elems;
    const names = MapFile__get_ColumnNames(mapFile);
    return createElement(Basic_FieldSet_Z67080547, {
        legend: "Input Config",
        isActive: state.SelectedColumnType != null,
        childpairs: [["Select id column type", createElement("select", createObj(ofArray([["className", "select"], ["title", "Select id column type"], ["defaultValue", "none"], ["onChange", (ev) => {
            const e = ev.target.value;
            console_log_4E60E31B(e);
            if (e === "none") {
                setState(new State(state.SelectedColumn, undefined, state.TransformColumns));
            }
            else {
                setState(new State(state.SelectedColumn, e, state.TransformColumns));
            }
        }], (elems = toList(delay(() => append(singleton(createElement("option", {
            value: "none",
            disabled: true,
            children: "Select id column type",
        })), delay(() => map((name_2) => createElement("option", {
            value: name_2,
            children: name_2,
        }), names))))), ["children", Interop_reactApi.Children.toArray(Array.from(elems))])])))]],
    });
}

function TransformData_TransformConfig_Z5C406B66(transformData_TransformConfig_Z5C406B66InputProps) {
    let xs_2;
    const setState = transformData_TransformConfig_Z5C406B66InputProps.setState;
    const state = transformData_TransformConfig_Z5C406B66InputProps.state;
    const mapFile = transformData_TransformConfig_Z5C406B66InputProps.mapFile;
    const names = MapFile__get_ColumnNames(mapFile);
    return createElement(Basic_FieldSet_Z67080547, {
        legend: "Transform Config",
        isActive: !isEmpty(state.TransformColumns),
        childpairs: [["Select annotation types", (xs_2 = toList(delay(() => map((name) => {
            let elems;
            return createElement("label", createObj(ofArray([["className", "label select-none"], (elems = [createElement("input", {
                type: "checkbox",
                className: "checkbox",
                checked: contains(name, state.TransformColumns, {
                    Equals: (x, y) => (x === y),
                    GetHashCode: stringHash,
                }),
                onChange: (ev) => {
                    if (ev.target.checked) {
                        setState(new State(state.SelectedColumn, state.SelectedColumnType, cons(name, state.TransformColumns)));
                    }
                    else {
                        setState(new State(state.SelectedColumn, state.SelectedColumnType, filter((y_1) => (name !== y_1), state.TransformColumns)));
                    }
                },
            }), name], ["children", Interop_reactApi.Children.toArray(Array.from(elems))])])));
        }, names))), react.createElement(react.Fragment, {}, ...xs_2))]],
    });
}

function TransformData_Confirm_Z7D869076(transformData_Confirm_Z7D869076InputProps) {
    let elems_2, elems_1, children;
    const setState = transformData_Confirm_Z7D869076InputProps.setState;
    const state = transformData_Confirm_Z7D869076InputProps.state;
    let disabledMsgs;
    const ra = [];
    if (state.SelectedColumn == null) {
        void (ra.push("Select ID column in your data!"));
    }
    if (state.SelectedColumnType == null) {
        void (ra.push("Select id column type!"));
    }
    if (isEmpty(state.TransformColumns)) {
        void (ra.push("Select annotation types!"));
    }
    disabledMsgs = ra.slice();
    const isDisabled = disabledMsgs.length > 0;
    return createElement("div", createObj(ofArray([["className", "mx-auto h-min flex flex-col justify-center shrink"], (elems_2 = [createElement("button", {
        className: "btn btn-wide btn-error",
        onClick: (_arg) => {
            console_log_4E60E31B("Confirmed");
        },
        disabled: isDisabled,
        children: "Confirm",
    }), createElement("div", createObj(ofArray([["className", ""], (elems_1 = [(children = toList(delay(() => map((msg) => {
        let elems;
        return createElement("li", createObj(ofArray([["className", "text-error"], (elems = [createElement("small", {
            children: [msg],
        })], ["children", Interop_reactApi.Children.toArray(Array.from(elems))])])));
    }, disabledMsgs))), createElement("ul", {
        children: Interop_reactApi.Children.toArray(Array.from(children)),
    }))], ["children", Interop_reactApi.Children.toArray(Array.from(elems_1))])])))], ["children", Interop_reactApi.Children.toArray(Array.from(elems_2))])])));
}

export function TransformData_Main() {
    let elems_2, elems_1, elems;
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
        return createElement("div", createObj(ofArray([["className", "flex lg:flex-row flex-col min-h-0 gap-2"], (elems_2 = [TransformData_UserDataTable_7C456071(userData_1, state, setState), createElement("div", createObj(ofArray([["className", "grow-0 flex flex-col gap-2"], (elems_1 = [TransformData_InputConfig_Z5C406B66(mapFile, state, setState), createElement(TransformData_TransformConfig_Z5C406B66, {
            mapFile: mapFile,
            state: state,
            setState: setState,
        }), createElement("div", createObj(ofArray([["className", "self-end flex grow items-end"], (elems = [createElement(TransformData_Confirm_Z7D869076, {
            state: state,
            setState: setState,
        })], ["children", Interop_reactApi.Children.toArray(Array.from(elems))])])))], ["children", Interop_reactApi.Children.toArray(Array.from(elems_1))])])))], ["children", Interop_reactApi.Children.toArray(Array.from(elems_2))])])));
    }
}

