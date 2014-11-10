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

namespace VHDL.statement
{
    using LabeledElement = VHDL.LabeledElement;

    /// <summary>
    /// Abstract base class for all sequential statements.
    /// </summary>
    [Serializable]
	public abstract class SequentialStatement : LabeledElement
	{
		private string label;

        /// <summary>
        /// Returns/Sets the statement's label.
        /// </summary>
		public override string Label
		{
            get { return label; }
            set { label = value; }
		}

		internal abstract void accept(SequentialStatementVisitor visitor);

        /// <summary>
        /// �������� ������ ���� ��������� ���������
        /// </summary>
        /// <returns></returns>
        public abstract List<VhdlElement> GetAllStatements();
	}

}