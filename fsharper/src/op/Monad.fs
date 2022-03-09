[<AutoOpen>]
module fsharper.op.Monad


let inline private runBind m f =
    (^ma: (member bind : (^v -> ^mb) -> ^mb) m, f)

let inline unit x = (^m: (static member unit : ^x -> ^m) x)

let inline bind m f = runBind m f

/// bind
let inline (>>=) m f = bind m f
