[<AutoOpen>]
module fsharper.op.Error

let panic e = raise e
let panicwith x = x.ToString() |> failwith
