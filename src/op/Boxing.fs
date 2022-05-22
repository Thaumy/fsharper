[<AutoOpen>]
module fsharper.op.Boxing


let inline wrap x = (^m: (static member wrap : ^x -> ^m) x)

let inline unwrap m = (^m: (member unwrap : unit -> ^v) m)
//TODO unwrapN并不优雅
let inline unwrap2 m = m |> unwrap |> unwrap
let inline unwrap3 m = m |> unwrap2 |> unwrap

let inline whenCanUnwrap m f =
    (^m: (member whenCanUnwrap : 'v -> unit) m, f)

let inline whenCanUnwrapOr m trueDo falseDo =
    (^m: (member whenCanUnwrapOr : ('v -> 'r) * (unit -> 'r) -> 'r) m, trueDo, falseDo)

let inline unwrapOr m value =
    (^m: (member unwrapOr : ^v -> ^v) m, value)

let inline flatten m = m >>= id
