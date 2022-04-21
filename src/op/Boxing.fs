[<AutoOpen>]
module fsharper.op.Boxing


let inline warp x = (^m: (static member warp : ^x -> ^m) x)

let inline unwarp m = (^m: (member unwarp : unit -> ^v) m)
//TODO unwarpN并不优雅
let inline unwarp2 m = m |> unwarp |> unwarp
let inline unwarp3 m = m |> unwarp |> unwarp |> unwarp

let inline unwarpOr m value =
    (^m: (member unwarpOr : ^v -> ^v) m, value)

let inline flatten m = m >>= id
