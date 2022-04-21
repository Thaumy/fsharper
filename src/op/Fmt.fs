module fsharper.op.Fmt

open System

let inline print x =
    (^x: (member ToString : unit -> string) x)
    |> Console.Write

let inline println x =
    (^x: (member ToString : unit -> string) x)
    |> Console.WriteLine
