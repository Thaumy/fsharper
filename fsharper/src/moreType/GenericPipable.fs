module fsharper.moreType.GenericPipable

open fsharper.moreType

type GenericPipable<'I, 'O> =
    abstract invoke : 'I -> 'O

type GenericPipe<'I, 'O>(func: 'I -> 'O) =

    interface GenericPipable<'I, 'O> with
        member self.invoke(arg: 'I) : 'O = arg |> self.func

    member val func = func with get, set

    member self.build() = self :> GenericPipable<'I, 'O>

    member self.import(pipable: GenericPipable<'T, 'I>) =
        GenericPipe<'T, 'O>(pipable.invoke >> func)

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
