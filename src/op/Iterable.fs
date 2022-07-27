[<AutoOpen>]
module fsharper.op.Iterable

open System.Collections.Generic

type IEnumerator<'T> with
    member self.map f =
        { new IEnumerator<'a> with
            member i.Current: 'a = f self.Current
            member i.Current: obj = (f self.Current) :> obj
            member i.Reset() = self.Reset()
            member i.Dispose() = self.Dispose()
            member i.MoveNext() = self.MoveNext() }

type IEnumerable<'T> with
    member inline self.map f = self.GetEnumerator().map f

let inline map (t: IEnumerable<'a>) f = t.map f
