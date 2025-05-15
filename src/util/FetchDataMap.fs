module App.FetchHelper

open Fable.Core
open Fetch
open ARCtrl

let fetchDataMap (url: string) =
    promise {
        let! response =
            fetch url [ ]
        let! stream = response.arrayBuffer()
        let bytes = Fable.Core.JS.Constructors.Uint8Array.Create(stream)
        let! fswb = FsSpreadsheet.Js.Xlsx.fromXlsxBytes (unbox bytes)
        let datamap = ARCtrl.XlsxController.Datamap.fromFsWorkbook fswb
        return datamap
    }