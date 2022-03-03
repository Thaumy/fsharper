﻿[<AutoOpen>]
module fsharper.fn.List

open fsharper.ethType

let rec last list =
    match list with
    | [] -> None
    | [ x ] -> Some x
    | _ :: xs -> last xs

let rec map f list =
    match list with
    | x :: xs -> (f x) :: map f xs
    | [] -> []

let rec filter p list =
    match list with
    | x :: xs ->
        if p x then
            x :: filter p xs
        else
            filter p xs
    | [] -> []

let inline head list =
    match list with
    | x :: _ -> Some x
    | _ -> None

let inline tail list =
    match list with
    | _ :: xs -> Some xs
    | _ -> None

let rec take n list =
    match list with
    | _ when n <= 0 -> []
    | [] -> []
    | x :: xs -> x :: take (n - 1) xs

let rec foldl f acc list =
    match list with
    | x :: xs -> foldl f (f acc x) xs
    | [] -> acc

let rec foldr f acc list =
    match list with
    | x :: xs -> f x (foldr f acc xs)
    | [] -> acc

let inline elem e list =
    foldl (fun acc y -> y = e || acc) false list

let inline concat list = foldr (@) [] list