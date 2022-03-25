namespace fsharper.types.Pipe.GenericPipable

type GenericPipable<'I, 'O> =
    abstract invoke : 'I -> 'O
