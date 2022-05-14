[<AutoOpen>]
module fsharper.typ.String

open System
open fsharper.op.Alias

[<AutoOpen>]
module ext =

    type String with

        member self.withoutLast = self.[0..^1] 
