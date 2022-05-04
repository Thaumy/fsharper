[<AutoOpen>]
module fsharper.op.Reflection

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

[<AutoOpen>]
module fn =
    /// 是否具有特性
    let inline hasAttr<'t, 'attr> =
        typeof<'t>.GetCustomAttributes(
            typeof<'attr>,
            false
        )
            .Length > 0