module fsharper.types.StatePipable

open fsharper.types.Pipable

type StatePipe<'T> private (beforeInvoked: 'T -> 'T) as self =
    [<DefaultValue>]
    val mutable func: 'T -> 'T

    interface Pipable<'T> with
        member self.invoke(arg: 'T) = arg |> beforeInvoked |> self.func

    do
        self.func <-
            fun arg ->
                self.func <- self.activated

                arg |> self.activate

    new() = StatePipe(id)

    member self.build() = self :> Pipable<'T>

    member self.import(pipable: Pipable<'T>) =
        StatePipe<'T>(pipable.invoke, activate = self.activate, activated = self.activated)

    member val activate: 'T -> 'T = id with get, set
    member val activated: 'T -> 'T = id with get, set

type StatePipe<'T> with
    //Semigroup
    member self.mappend(mb: StatePipe<'T>) = self |> mb.import

    //Monoid
    static member mempty() = StatePipe<'T>()
