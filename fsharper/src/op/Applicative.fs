[<AutoOpen>]
module fsharper.op.Applicative

/// ap
let inline (<*>) ma mb =
    (^ma: (static member ap : ^ma -> ^mb -> ^mc) ma, mb)
