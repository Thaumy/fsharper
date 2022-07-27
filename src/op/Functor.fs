[<AutoOpen>]
module fsharper.op.Functor

let inline fmap f fa =
    (^fa: (member fmap : (^a -> ^b) -> ^fb) fa, f)

/// fmap
let inline (<%>) f fa = fmap f fa
