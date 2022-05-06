namespace fsharper.typ.Pipe.GenericPipable

type GenericPipable<'I, 'O> =
    abstract invoke : 'I -> 'O
