﻿using System;
using System.Collections.Generic;
using System.Text;
using Models.Model;
using Xamarin.Forms;

namespace app.seed
{
    public static class ExtensionMethods
    {

        

        public static void GetPositionId(this Machine machine)
        {


        }


        public static bool MoveMachineLeft(this Machine machine, IContractXam channel)
        {
            if (channel != null)
            {
                int temp_x = machine.X - 1;

                if (MainPage.MinX <= temp_x &&
                    temp_x < MainPage.MaxX &&
                    machine.Y % 2 != 0
                    )
                {
                    machine.X--;

                    return channel.ChangeMachinePosition(machine);
                }

                return false;
            }

            return false;
        }

        public static bool MoveMachineRight(this Machine machine, IContractXam channel)
        {
            if (channel != null)
            {
                int temp_x = machine.X + 1;

                if (MainPage.MinX <= temp_x &&
                    temp_x < MainPage.MaxX &&
                    machine.Y % 2 != 0
                    )
                {
                    machine.X++;

                    return channel.ChangeMachinePosition(machine);
                }

                return false;
            }

            return false;
        }

        public static bool MoveMachineUp(this Machine machine, IContractXam channel)
        {
            if (channel != null)
            {
                int temp_y = machine.Y - 1;

                if (MainPage.MinY <= temp_y &&
                    temp_y < MainPage.MaxY &&
                    machine.X % 2 != 0
                    )
                {
                    machine.Y--;

                    return channel.ChangeMachinePosition(machine);
                }

                return false;
            }

            return false;
        }

        public static bool MoveMachineDown(this Machine machine, IContractXam channel)
        {
            if (channel != null)
            {
                int temp_y = machine.Y + 1;

                if (MainPage.MinY <= temp_y &&
                    temp_y < MainPage.MaxY &&
                    machine.X % 2 != 0
                    )
                {
                    machine.Y++;

                    return channel.ChangeMachinePosition(machine);
                }

                return false;
            }

            return false;
        }


        public static bool Pause(this Machine machine, IContractXam channel)
        {
            /*
            if (channel != null)
            {
                int temp_y = machine.Y + 1;

                if (RootModel.MinY <= temp_y &&
                    temp_y < RootModel.MaxY &&
                    machine.X % 2 != 0
                    )
                {
                    machine.Y++;

                    return channel.ChangeMachinePosition(machine);
                }

                return false;
            }
            */
            return false;
        }

    }
}
