using System;
using MarRover.Con;
using Xunit;
using FluentAssertions;
using MarRover.Con.Models;
using System.Collections.Generic;

namespace MarRover.Test
{
    public class UnitTest1
    {
        [Theory]
        [MemberData(nameof(DataOfControlRover))]
        public void ProcessingControlTest(string size, string position, string command, Rovers expected)
        {
            var sut = new CommandRover();
            var setSize = sut.SetSize(size);
            var setPositionRover = sut.SetPositionRover(position);
            var direction = sut.ConvertDirectionToInt(setPositionRover.Direction);
            var commandRover = sut.SetCommands(command);
            var directionNow = sut.CommandRoverMoving(direction, commandRover, setPositionRover, setSize);
            var result = sut.ConvertDirectionToString(directionNow, setPositionRover);
            result.Should().BeEquivalentTo(expected);
        }

        public static IEnumerable<object[]> DataOfControlRover = new List<object[]>{
            new object[]{
                "5 5",
                "1 2 N",
                "LMLMLMLMM",
                new Rovers
                {
                    Coordinate_X = 1,
                    Coordinate_Y = 3,
                    Direction = "N"
                }
            },
            new object[]{
                "5 5",
                "3 3 E",
                "MMRMMRMRRM",
                new Rovers
                {
                    Coordinate_X = 5,
                    Coordinate_Y = 1,
                    Direction = "E"
                }
            },
            new object[]{
                "5 5",
                "3 1 W",
                "MRMMLMRMMRR",
                new Rovers
                {
                    Coordinate_X = 1,
                    Coordinate_Y = 5,
                    Direction = "S"
                }
            },
            new object[]{
                "5 5",
                "0 0 E",
                "MLMMMRMMRMMRMRMMMLMR",
                new Rovers
                {
                    Coordinate_X = 1,
                    Coordinate_Y = 4,
                    Direction = "N"
                }
            },
        };
    }
}
