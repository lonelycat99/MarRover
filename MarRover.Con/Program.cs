using System;
using System.Collections.Generic;
using System.Text;
using MarRover.Con.Models;

namespace MarRover.Con
{
    class Program
    {
        public static List<DataRovers> dataRovers = new List<DataRovers>();
        static void Main(string[] args)
        {
            var rover = new CommandRover();

            System.Console.Write("Input the number of Rover: ");
            var number = Convert.ToInt32(Console.ReadLine());
            System.Console.Write("Input Coordinates: ");
            var size = Console.ReadLine();

            for (var i = 0; i < number; i++)
            {
                System.Console.Write($"Input Rover's Position{i + 1}: ");
                var roverPosition = Console.ReadLine();
                System.Console.Write("Input Command: ");
                var commandRover = Console.ReadLine();

                dataRovers.Add(new DataRovers
                {
                    Input_Position = roverPosition,
                    Input_Command = commandRover
                });
            }

            for (int i = 0; i < dataRovers.Count; i++)
            {
                var sizeMap = rover.SetSize(size);
                var setPositionRover = rover.SetPositionRover(dataRovers[i].Input_Position);

                //Check first position and direction of Rover.
                var checkPositionStartRover = rover.IsCheckFirstPositionOfRover(sizeMap, dataRovers[i].Input_Position);
                var checkDirectionInputRover = rover.IsCheckDirection(dataRovers[i].Input_Position);
                var checkeDataRover = rover.IsCheckCaseError(checkPositionStartRover, checkDirectionInputRover, setPositionRover);

                if (string.IsNullOrEmpty(checkeDataRover.ErrorText))
                {
                    //Convert direction to number.
                    int direction = rover.ConvertDirectionToInt(checkeDataRover.Direction);

                    //Split control string to char array.
                    char[] commandsRover = rover.SetCommands(dataRovers[i].Input_Command);

                    //Rover's movement.
                    var directionNow = rover.CommandRoverMoving(direction, commandsRover, checkeDataRover, sizeMap);
                    var lastedPosition = rover.ConvertDirectionToString(directionNow, checkeDataRover);

                    //Check the last position of Rover on plateau
                    var result = rover.IsCheckLastedPositionOfRover(sizeMap, lastedPosition);

                    //Display Result.
                    var displayResult = (string.IsNullOrEmpty(result.ErrorText)) ?
                        $"{result.Coordinate_X} {result.Coordinate_Y} {result.Direction}" :
                        $"{result.ErrorText}";
                    System.Console.WriteLine(displayResult);
                }
                else
                {
                    System.Console.WriteLine(checkeDataRover.ErrorText);
                    i += 1;
                }
            }
        }
    }
}
