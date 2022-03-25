[<AutoOpen>]
module fsharper.op.Lazy

type Delayed<'v> = unit -> 'v

let delay v = fun () -> v
let force v = v ()
