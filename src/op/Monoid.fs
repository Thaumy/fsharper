[<AutoOpen>]
module fsharper.op.Monoid

let inline mempty< ^m when ^m: (static member mempty : unit -> ^m)> =
    (^m: (static member mempty : unit -> ^m) ())
