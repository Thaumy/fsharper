[<AutoOpen>]
module fsharper.types.List'

open fsharper.typeExt
open fsharper.types


let rec internal map f list =
    match list with
    | x :: xs -> (f x) :: map f xs
    | [] -> []

let rec internal foldr f acc list =
    match list with
    | x :: xs -> f x (foldr f acc xs)
    | [] -> acc

let rec internal foldl f acc list =
    foldr (fun x g acc' -> g (f acc' x)) id list acc

let inline internal concat list = foldr (@) [] list

type List'<'a>(init: 'a list) =
    new() = List' []
    member private self.list: 'a list = init

type List'<'a> with
    //Functor
    member self.fmap(f: 'a -> 'b) = map f self.list |> List'

    //Applicative
    static member ap(ma: List'<'x -> 'y>, mb: List'<'x>) =
        let rec ap lfs lxs =
            match lfs, lxs with
            | [], _ -> []
            | _, [] -> []
            | f :: fs, x :: xs -> (f x) :: ap fs xs

        ap ma.list mb.list |> List'

    static member inline ``pure`` x = List' [ x ]

    //Monad
    member self.bind(f: 'a -> 'b List') : 'b List' =
        let f' x = (f x).list

        map f' self.list |> concat |> List'

    static member inline unit x = List'<_>.``pure`` x

type List'<'a> with
    //Semigroup
    member self.mappend(mb: List'<'a>) = (self.list @ mb.list) |> List'

    //Monoid
    static member mempty() = list<'a>.Empty |> List'

type List'<'a> with
    //Foldable
    member self.foldMap f = map f self.list |> concat
//member self.foldr(f, acc) = foldr f acc self.list

type List'<'a> with
    //Boxing
    static member inline warp x = List'<_>.``pure`` x

    member self.unwarp() = self.list

    member self.debug() =
        let f acc x =
            //下一级调试信息
            let msg =
                try
                    x.tryInvoke "debug"
                with
                | _ -> x.ToString()

            $"{acc}; {msg}"

        let result = foldl f "" self.list

        //去除首部分号
        $"[{result.Remove(0, 1)} ]"
