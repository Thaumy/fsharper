[<AutoOpen>]
module fsharper.op.Functor

/// fmap
let inline (<%>) m f = (^ma: (member fmap : ^f -> ^mb) m, f)
