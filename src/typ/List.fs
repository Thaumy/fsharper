[<AutoOpen>]
module fsharper.typ.List

[<AutoOpen>]
module ext =

    type 'a List with

        member self.toArray() = self |> Array.ofList
        member self.toSeq() = self |> Seq.ofList

[<AutoOpen>]
module fn =

    open fsharper.op
    open fsharper.typ

    let inline head list =
        match list with
        | x :: _ -> Some x
        | _ -> None

    let inline tail list =
        match list with
        | _ :: xs -> Some xs
        | _ -> None

    let rec last list =
        match list with
        | [] -> None
        | [ x ] -> Some x
        | _ :: xs -> last xs

    let rec zip a b =
        match a, b with
        | [], _ -> []
        | _, [] -> []
        | x :: xs, y :: ys -> (x, y) :: zip xs ys

    let rec zipAll a b =
        match a, b with
        | [], y :: ys -> (None, Some y) :: zipAll [] ys
        | x :: xs, [] -> (Some x, None) :: zipAll xs []
        | x :: xs, y :: ys -> (Some x, Some y) :: zipAll xs ys
        | [], [] -> []

    let rec map f list = (List' list).fmap f |> unwrap

    let rec mapOn<'x, 't> (f: 't -> 'x) (list: 'x list) =
        match list with
        | x :: xs when x.is<'t> () -> (coerce x |> f) :: mapOn<'x, 't> f xs
        | x :: xs -> x :: mapOn<'x, 't> f xs
        | [] -> []

    let inline foldMap (f: 'a -> 'foldable) (list: 'a list) = Foldable.foldMap f (List' list)

    let inline foldr f (acc: 'acc) (list: 'a list) = Foldable.foldr f acc (List' list)

    let inline foldl f (acc: 'acc) (list: 'a list) = Foldable.foldl f acc (List' list)

    let rec take n list =
        match list with
        | _ when n <= 0 -> []
        | [] -> []
        | x :: xs -> x :: take (n - 1) xs

    let rec filter p list =
        let f = fun acc x -> if p x then x :: acc else acc

        (List' list).foldl (f, [])

    let rec filterOnce p list =
        match list with
        | x :: xs -> if p x then Some x else filterOnce p xs
        | [] -> None

    let inline any p list =
        foldl (fun acc it -> p it || acc) false list

    let inline none p list = any p list |> not

    let rec find p list =
        match list with
        | x :: xs -> if p x then Some x else find p xs
        | [] -> None

    let inline elem x list = any (eq x) list

    let inline concat list = foldr (@) [] list

    let inline flatMap f list =
        let f' x = x |> f |> List'
        (List' list).foldMap f' |> unwrap

    let inline leftJoinNoInnerWhen p ls rs =
        filter (fun l -> not <| any (p l) rs) ls

    let inline rightJoinNoInnerWhen p = leftJoinNoInnerWhen p |> flip

    let inline leftJoinNoInner ls rs = leftJoinNoInnerWhen eq ls rs

    let inline rightJoinNoInner ls rs = leftJoinNoInner rs ls

    let inline innerJoinWhen p ls rs = filter (fun l -> any (p l) rs) ls

    let inline innerJoin ls rs = innerJoinWhen eq ls rs

    let inline fullJoin ls rs = ls @ rs

    let rec duplicateWhen p list =
        match list with
        | x :: xs ->
            match x :: filter (p x) xs with
            | [ _ ] -> duplicateWhen p xs
            | ds ->
                ds
                @ duplicateWhen p (rightJoinNoInnerWhen p ds xs)
        | [] -> []

    let duplicate list = duplicateWhen eq list
