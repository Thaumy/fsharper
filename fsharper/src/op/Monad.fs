[<AutoOpen>]
module fsharper.op.Monad

/// flatMap
let inline (>>=) m f =
    (^ma: (member bind : (^v -> ^mb) -> ^mb) m, f)

/// flatMap但不返回值
let inline (>>=|) m f = m >>= f |> ignore
