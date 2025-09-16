namespace App

open Fable.Core
open Fable.Core.JsInterop

type ISeries =
    abstract member values: obj []
    abstract member loc: obj -> ISeries
    abstract member map: obj -> obj
    abstract member print: unit -> unit

type IDataFrame =
    abstract member print: unit -> unit
    abstract member column: string -> ISeries
    abstract member column: int -> ISeries
    abstract member columns: string [] with get
    abstract member values: obj [] []
    abstract member loc: {|rows: obj; columns: obj|} -> IDataFrame
    abstract member loc: {|rows: obj|} -> IDataFrame
    abstract member loc: {|columns: obj|} -> IDataFrame
    [<Emit("$0[$1]")>]
    abstract member item: string -> ISeries
    [<Emit("$0.rename($1, { inplace: true })")>]
    abstract member rename: obj -> unit
    abstract member fillNa: obj -> IDataFrame

[<StringEnum(CaseRules.LowerFirst)>]
type MergeHow =
    | Left
    | Right
    | Outer
    | Inner

type IDFD =
    abstract member readCSV: string * ?options: obj -> JS.Promise<IDataFrame>
    abstract member toCSV: IDataFrame * ?options: obj -> JS.Promise<unit>
    [<Emit("new $0.DataFrame($1,$2)")>]
    abstract member DataFrame: string [] [] * ?options: obj -> IDataFrame
    [<ParamObject>]
    abstract member merge: left: IDataFrame * right: IDataFrame * on: string [] * ?how: MergeHow -> IDataFrame

[<Erase>]
type Danfojs =

    [<ImportAll("danfojs")>]
    static member dfd: IDFD = jsNative


module Test =
    let df() = Danfojs.dfd.DataFrame( [| [|"A"; "B"|]; [|"1"; "2"|]; [|"3"; "4"|]|])