[<AutoOpen>]
module fsharper.types.Pipe.GenericPipable

open fsharper.op.Coerce

type GenericPipable<'I, 'O> =
    abstract invoke : 'I -> 'O

type GenericPipe<'I, 'O>(func: 'I -> 'O) =

    interface GenericPipable<'I, 'O> with
        member self.invoke(arg: 'I) : 'O = arg |> self.func

    member val func = func with get, set

    member self.build() = self :> GenericPipable<'I, 'O>

    member self.import(pipable: GenericPipable<'T, 'I>) =
        GenericPipe<'T, 'O>(pipable.invoke >> func)

type GenericPipe<'I, 'O> with
    //Semigroup
    member self.mappend<'a>(mb: GenericPipe<'O, 'a>) = self |> mb.import

    //Monoid
    static member mempty() = GenericPipe<'I, 'O>(coerce)
