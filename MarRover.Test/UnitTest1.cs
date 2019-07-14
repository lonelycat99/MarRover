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
        // [InlineData("5 5", "3 1 B", "LMMMRM", "Wrong Input Rover1's Direction")]
        // [InlineData("5 5", "4 4 S", "RMRMM", "The beginning of the Rover1 is incorrect.")]
        public void ProcessingControlTest(string size, string position, string command, Positions expected)
        {
            var sut = new CommandRover();
            var setSize = sut.SetSize(size);
            var result = sut.ProcessingControl(position, command, setSize);
            result.Should().BeEquivalentTo(expected);
        }

        public static IEnumerable<object[]> DataOfControlRover = new List<object[]>{
            new object[]{
                "5 5",
                "1 2 N",
                "LMLMLMLMM",
                new Positions
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
                new Positions
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
                new Positions
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
                new Positions
                {
                    Coordinate_X = 1,
                    Coordinate_Y = 4,
                    Direction = "N"
                }
            },
        };
    }
}
