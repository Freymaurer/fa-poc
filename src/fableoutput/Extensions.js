import { class_type } from "./fable_modules/fable-library-js.4.19.0/Reflection.js";
import { some } from "./fable_modules/fable-library-js.4.19.0/Option.js";

export class console$ {
    constructor() {
    }
}

export function console$_$reflection() {
    return class_type("Extensions.console", undefined, console$);
}

export function console_log_4E60E31B(message) {
    console.log(some(message));
}

