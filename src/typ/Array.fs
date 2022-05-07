[<AutoOpen>]
module fsharper.typ.Array

open System
open fsharper.op.Alias

[<AutoOpen>]
module ext =

    type 'a ``[]`` with

        member self.toList() = self |> List.ofArray

[<AutoOpen>]
module fn =
    let inline reverseArray array = Array.Reverse array

    let inline get (index: u32) (arr: ^t []) = arr.[i32 index]
