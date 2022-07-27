namespace fsharper.typ

module Cons =

    open System
    open fsharper.op.Reflect

    type Cons<'a> =
        | Nil
        | Cons of car: 'a * cdr: Cons<'a>

    let rec append ca cb =
        match ca, cb with
        | Nil, _ -> cb
        | _, Nil -> ca
        | Cons (x, xs), _ -> Cons(x, append xs cb)

    let rec foldl f acc cons =
        match cons with
        | Cons (x, xs) -> foldl f (f acc x) xs
        | Nil -> acc

    let rec foldr f acc cons =
        match cons with
        | Cons (x, xs) -> f x (foldr f acc xs)
        | Nil -> acc

    let inline concat cons = foldl append Nil cons

    type Cons<'a> with
        //Functor
        member self.fmap f =
            match self with
            | Cons (x, xs) -> Cons(f x, xs.fmap f)
            | Nil -> Nil


        //Applicative
        static member inline ap(ma: Cons<'x -> 'y>, mb: Cons<'x>) =
            let rec ap ma mb =
                match ma, mb with
                | Nil, _ -> Nil
                | _, Nil -> Nil
                | Cons (f, fs), Cons (x, xs) -> Cons(f x, ap fs xs)

            ap ma mb

        //Monad
        member inline self.bind f = self.fmap f |> concat

    type Cons<'a> with
        //Semigroup
        member self.mappend(mb: Cons<'a>) =
            match self, mb with
            | Nil, _ -> mb
            | _, Nil -> self
            | Cons (x, xs), _ -> Cons(x, xs.mappend mb)

        //Monoid
        static member mempty() = Nil

    type Cons<'t> with
        //Boxing
        static member inline wrap x = Cons(x, Nil)

        member self.unwrap() =
            match self with
            | Nil -> failwith "Try to unwrap Nil"
            | Cons (x, _) -> x


        member self.debug() =
            let f x acc =
                let msg =
                    try //下一级调试信息
                        x.tryInvoke "debug"
                    with
                    | _ -> x.ToString()

                $"{acc} {msg}"

            let result = foldl f "" self

            //去除首部空格
            $"({result.Remove(0, 1)})"

        static member list<'t>([<ParamArray>] arr: 't array) =
            let max = arr.Length - 1

            let rec withCount count =
                match count with
                | i when i = max -> Nil
                | i -> Cons(arr.[i], withCount (count + 1))

            withCount 0

    [<AutoOpen>]
    module fn =

        let car c =
            match c with
            | Cons (x, _) -> x
            | Nil -> failwith "Cons is Nil"

        let cdr c =
            match c with
            | Cons (_, x) -> x
            | Nil -> failwith "Cons is Nil"

        let cadr x = car <| cdr x

        let cdar x = cdr <| car x
