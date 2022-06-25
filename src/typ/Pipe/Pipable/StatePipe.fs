namespace fsharper.typ.Pipe.Pipable

type StatePipe<'T> private (beforeInvoked: 'T -> 'T) as self =

    [<DefaultValue>]
    val mutable func: 'T -> 'T

    member val activate: 'T -> 'T = id with get, set
    member val activated: 'T -> 'T = id with get, set

    do
        self.func <-
            fun arg ->
                self.func <- self.activated

                arg |> self.activate

    new() = StatePipe(id)

    member self.invoke input = input |> beforeInvoked |> self.func

    member self.import(p: Pipable<'T>) =
        StatePipe<'T>(p.invoke, activate = self.activate, activated = self.activated)

    member self.export(p: Pipable<'T>) = p.import self

    interface Pipable<'T> with
        member i.invoke input = self.invoke input
        member i.import p = p.export i
        member i.export p = p.import i

type StatePipe<'T> with

    //Semigroup
    member ma.mappend(mb: StatePipe<'T>) = ma.export mb

    //Monoid
    static member mempty() = StatePipe<'T>()
