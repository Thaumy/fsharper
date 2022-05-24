module fsharper.op.Async

open System.Threading.Tasks

let inline wait (task: Task) = task.Wait()

let inline waitAll (tasks: Task []) = Task.WaitAll tasks

let inline result (task: Task<'r>) = task.Result

let inline resultAll (tasks: Task<'r> []) =
    [| for task in tasks -> task :> Task |] |> waitAll
    [| for task in tasks -> task.Result |]
