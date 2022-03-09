[<AutoOpen>]
module rec fsharper.fn.Procedure


let inline flip f a b = f b a

/// aka const
let inline konst x _ = x
