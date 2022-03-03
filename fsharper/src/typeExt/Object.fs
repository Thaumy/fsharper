[<AutoOpen>]
module fsharper.typeExt.Object

open System
open fsharper.op

type Object with

    member self.tryInvoke(methodName, para) =
        self
            .GetType()
            .GetMethod(methodName)
            .Invoke(self, para)
        |> cast

    member self.tryInvoke(methodName) =
        self.tryInvoke (methodName, [||]) |> cast

    member self.let' f = f self

    member self.also' f =
        self.let' f
        self
    