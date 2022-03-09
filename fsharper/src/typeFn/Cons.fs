[<AutoOpen>]
module rec fsharper.fn.Cons

open fsharper.types.Cons

exception ConsIsNil


let car c =
    match c with
    | Cons (x, _) -> x
    | Nil -> raise ConsIsNil

let cdr c =
    match c with
    | Cons (_, x) -> x
    | Nil -> raise ConsIsNil

let cadr x = car <| cdr x

let cdar x = cdr <| car x
