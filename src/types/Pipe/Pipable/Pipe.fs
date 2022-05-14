namespace fsharper.types.Pipe.Pipable

type Pipe<'T> internal (beforeInvoked: 'T -> 'T) =
    interface Pipable<'T> with
        member self.invoke(arg: 'T) = arg |> beforeInvoked |> self.func

    new() = Pipe(id)

    member self.build() = self :> Pipable<'T>

    member self.import(pipable: Pipable<'T>) =
        Pipe<'T>(pipable.invoke, func = self.func)

    member val func: 'T -> 'T = id with get, set

type Pipe<'T> with
    //Semigroup
    member self.mappend(mb: Pipe<'T>) = self |> mb.import

    //Monoid
    static member mempty() = Pipe<'T>()
