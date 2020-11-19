using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using Models.Model;

namespace app.seed
{
    public class RootModel : INotifyPropertyChanged
    {

        public static int MinX = 0;
        public static int MaxX = 9;
        public static int MinY = 0;
        public static int MaxY = 19;

        List<Machine> machineList;
        public List<Machine> MachineList
        {
            get { return machineList; }
            set
            {
                if (machineList != value)
                {
                    machineList = value;
                    OnPropertyChanged();
                }
            }
        }

        Machine selectedMachine;
        public Machine SelectedMachine
        {
            get { return selectedMachine; }
            set
            {
                if (selectedMachine != value)
                {
                    selectedMachine = value;
                    OnPropertyChanged();
                }
            }
        }

        Position targetPosition;
        public Position TargetPosition
        {
            get { return targetPosition; }
            set
            {
                if (targetPosition != value)
                {
                    targetPosition = value;
                    OnPropertyChanged();
                }
            }
        }

        List<Position> positionsList;
        public List<Position> PositionsList
        {
            get { return positionsList; }
            set
            {
                if (positionsList != value)
                {
                    positionsList = value;
                    OnPropertyChanged();
                }
            }
        }

        public Position GetPositionInListByXY(int x, int y)
        {
            Position position = new Position();
            positionsList.ForEach(i =>
            {
                if (i.X == x && i.Y == y)
                {
                    position = i;
                }

            });

            return position;

            /*
            for (int i = 0; i < positionsList.Count; i++)
            {
                if (positionsList[i].X == x && positionsList[i].Y == y)
                {
                    return i;
                }
            }

            throw new Exception("");

            */

        }








        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
