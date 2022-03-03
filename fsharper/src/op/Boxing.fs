[<AutoOpen>]
module fsharper.op.Boxing

let inline unwarp m = (^m: (member unwarp : unit -> ^v) m)

let inline unwarpOr m value =
    (^m: (member unwarpOr : ^v -> ^v) m, value)

let inline flatten m = m >>= id
