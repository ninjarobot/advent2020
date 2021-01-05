module FsAdvent2020.ExpenseReport

type ExpenseReportError = No2020PairFound | No2020TripleFound
let productOf2020Sum2 (entries:int list) : Result<int,ExpenseReportError> =
    let rec findPair (remaining:int list) =
        match remaining with
        | [] -> Result.Error No2020PairFound
        | head::tail ->
            let difference = 2020 - head
            match tail |> List.tryFind(fun n -> n = difference) with
            | Some found -> head * found |> Result.Ok
            | None -> findPair tail
    findPair entries

let productOf2020Sum3 (entries:int list) : Result<int,ExpenseReportError> =
    let rec findPair current (remaining:int list) =
        match remaining with
        | [] -> None
        | head::tail ->
            let difference = current - head
            match tail |> List.tryFind(fun n -> n = difference) with
            | Some found -> (head, found) |> Some
            | None -> findPair current tail
    let rec findTriple (remaining:int list) =
        match remaining with
        | [] -> Result.Error No2020TripleFound
        | head::tail ->
            let difference = 2020 - head
            match findPair difference tail with
            | Some (first, second) -> (head * first * second) |> Result.Ok
            | None -> findTriple tail
    findTriple entries
