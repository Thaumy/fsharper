[<AutoOpen>]
module fsharper.types.Object

[<AutoOpen>]
module ext =
    open System
    open fsharper.op.Coerce

    type Object with
        member self.tryInvoke(methodName, para) =
            self
                .GetType()
                .GetMethod(methodName)
                .Invoke(self, para)
            |> coerce

        member self.tryInvoke(methodName) =
            self.tryInvoke (methodName, [||]) |> coerce
