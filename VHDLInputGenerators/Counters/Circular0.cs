//
//  Copyright (C) 2010-2014  Denis Gavrish
//
//  This program is free software: you can redistribute it and/or modify
//  it under the terms of the GNU General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
//
//  This program is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU General Public License for more details.
//
//  You should have received a copy of the GNU General Public License
//  along with this program.  If not, see <http://www.gnu.org/licenses/>.
//

using System;
using System.Collections.Generic;
using System.Text;
using VHDLRuntime;

namespace VHDLInputGenerators.Counters
{
    public class Circular0 : Counter
    {
        public override bool[] Next(bool[] value)
        {
            bool[] res = new bool[value.Length];
            value.CopyTo(res, 0);

            int index = StartIndexOf(value, false);
            if (index < 0)
                index = 0;

            res[index] = true;
            index = (index + value.Length - 1) % value.Length;
            res[index] = false;
            return res;
        }

        public override bool[] Next(bool[] value, uint step_count)
        {
            if (step_count == 1)
            {
                return this.Next(value);
            }
            else
            {
                step_count %= (uint)value.Length;
                bool[] res = new bool[value.Length];
                value.CopyTo(res, 0);

                int index = StartIndexOf(value, false);
                if (index == -1)
                    index = 0;

                res[index] = true;
                index = (index + value.Length - (int)step_count) % value.Length;
                res[index] = false;
                return res;
            }
        }

        public override bool[] Prev(bool[] value)
        {
            bool[] res = new bool[value.Length];
            value.CopyTo(res, 0);

            int index = StartIndexOf(value, false);
            if (index < 0)
                index = value.Length - 1;

            res[index] = true;
            index = (index + 1) % (value.Length);
            res[index] = false;
            return res;
        }

        public override bool[] Prev(bool[] value, uint step_count)
        {
            if (step_count == 1)
            {
                return this.Prev(value);
            }
            else
            {
                step_count %= (uint)value.Length;
                bool[] res = new bool[value.Length];
                value.CopyTo(res, 0);

                int index = StartIndexOf(value, false);
                if (index < 0)
                    index = value.Length - 1;
                res[index] = true;
                index = (index + (int)step_count) % (value.Length);
                res[index] = false;
                return res;
            }
        }

        public override bool CheckForCorrect(bool[] value)
        {
            int zerocount = 0;
            int onecount = 0;
            for (int i = 0; i < value.Length; i++)
            {
                if (value[i] == false)
                    zerocount++;
                else
                    onecount++;
            }
            if (zerocount == 1)
                return true;
            else
                return false;
        }
        public override string ToString()
        {
            return "Counter : Circular0";
        }

        public override StringBuilder StringVhdlRealization(KeyValuePair<string, TimeInterval> param)
        {
            StringBuilder res = new StringBuilder();

            res.Append("\n").Append(param.Key).Append(" <= ").Append(param.Key).Append("(").Append(param.Key).Append("\'left - ").Append(stepCount).Append(" downto ").Append(param.Key).Append("\'right)&( ").Append(param.Key).Append("(").Append(param.Key).Append("\'left ))").Append(" after ").Append(timeStep.TimeNumber).Append(" ").Append(timeStep.Unit).Append(" when now < END_TIME ").Append(" ; ").Append("\n");
            //   s1<=s1(s1'left-1 downto s1'right)&s1(s1'left) after 10 ns;    
            return res;
        }

    }
}
