[<AutoOpen>]
module fsharper.fn.Tuple

let car (x, _) = x

let cdr (_, x) = x

let inline fst3 (x, _, _) = x

let inline snd3 (_, x, _) = x

let inline trd3 (_, _, x) = x
