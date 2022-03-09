[<AutoOpen>]
module fsharper.op.Semigroup

let inline mappend ma mb =
    (^m: (static member mappend : ^m -> ^m -> ^m) ma, mb)
