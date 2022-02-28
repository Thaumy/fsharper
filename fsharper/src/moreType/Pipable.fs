module fsharper.moreType.Pipable

open fsharper.moreType
open fsharper.moreType.GenericPipable

type Pipable<'T> =
    inherit GenericPipable<'T, 'T>

type Pipe<'T> internal (beforeInvoked: 'T -> 'T) =
    interface Pipable<'T> with
        member self.invoke(arg: 'T) = arg |> beforeInvoked |> self.func

    new() = Pipe(id)

    member self.build() = self :> Pipable<'T>

    member self.import(pipable: Pipable<'T>) =
        Pipe<'T>(pipable.invoke, func = self.func)

    member val func: 'T -> 'T = id with get, set

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
