[<AutoOpen>]
module fsharper.op.Monad

/// bind
let inline (>>=) m f =
    (^ma: (member bind : (^v -> ^mb) -> ^mb) m, f)

/// bind but no return
let inline (>>=|) m f = m >>= f |> ignore

