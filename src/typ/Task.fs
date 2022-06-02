[<AutoOpen>]
module fsharper.typ.Task

open System.Threading.Tasks

[<AutoOpen>]
module ext =

    type Task with

        static member RunIgnore f =
            fun _ -> task { f () |> ignore }
            |> Task.Run<unit>
            |> ignore

        member self.Then f = self.ContinueWith(fun t -> t |> f)
        member self.Then f = self.ContinueWith(fun _ -> f ())

    type Task<'r> with

        member self.Then f =
            self.ContinueWith(fun (t: Task<_>) -> t |> f)

        member self.Then f =
            self.ContinueWith(fun (t: Task<_>) -> f ())
