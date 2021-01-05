module FsAdvent2020.CustomCustoms

open System

let groups (data:string) =
    data.Split(sprintf "%s%s" Environment.NewLine Environment.NewLine)

let numDistinctAnswers (groupData:string) =
    groupData |> Seq.filter(fun c -> c >= 'a' && c <= 'z') |> Seq.distinct |> Seq.length

let sumDistinctPerGroup (data:string) =
    data |> groups |> Seq.map numDistinctAnswers |> Seq.sum

let numEveryoneAnswers (groupData:string) =
    let groupAnswers = groupData.Split (Environment.NewLine, StringSplitOptions.RemoveEmptyEntries)
    let sizeOfGroup = groupAnswers |> Seq.length
    let groupedByAnswers = groupData |> Seq.filter(fun c -> c >= 'a' && c <= 'z') |> Seq.groupBy (fun c -> c)
    let mutable countEveryoneAnsweredSame = 0
    for _, answers in groupedByAnswers do
        if answers |> Seq.length = sizeOfGroup then
            countEveryoneAnsweredSame <- countEveryoneAnsweredSame + 1
    countEveryoneAnsweredSame

let sumEveryoneInGroup (data:string) =
    data |> groups |> Seq.map numEveryoneAnswers |> Seq.sum
