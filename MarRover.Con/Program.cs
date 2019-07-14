using System;
using MarRover.Con.Models;

namespace MarRover.Con
{
    class Program
    {
        static void Main(string[] args)
        {
            var control = new CommandRover();

            System.Console.Write("Input Coordinates: ");
            var size = Console.ReadLine();
            System.Console.Write("Input First Rover's Position: ");
            var firstRover = Console.ReadLine();
            System.Console.Write("Input Command: ");
            var firstCommandRover = Console.ReadLine();
            System.Console.Write("Input Second Rover's Position: ");
            var secondRover = Console.ReadLine();
            System.Console.Write("Input Command: ");
            var secondCommandRover = Console.ReadLine();

            var sizeMap = control.SetSize(size);

            var checkPositionStartRover1 = control.CheckFirstPositionOfRover(sizeMap, firstRover);
            var checkPositionStartRover2 = control.CheckFirstPositionOfRover(sizeMap, secondRover);
            var checkDirectionInputRover1 = control.CheckDirection(firstRover);
            var checkDirectionInputRover2 = control.CheckDirection(secondRover);

            if (!checkPositionStartRover1 || !checkDirectionInputRover1)
            {
                string errorTxt = control.CheckCaseError(checkPositionStartRover1, checkDirectionInputRover1);
                System.Console.WriteLine(errorTxt);
            }
            else
            {
                Positions setPositionRover1 = control.ProcessingControl(control, firstRover, firstCommandRover, sizeMap);
                System.Console.WriteLine($"{setPositionRover1.Coordinate_X} {setPositionRover1.Coordinate_Y} {setPositionRover1.Direction}");
                if (!checkPositionStartRover2 || !checkDirectionInputRover2)
                {
                    string errorTxt = control.CheckCaseError(checkPositionStartRover2, checkDirectionInputRover2);
                    System.Console.WriteLine(errorTxt);
                }
                else
                {
                    Positions setPositionRover2 = control.ProcessingControl(control, secondRover, secondCommandRover, sizeMap);
                    System.Console.WriteLine($"{setPositionRover2.Coordinate_X} {setPositionRover2.Coordinate_Y} {setPositionRover2.Direction}");

                }
            }
        }
    }
}
