[<AutoOpen>]
module fsharper.op.Monad


let inline bind m f =
    (^ma: (member bind : (^v -> ^mb) -> ^mb) m, f)

let inline unit x = (^m: (static member unit : ^x -> ^m) x)

/// bind
let inline (>>=) m f = bind m f

let inline (>>) m f = m >>= fun _ -> f
