[<AutoOpen>]
module fsharper.types.Array

open System

[<AutoOpen>]
module ext =

    type 'a ``[]`` with

        member self.toList() = self |> List.ofArray

[<AutoOpen>]
module fn =
    let inline reverseArray array = Array.Reverse array

    let inline get (index: uint) (arr: ^t []) = arr.[int index]
