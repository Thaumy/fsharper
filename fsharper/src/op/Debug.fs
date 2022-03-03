[<AutoOpen>]
module fsharper.op.Debug

open System

/// 对程序员友好的格式
/// 通常这类API使用递归+反射实现，会带来一定的性能损失。
let inline debug m = (^m: (member debug : unit -> string) m)
/// 打印对程序员友好对格式
/// 通常这类API使用递归+反射实现，会带来一定的性能损失。
let inline debugLog m = debug m |> Console.WriteLine
