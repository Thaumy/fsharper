[<AutoOpen>]
module fsharper.types.Array

[<AutoOpen>]
module ext =

    type 'a ``[]`` with

        member self.toList() = self |> List.ofArray
