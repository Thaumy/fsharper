[<AutoOpen>]
module fsharper.op.Monad

/// bind
let inline (>>=) m f =
    (^ma: (member bind : (^v -> ^mb) -> ^mb) m, f)
