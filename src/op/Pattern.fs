module fsharper.op.Pattern

open System.Collections.Generic

let (|KV|) (kv: KeyValuePair<_, _>) = kv.Key, kv.Value
