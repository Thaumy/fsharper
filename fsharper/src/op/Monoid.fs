[<AutoOpen>]
module fsharper.op.Monoid

let inline mempty< ^m when ^m: (static member mempty : ^m)> = (^m: (static member mempty : ^m) ())

