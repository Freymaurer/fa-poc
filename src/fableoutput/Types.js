import { Record, Union } from "./fable_modules/fable-library-js.4.19.0/Types.js";
import { record_type, array_type, string_type, union_type } from "./fable_modules/fable-library-js.4.19.0/Reflection.js";
import { contains, find, tryFindIndex, map, item } from "./fable_modules/fable-library-js.4.19.0/Array.js";
import { split } from "./fable_modules/fable-library-js.4.19.0/String.js";
import { stringHash } from "./fable_modules/fable-library-js.4.19.0/Util.js";

export class Pages extends Union {
    constructor(tag, fields) {
        super();
        this.tag = tag;
        this.fields = fields;
    }
    cases() {
        return ["LoadData", "SelectIdCol", "Annotation"];
    }
}

export function Pages_$reflection() {
    return union_type("App.Pages", [], Pages, () => [[], [], []]);
}

export class MapFileColumn extends Record {
    constructor(Name, Description, Values) {
        super();
        this.Name = Name;
        this.Description = Description;
        this.Values = Values;
    }
}

export function MapFileColumn_$reflection() {
    return record_type("App.MapFileColumn", [], MapFileColumn, () => [["Name", string_type], ["Description", string_type], ["Values", array_type(string_type)]]);
}

export class MapFile extends Record {
    constructor(Columns) {
        super();
        this.Columns = Columns;
    }
}

export function MapFile_$reflection() {
    return record_type("App.MapFile", [], MapFile, () => [["Columns", array_type(MapFileColumn_$reflection())]]);
}

/**
 * Length of all column values must be the same
 */
export function MapFile__get_IsValid(this$) {
    if (this$.Columns.length === 0) {
        return false;
    }
    else {
        const length = item(0, this$.Columns).Values.length | 0;
        return this$.Columns.every((c) => (c.Values.length === length));
    }
}

export function MapFile__get_ColumnNames(this$) {
    return map((c) => c.Name, this$.Columns);
}

export function MapFile__tryFindIndex_Z384F8060(this$, columnName, key) {
    return tryFindIndex((v) => (v === key), find((c) => (c.Name === columnName), this$.Columns).Values);
}

export function MapFile__getValuesByIndex_Z72EEBE71(this$, index, columnNames) {
    let columnNames_1;
    if (columnNames == null) {
        columnNames_1 = MapFile__get_ColumnNames(this$);
    }
    else {
        const names = columnNames;
        columnNames_1 = names;
    }
    return map((c_1) => ({
        name: c_1.Name,
        values: split(item(index, c_1.Values).trim(), ["; "], undefined, 0),
    }), this$.Columns.filter((c) => contains(c.Name, columnNames_1, {
        Equals: (x, y) => (x === y),
        GetHashCode: stringHash,
    })));
}

export class UserData extends Record {
    constructor(Data) {
        super();
        this.Data = Data;
    }
}

export function UserData_$reflection() {
    return record_type("App.UserData", [], UserData, () => [["Data", array_type(array_type(string_type))]]);
}

export function UserData_init() {
    return new UserData([]);
}

