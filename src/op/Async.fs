module fsharper.op.Async

open System.Threading.Tasks

let inline wait (task: Task) = task.Wait()

let inline waitAll (tasks: Task []) = Task.WaitAll tasks

let inline result (task: Task<'r>) = task.Result

let inline resultAll (tasks: Task<'r> []) =
    [| for task in tasks -> task :> Task |] |> waitAll
    [| for task in tasks -> task.Result |]

let inline asTask (vt: ValueTask) = vt.AsTask()
let inline asTaskWait (vt: ValueTask) = vt.AsTask().Wait()
let inline asTaskResult (vt: ValueTask<'r>) = vt.AsTask().Result
