using System;
using MarRover.Con.Models;

namespace MarRover.Con
{
    public class CommandRover
    {
        public Size SetSize(string size)
        {
            //Set size.
            var dataSize = size.Split(" ");
            var setSize = new Size
            {
                X_Axis = Convert.ToInt32(dataSize[0]),
                Y_Axis = Convert.ToInt32(dataSize[1]),
            };
            return setSize;
        }

        public Rovers SetPositionRover(string dataRover)
        {
            //Set Rover's Position.
            var dataPosition = dataRover.Split(" ");
            var setPosition = new Rovers
            {
                Coordinate_X = Convert.ToInt32(dataPosition[0]),
                Coordinate_Y = Convert.ToInt32(dataPosition[1]),
                Direction = dataPosition[2]
            };
            return setPosition;
        }

        public char[] SetCommands(string commandRover) => commandRover.ToCharArray();

        public int ConvertDirectionToInt(string position)
        {
            //Set Direction.
            switch (position)
            {
                case "N": return (int)DirectionsRover.Directions.N;
                case "E": return (int)DirectionsRover.Directions.E;
                case "S": return (int)DirectionsRover.Directions.S;
                case "W": return (int)DirectionsRover.Directions.W;
                default: return 0;
            }
        }

        public Rovers ConvertDirectionToString(int direction, Rovers positionRover)
        {
            switch (direction)
            {
                case 1:
                    positionRover.Direction = DirectionsRover.Directions.N.ToString();
                    break;
                case 2:
                    positionRover.Direction = DirectionsRover.Directions.E.ToString();
                    break;
                case 3:
                    positionRover.Direction = DirectionsRover.Directions.S.ToString();
                    break;
                case 4:
                    positionRover.Direction = DirectionsRover.Directions.W.ToString();
                    break;
                default:
                    return new Rovers();
            }
            return positionRover;
        }

        public bool IsCheckFirstPositionOfRover(Size sizeMap, string dataRover)
        {
            var checkPositionStartRover = SetPositionRover(dataRover);
            return ((checkPositionStartRover.Coordinate_X >= 0 && checkPositionStartRover.Coordinate_X <= sizeMap.X_Axis)
                && (checkPositionStartRover.Coordinate_Y >= 0 && checkPositionStartRover.Coordinate_Y <= sizeMap.Y_Axis));
        }

        public bool IsCheckDirection(string dataRover)
        {
            var checkPositionStartRover = SetPositionRover(dataRover);
            return (checkPositionStartRover.Direction.Equals("N")
                || checkPositionStartRover.Direction.Equals("E")
                || checkPositionStartRover.Direction.Equals("S")
                || checkPositionStartRover.Direction.Equals("W"))
                ? true : false;
        }

        public Rovers IsCheckLastedPositionOfRover(Size sizeMap, Rovers dataRover)
        {
            if ((dataRover.Coordinate_X < 0) || (dataRover.Coordinate_Y < 0) ||
                (dataRover.Coordinate_X > sizeMap.X_Axis) || (dataRover.Coordinate_Y > sizeMap.Y_Axis))
            {
                dataRover.ErrorText = "Rover out of Plateau on Mars.";
            }
            return dataRover;
        }

        public Rovers IsCheckCaseError(bool checkPositionStartRover, bool checkDirectionInputRover, Rovers positionRover)
        {
            if (!checkPositionStartRover && !checkDirectionInputRover)
            {
                positionRover.ErrorText = "Rover's Direction and Position of the Rover is incorrect.";
            }
            else if (!checkPositionStartRover)
            {
                positionRover.ErrorText = "The beginning position of the Rover is incorrect.";
            }
            else if (!checkDirectionInputRover)
            {
                positionRover.ErrorText = "Wrong Input Rover's Direction.";
            }
            return positionRover;
        }

        public Rovers CommandRoverMoving(int direction, char[] commands, Rovers setPositionRover, Size sizeMap)
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

            return ConvertDirectionToString(getDirection, setPositionRover); ;
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

        public void RoverMoving(Rovers setPositionRover, int getDirection, Size sizeMap)
        {
            if ((setPositionRover.Coordinate_X <= sizeMap.X_Axis) && (setPositionRover.Coordinate_Y <= sizeMap.Y_Axis))
            {
                switch (getDirection)
                {
                    case 1:
                        setPositionRover.Coordinate_Y = setPositionRover.Coordinate_Y + 1;
                        break;
                    case 2:
                        setPositionRover.Coordinate_X = setPositionRover.Coordinate_X + 1;
                        break;
                    case 3:
                        setPositionRover.Coordinate_Y = setPositionRover.Coordinate_Y - 1;
                        break;
                    case 4:
                        setPositionRover.Coordinate_X = setPositionRover.Coordinate_X - 1;
                        break;
                    default:
                        break;
                }
            }
        }
    }
}