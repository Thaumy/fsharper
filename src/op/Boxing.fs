[<AutoOpen>]
module fsharper.op.Boxing

open System

let inline wrap x = (^m: (static member wrap: ^x -> ^m) x)

let inline unwrap m = (^m: (member unwrap: unit -> ^v) m)
//TODO unwrapN并不优雅
let inline unwrap2 m = m |> unwrap |> unwrap

let inline unwrapOr m v = (^m: (member unwrapOr: ^v -> ^v) m, v)

let inline unwrapOrEval m f =
    (^m: (member unwrapOrEval: (^e -> ^v) -> ^v) m, f)

let inline unwrapOrPanic m e =
    (^m: (member unwrapOrPanic: Exception -> ^v) m, e)

let inline ifCanUnwrapOr m trueDo falseDo =
    (^m: (member ifCanUnwrapOr: ('v -> 'r) * (unit -> 'r) -> 'r) m, trueDo, falseDo)

let inline flatten m = m >>= id
