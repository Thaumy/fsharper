module fsharper.types.GenericStatePipable

open fsharper.op.Coerce
open fsharper.types.GenericPipable

type GenericStatePipe<'I, 'O>(activate: 'I -> 'O, activated: 'I -> 'O) as self =
    [<DefaultValue>]
    val mutable func: 'I -> 'O

    interface GenericPipable<'I, 'O> with
        member self.invoke(arg: 'I) : 'O = arg |> self.func

    do
        self.func <-
            fun arg ->
                self.func <- self.activated

                arg |> self.activate

    member val activate = activate with get, set
    member val activated = activated with get, set

    member self.build() = self :> GenericPipable<'I, 'O>

    member self.import(pipable: GenericPipable<'T, 'I>) =
        GenericStatePipe<'T, 'O>(pipable.invoke >> activate, pipable.invoke >> activated)

type GenericStatePipe<'I, 'O> with
    //Semigroup
    member self.mappend<'a>(mb: GenericStatePipe<'O, 'a>) = self |> mb.import

    //Monoid
    static member mempty() =
        GenericStatePipe<'I, 'O>(coerce, coerce)
