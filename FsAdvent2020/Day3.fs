module FsAdvent2020.TobogganTrajectory

let hasTree = function
    | '#' -> true
    | _ -> false

/// Pattern for a hill repeats forever.
let hill (line:string) =
    seq {
        while true do
            yield! line |> Seq.map hasTree
    }

let countTrees (right, down) (lines:string seq) =
    seq {
        let mutable xPos = Unchecked.defaultof<int>
        let mutable countDown = down
        for hill in (lines |> Seq.map hill |> Seq.skip 1) do // skip to first line
            if countDown = 1 then
                countDown <- down
                xPos <- xPos + right
                let tobogganPosition = hill |> Seq.skip xPos |> Seq.head
                yield tobogganPosition
            else
                countDown <- countDown - 1
    }
