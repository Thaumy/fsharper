﻿[<AutoOpen>]
module fsharper.fn.List

open fsharper.op.Casting
open fsharper.types


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

let rec map = List'.map

let rec mapOn<'a, 't> (f: 't -> 'a) (list: 'a list) =
    match list with
    | x :: xs when is<'t> x -> (cast x |> f) :: mapOn<'a, 't> f xs
    | x :: xs -> x :: mapOn<'a, 't> f xs
    | [] -> []

let rec filter p list =
    match list with
    | x :: xs when p x -> x :: filter p xs
    | _ :: xs -> filter p xs
    | [] -> []

let rec take n list =
    match list with
    | _ when n <= 0 -> []
    | [] -> []
    | x :: xs -> x :: take (n - 1) xs

let rec foldr = List'.foldr

let rec foldl = List'.foldl

let inline any p list =
    foldl (fun acc it -> p it || acc) false list

let inline elem x list = any (eq x) list

let concat = List'.concat

let flatMap f list = map f list |> concat

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
