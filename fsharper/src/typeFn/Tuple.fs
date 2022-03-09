[<AutoOpen>]
module rec fsharper.fn.Tuple


let car = fst

let cdr = snd

let cadr x = car <| cdr x

let cdar x = cdr <| car x

let inline fst3 (x, _, _) = x

let inline snd3 (_, x, _) = x

let inline trd3 (_, _, x) = x
