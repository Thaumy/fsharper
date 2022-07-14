[<AutoOpen>]
module fsharper.typ.Tuple

open System.Runtime.CompilerServices

[<AutoOpen>]
module fn =

    let inline fst3 (x, _, _) = x

    let inline snd3 (_, x, _) = x

    let inline trd3 (_, _, x) = x

[<Extension>]
type ext() =
    [<Extension>]
    static member inline fst((x, _)) = x

    [<Extension>]
    static member inline snd((_, y)) = y
