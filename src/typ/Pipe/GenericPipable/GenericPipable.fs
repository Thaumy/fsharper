namespace fsharper.typ.Pipe.GenericPipable

type GenericPipable<'I, 'O> =
    
    /// 填充管道
    abstract fill : 'I -> 'O

    /// 在管道入口前对接管道
    /// 返回一个新的管道
    abstract import : GenericPipable<'t, 'I> -> GenericPipable<'t, 'O>

    /// 在管道出口后对接管道
    /// 返回一个新的管道
    abstract export : GenericPipable<'O, 't> -> GenericPipable<'I, 't>
