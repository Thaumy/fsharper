namespace fsharper.typ.Pipe

/// 泛用管道接口
type IGenericPipe<'I, 'O> =

    /// 填充管道
    abstract fill : 'I -> 'O

    //import和export通常一方必须实现，另一方由一方导出，是为交换律

    /// 在管道入口前对接管道
    /// 返回一个新的管道
    abstract import : IGenericPipe<'t, 'I> -> IGenericPipe<'t, 'O>

    /// 在管道出口后对接管道
    /// 返回一个新的管道
    abstract export : IGenericPipe<'O, 't> -> IGenericPipe<'I, 't>

type IPipe<'T> = IGenericPipe<'T, 'T>

[<AutoOpen>]
module ext =
    type IGenericPipe<'I, 'O> with

        //Semigroup
        member ma.mappend(mb: IGenericPipe<'O, 't>) = ma.export mb
