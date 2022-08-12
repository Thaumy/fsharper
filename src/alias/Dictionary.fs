[<AutoOpen>]
module fsharper.alias.Dictionary

open System
open System.Collections.Generic
open System.Collections.Concurrent

type Dict<'k, 'v> = Dictionary<'k, 'v>
type ConcurrentDict<'k, 'v> = ConcurrentDictionary<'k, 'v>
