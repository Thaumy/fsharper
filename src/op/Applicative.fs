[<AutoOpen>]
module fsharper.op.Applicative


let inline ap fa fb =
    (^fa: (static member ap : ^fa -> ^fb -> ^fc) fa, fb)

let inline ``pure`` x =
    (^m: (static member ``pure`` : ^x -> ^m) x)

/// ap
let inline (<*>) fa fb = ap fa fb
