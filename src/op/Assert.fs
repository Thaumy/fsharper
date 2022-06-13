[<AutoOpen>]
module fsharper.op.Assert

open System
open System.Diagnostics

let inline mustTrue t = Trace.Assert t
let inline mustFalse f = Trace.Assert(not f)
