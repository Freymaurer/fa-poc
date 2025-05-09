import { createRoot } from "react-dom/client";
import { createElement } from "react";
import { View_Main } from "./View.js";
import "../tailwind.css";


export const root = createRoot(document.getElementById("feliz-app"));

root.render(createElement(View_Main, null));

