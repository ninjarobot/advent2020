#load "FsAdvent2020/Day6.fs"

open FsAdvent2020.CustomCustoms

"Day6Data.txt" |> System.IO.File.ReadAllText
|> sumDistinctPerGroup
|> printfn "Sum distinct: %i"

"Day6Data.txt" |> System.IO.File.ReadAllText
|> sumEveryoneInGroup
|> printfn "Sum agreed: %i"
