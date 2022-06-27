namespace fsharper.typ.Pipe.IGenericPipe

type IGenericPipe<'I, 'O> =
    
    /// 填充管道
    abstract fill : 'I -> 'O

    /// 在管道入口前对接管道
    /// 返回一个新的管道
    abstract import : IGenericPipe<'t, 'I> -> IGenericPipe<'t, 'O>

    /// 在管道出口后对接管道
    /// 返回一个新的管道
    abstract export : IGenericPipe<'O, 't> -> IGenericPipe<'I, 't>
