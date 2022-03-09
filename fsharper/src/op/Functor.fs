[<AutoOpen>]
module fsharper.op.Functor


let inline private runFmap f fa = (^fa: (member fmap : ^f -> ^fb) fa, f)

let inline fmap f fa = runFmap f fa

/// fmap
let inline ``<$>`` f fa = fmap f fa
