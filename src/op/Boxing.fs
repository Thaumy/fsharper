[<AutoOpen>]
module fsharper.op.Boxing

open System

let inline wrap x = (^m: (static member wrap: ^x -> ^m) x)

let inline unwrap m = (^m: (member unwrap: unit -> ^v) m)
//TODO unwrapN并不优雅
let inline unwrap2 m = m |> unwrap |> unwrap

let inline unwrapOr m f =
    (^m: (member unwrapOr: (^e -> ^v) -> ^v) m, f)

let inline unwrapOrPanic m e =
    (^m: (member unwrapOrPanic: Exception -> ^v) m, e)

let inline ifCanUnwrapOr m trueDo falseDo =
    (^m: (member ifCanUnwrapOr: ('v -> 'r) * (unit -> 'r) -> 'r) m, trueDo, falseDo)

let inline flatten m = m >>= id
