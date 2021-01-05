module FsAdvent2020.BinaryBoarding

let parseBoardingPass (boardingPass:string) =
    let rec findRow (range:int * int) (remainingChars:char list) =
        let start, finish = range
        let distanceToMidpoint = (finish - start) / 2
        match remainingChars with
        | [] -> start
        | 'F'::remaining ->
            findRow (start, (finish - distanceToMidpoint)) remaining
        | 'B'::remaining ->
            findRow ((finish - distanceToMidpoint), finish) remaining
        | _ -> failwithf "Malformed boarding pass (row)"
    let row = findRow (0, 128) (boardingPass |> Seq.take 7 |> List.ofSeq)
    let rec findColumn (range:int * int) (remainingChars:char list) =
        let left, right = range
        let distanceToMidpoint = (right - left) / 2
        match remainingChars with
        | [] -> left
        | 'L'::remaining ->
            findColumn (left, (right - distanceToMidpoint)) remaining
        | 'R'::remaining ->
            findColumn ((right - distanceToMidpoint), right) remaining
        | _ -> failwithf "Malformed boarding pass (column)"
    let column = findColumn (0, 7) (boardingPass |> Seq.skip 7 |> Seq.take 3 |> List.ofSeq)
    row, column, ((row * 8) + column)

let maxSeatID (boardingPasses:string seq) =
    boardingPasses
    |> Seq.map (parseBoardingPass >> (fun (_,_,seatID) -> seatID))
    |> Seq.max

let missingSeatID (boardingPasses:string seq) =
    let sortedSeats =
        boardingPasses
        |> Seq.map (parseBoardingPass >> (fun (_,_,seatID) -> seatID))
        |> Seq.sort
        |> Array.ofSeq
    let mutable mySeat = 0
    for i in 1..sortedSeats.Length - 2 do
        let current = sortedSeats.[i]
        let previous = sortedSeats.[i-1]
        let next = sortedSeats.[i+1]
        if current - previous = 2 && next - current = 1 then mySeat <- current - 1
        elif current - previous = 1 && next - current = 2 then mySeat <- current + 1
    mySeat
