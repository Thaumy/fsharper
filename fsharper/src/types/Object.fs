[<AutoOpen>]
module fsharper.types.Object

open fsharper.op

[<AutoOpen>]
module ext =
    open System
    open fsharper.op.Coerce

    type Object with

        member self.cast() = self |> coerce

        member self.tryInvoke(methodName, para) =
            self
                .GetType()
                .GetMethod(methodName)
                .Invoke(self, para)
            |> coerce

        member self.tryInvoke(methodName) =
            self.tryInvoke (methodName, [||]) |> coerce

        member self.``let`` f = f self

        member self.also f =
            self.``let`` f
            self
