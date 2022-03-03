[<AutoOpen>]
module fsharper.typeExt.List

open System
open fsharper.op

type List<'T> with

    (*member self.loopOn<'a when 'a :> 'T>(f: 'a -> 'T) =
        let rec map f list =
            match list with
            | x :: xs -> (f x) :: map f xs
            | [] -> []

        let apply (it: 'T) : 'T =
            match it with
            | :? 'a as a -> f a
            | _ -> it

        map apply self*)
