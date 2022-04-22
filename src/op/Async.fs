module fsharper.op.Async

open System.Threading.Tasks

let inline wait (task: Task) = task.Wait()

let inline waitAll (tasks: Task []) = Task.WaitAll tasks

let inline waitResult (task: Task<'r>) =
    task.Wait()
    task.Result

let inline result (task: Task<'r>) = task.Result
