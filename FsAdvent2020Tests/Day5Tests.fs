module Day5Tests

open FsAdvent2020
open Xunit

[<Fact>]
let ``Calculate seat 1`` () =
    let boardingPass = "BFFFBBFRRR"
    let row, column, seatID = 70, 7, 567
    let parsedRow, parsedColumn, parsedSeatID = BinaryBoarding.parseBoardingPass boardingPass
    Assert.Equal(row, parsedRow)
    Assert.Equal(column, parsedColumn)
    Assert.Equal(seatID, parsedSeatID)

[<Fact>]
let ``Calculate seat 2`` () =
    let boardingPass = "FFFBBBFRRR"
    let row, column, seatID = 14, 7, 119
    let parsedRow, parsedColumn, parsedSeatID = BinaryBoarding.parseBoardingPass boardingPass
    Assert.Equal(row, parsedRow)
    Assert.Equal(column, parsedColumn)
    Assert.Equal(seatID, parsedSeatID)

[<Fact>]
let ``Calculate seat 3`` () =
    let boardingPass = "BBFFBBFRLL"
    let row, column, seatID = 102, 4, 820
    let parsedRow, parsedColumn, parsedSeatID = BinaryBoarding.parseBoardingPass boardingPass
    Assert.Equal(row, parsedRow)
    Assert.Equal(column, parsedColumn)
    Assert.Equal(seatID, parsedSeatID)

[<Fact>]
let ``Find max seat ID`` () =
    let data = [
        "BFFFBBFRRR"
        "FFFBBBFRRR"
        "BBFFBBFRLL"
    ]
    let maxSeatID = BinaryBoarding.maxSeatID data
    Assert.Equal(820, maxSeatID)
