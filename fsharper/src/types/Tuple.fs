[<AutoOpen>]
module fsharper.types.Tuple

[<AutoOpen>]
module fn =

    let inline fst3 (x, _, _) = x

    let inline snd3 (_, x, _) = x

    let inline trd3 (_, _, x) = x
