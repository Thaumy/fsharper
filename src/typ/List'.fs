[<AutoOpen>]
module fsharper.typ.List'

open System.Collections
open fsharper.op
open fsharper.op.Reflection

//由于RFC FS-1043尚未完成，为List扩展方法的约束不得不通过包装类完成
//但貌似可以通过辅助函数补充+静态约束的方式来隐藏这一问题

type List'<'a>(init: 'a list) =
    new() = List' []

    member self.list: 'a list = init

type List'<'a> with
    //Functor
    member self.fmap(f: 'a -> 'b) =
        let rec map f list =
            match list with
            | x :: xs -> (f x) :: map f xs
            | [] -> []

        map f self.list |> List'

    //Applicative
    static member ap(ma: ('x -> 'y) List', mb: 'x List') =
        let rec ap lfs lxs =
            match lfs, lxs with
            | [], _ -> []
            | _, [] -> []
            | f :: fs, x :: xs -> (f x) :: ap fs xs

        ap ma.list mb.list |> List'

    static member inline ``pure`` x = List' [ x ]

type List'<'a> with
    //Semigroup
    member self.mappend(mb: List'<'a>) = (self.list @ mb.list) |> List'

    //Monoid
    static member mempty() = list<'a>.Empty |> List'

type List'<'a> with
    //Foldable

    member inline self.foldr(f, acc: 'acc) =
        let rec foldr f acc list =
            match list with
            | x :: xs -> f x (foldr f acc xs)
            | [] -> acc

        foldr f acc self.list

    member inline self.foldl(f, acc: 'acc) =
        let f' = fun x g -> fun acc' -> g (f acc' x)
        self.foldr (f', id) acc

    member inline self.foldMap f =
        self.foldr ((fun x (acc: ^acc) -> mappend (f x) acc), mempty)

type List'<'a> with

    //Monad
    member self.bind(f: 'a -> 'b List') : 'b List' = self.foldMap f

    static member inline unit x = List'<_>.``pure`` x

type List'<'a> with
    //Boxing
    static member inline wrap x = List'<_>.``pure`` x

    member self.unwrap() = self.list

    member self.debug() =
        let f acc x =
            //下一级调试信息
            let msg =
                try
                    x.tryInvoke "debug"
                with
                | _ -> x.ToString()

            $"{acc}; {msg}"

        let result = self.foldl (f, "")

        //去除首部分号
        $"[{result.Remove(0, 1)} ]"

type List' = List<obj>
