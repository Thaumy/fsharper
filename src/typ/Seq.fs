[<AutoOpen>]
module fsharper.typ.Seq

open System
open System.Collections.Generic
open fsharper.alias

[<AutoOpen>]
module ext =

    type IEnumerable<'T> with

        member self.toList() = self |> List.ofSeq
        member self.toArray() = self |> Array.ofSeq
