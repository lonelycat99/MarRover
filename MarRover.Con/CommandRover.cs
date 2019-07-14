using System;
using MarRover.Con.Models;

namespace MarRover.Con
{
    public class CommandRover
    {
        public Size SetSize(string size)
        {
            //Set size
            var dataSize = size.Split(" ");
            var setSize = new Size
            {
                X_Axis = Convert.ToInt32(dataSize[0]),
                Y_Axis = Convert.ToInt32(dataSize[1]),
            };
            return setSize;
        }

        public Positions SetPositionRover(string firstRover)
        {
            //Set Rover's Position
            var dataPosition = firstRover.Split(" ");
            var setPosition = new Positions
            {
                Coordinate_X = Convert.ToInt32(dataPosition[0]),
                Coordinate_Y = Convert.ToInt32(dataPosition[1]),
                Direction = dataPosition[2]
            };
            return setPosition;
        }

        public bool CheckFirstPositionOfRover(Size sizeMap, string firstRover)
        {
            var checkPositionStartRover = SetPositionRover(firstRover);
            return ((checkPositionStartRover.Coordinate_X <= sizeMap.X_Axis) && (checkPositionStartRover.Coordinate_Y <= sizeMap.Y_Axis));
        }

        public bool CheckDirection(string firstRover)
        {
            var checkPositionStartRover = SetPositionRover(firstRover);
            return (checkPositionStartRover.Direction.Equals("N") || checkPositionStartRover.Direction.Equals("E") || checkPositionStartRover.Direction.Equals("S") || checkPositionStartRover.Direction.Equals("W")) ? true : false;
        }

        public void ConvertDirectionToString(int direction, Positions setPositionRover)
        {
            if (direction == 1)
            {
                setPositionRover.Direction = DirectionsRover.Directions.N.ToString();
            }
            if (direction == 2)
            {
                setPositionRover.Direction = DirectionsRover.Directions.E.ToString();
            }
            if (direction == 3)
            {
                setPositionRover.Direction = DirectionsRover.Directions.S.ToString();
            }
            if (direction == 4)
            {
                setPositionRover.Direction = DirectionsRover.Directions.W.ToString();
            }
        }

        public int CommandRoverMoving(int direction, char[] commands, Positions setPositionRover, Size sizeMap)
        {
            var getDirection = direction;
            foreach (var command in commands)
            {
                switch (command)
                {
                    case 'L':
                        getDirection = RoverTurnLeft(getDirection);
                        break;
                    case 'R':
                        getDirection = RoverTurnRight(getDirection);
                        break;
                    case 'M':
                        RoverMoving(setPositionRover, getDirection, sizeMap);
                        break;
                    default:
                        break;
                }
            }
            return getDirection;
        }

        public int RoverTurnRight(int getDirection)
        {
            getDirection = ((getDirection + 1) > (int)DirectionsRover.Directions.W) ? (int)DirectionsRover.Directions.N : getDirection + 1;
            return getDirection;
        }

        public int RoverTurnLeft(int getDirection)
        {
            getDirection = ((getDirection - 1) < (int)DirectionsRover.Directions.N) ? (int)DirectionsRover.Directions.W : getDirection - 1;
            return getDirection;
        }

        public void RoverMoving(Positions setPositionRover, int getDirection, Size sizeMap)
        {
            if ((setPositionRover.Coordinate_X <= sizeMap.X_Axis) && (setPositionRover.Coordinate_Y <= sizeMap.Y_Axis))
            {
                if (getDirection == 1)
                {
                    setPositionRover.Coordinate_Y = setPositionRover.Coordinate_Y + 1;
                }
                if (getDirection == 2)
                {
                    setPositionRover.Coordinate_X = setPositionRover.Coordinate_X + 1;
                }
                if (getDirection == 3)
                {
                    setPositionRover.Coordinate_Y = setPositionRover.Coordinate_Y - 1;
                }
                if (getDirection == 4)
                {
                    setPositionRover.Coordinate_X = setPositionRover.Coordinate_X - 1;
                }
            }
        }

        public char[] SetCommands(string firstCommandRover)
        {
            //Get Commands
            return firstCommandRover.ToCharArray();
        }

        public int ConvertDirectionToInt(string position)
        {
            //Set Direction
            var direction = 0;
            if (position == "N")
            {
                direction = (int)DirectionsRover.Directions.N;
            }
            if (position == "E")
            {
                direction = (int)DirectionsRover.Directions.E;
            }
            if (position == "S")
            {
                direction = (int)DirectionsRover.Directions.S;
            }
            if (position == "W")
            {
                direction = (int)DirectionsRover.Directions.W;
            }
            return direction;
        }

        public string CheckCaseError(bool checkPositionStartRover1, bool checkDirectionInputRover1)
        {
            var errorTxt = "";
            if (!checkPositionStartRover1 && !checkDirectionInputRover1)
            {
                errorTxt = "Rover1's Direction or beginning of the Rover1 is incorrect";
            }
            else if (!checkPositionStartRover1)
            {
                errorTxt = "The beginning of the Rover1 is incorrect.";
            }
            else if (!checkDirectionInputRover1)
            {
                errorTxt = "Wrong Input Rover1's Direction";
            }

            return errorTxt;
        }

        public Positions ProcessingControl(string firstRover, string firstCommandRover, Size sizeMap)
        {
            var setPositionRover = SetPositionRover(firstRover);
            int direction = ConvertDirectionToInt(setPositionRover.Direction);
            char[] commandsRover = SetCommands(firstCommandRover);
            var latedDirection = CommandRoverMoving(direction, commandsRover, setPositionRover, sizeMap);
            ConvertDirectionToString(latedDirection, setPositionRover);
            return setPositionRover;
        }
    }
}