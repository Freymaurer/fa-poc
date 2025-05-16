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

[<Erase; Global>]
type IDFD =
    member this.readCSV (url:string, ?options: obj) : JS.Promise<IDataFrame> = jsNative

    member this.toCSV (df: IDataFrame, ?options: obj) : JS.Promise<unit> = jsNative

    [<Emit("new $0.DataFrame($1,$2)")>]
    member this.DataFrame(data: string [] [], ?options: obj) : IDataFrame = jsNative

    [<ParamObject>]
    member this.merge(left: IDataFrame, right: IDataFrame, on: string [], ?how: string) : IDataFrame = jsNative

type Danfojs =

    [<ImportAll("danfojs")>]
    static member dfd: IDFD = jsNative