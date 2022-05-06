[<AutoOpen>]
module fsharper.typ.Object

[<AutoOpen>]
module ext =
    open System
    open fsharper.op.Coerce

    type Object with
        member self.whenNull f = if self = null then f else self
