[<AutoOpen>]
module fsharper.op.Applicative


let inline private runAp fa fb =
    (^fa: (static member ap : ^fa -> ^fb -> ^fc) fa, fb)

let inline ``pure`` x =
    (^m: (static member ``pure`` : ^x -> ^m) x)

let inline ap fa fb = runAp fa fb

/// ap
let inline (<*>) fa fb = ap fa fb
