[<AutoOpen>]
module fsharper.op.Lazy

type delayed<'v> = unit -> 'v

let delay v = fun () -> v
let force v = v ()
