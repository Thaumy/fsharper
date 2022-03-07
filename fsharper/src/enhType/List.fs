[<AutoOpen>]
module fsharper.enhType.enhList

open fsharper.typeExt

let rec private map f list =
    match list with
    | x :: xs -> (f x) :: map f xs
    | [] -> []

let rec foldl f acc list =
    match list with
    | x :: xs -> foldl f (f acc x) xs
    | [] -> acc

let rec private foldr f acc list =
    match list with
    | x :: xs -> f x (foldr f acc xs)
    | [] -> acc

let inline private concat list = foldr (@) [] list

type List'<'a>(init: 'a list) =
    new() = List' []

    member private self.list: 'a list = init

    member self.fmap(f: 'a -> 'b) = map f self.list |> List'

    static member ap(ma: List'<'a -> 'b>, mb: List'<'a>) =
        let rec ap lfs lxs =
            match lfs, lxs with
            | [], _ -> []
            | _, [] -> []
            | f :: fs, x :: xs -> (f x) :: ap fs xs

        ap ma.list mb.list |> List'

    member self.bind(f: 'a -> 'b List') : 'b List' =
        let f' x = (f x).list

        map f' self.list |> concat |> List'

    static member inline warp x = List' [ x ]

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
