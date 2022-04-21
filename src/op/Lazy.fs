[<AutoOpen>]
module fsharper.op.Lazy

let inline force (v: Lazy<'t>) = v.Force()
