[<AutoOpen>]
module fsharper.typ.Array

open System
open fsharper.alias

[<AutoOpen>]
module ext =

    type 'a ``[]`` with

        member self.toList() = self |> List.ofArray
        member self.toSeq() = self |> Seq.ofArray

[<AutoOpen>]
module fn =
    let inline reverseArray array = Array.Reverse array

    /// 未检测索引超出
    let inline get (index: u32) (arr: ^t []) = arr.[i32 index]
