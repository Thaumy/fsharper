[<AutoOpen>]
module fsharper.op.Semigroup


let inline mappend ma mb =
    (^m: (member mappend : ^m -> ^m) ma, mb)
