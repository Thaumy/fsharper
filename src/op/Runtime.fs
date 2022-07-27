[<AutoOpen>]
module fsharper.op.Runtime

let panic e = raise e
let panicwith x = x.ToString() |> failwith
