[<AutoOpen>]
module fsharper.types.Procedure

[<AutoOpen>]
module fn =
    let inline flip f a b = f b a

    /// aka const
    let inline konst x _ = x
