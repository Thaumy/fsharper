module fsharper.op.Error

let panic e = raise e |> ignore
