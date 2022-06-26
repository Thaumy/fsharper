namespace fsharper.typ.Pipe.Pipable

open fsharper.typ.Procedure

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

    member self.fill = beforeInvoked .> self.func

    member self.import(p: Pipable<'T>) =
        StatePipe<'T>(p.fill, activate = self.activate, activated = self.activated)

    member self.export(p: Pipable<'T>) = p.import self

    interface Pipable<'T> with
        member i.fill input = self.fill input
        member i.import p = p.export i
        member i.export p = p.import i

type StatePipe<'T> with

    //Semigroup
    member ma.mappend(mb: StatePipe<'T>) = ma.export mb

    //Monoid
    static member mempty() = StatePipe<'T>()
