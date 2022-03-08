[<AutoOpen>]
module fsharper.op.Monoid

let inline mempty< ^m when ^m: (static member mempty : ^m)> = (^m: (static member mempty : ^m) ())

let inline mappend ma mb =
    (^m: (static member mappend : ^m -> ^m -> ^m) ma, mb)
