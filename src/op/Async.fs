module fsharper.op.Async

open System.Threading.Tasks

let inline wait (task: Task) = task.Wait()

let inline result (task: Task<'r>) = task.Result
