//
// System.Data.OleDb.OleDbDataReader
//
// Authors:
//	Konstantin Triger <kostat@mainsoft.com>
//	Boris Kirzner <borisk@mainsoft.com>
//
// (C) 2005 Mainsoft Corporation (http://www.mainsoft.com)
//

//
// Permission is hereby granted, free of charge, to any person obtaining
// a copy of this software and associated documentation files (the
// "Software"), to deal in the Software without restriction, including
// without limitation the rights to use, copy, modify, merge, publish,
// distribute, sublicense, and/or sell copies of the Software, and to
// permit persons to whom the Software is furnished to do so, subject to
// the following conditions:
// 
// The above copyright notice and this permission notice shall be
// included in all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
// MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
// NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
// LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION
// OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION
// WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
//

using System.Data.Common;
using System.Data.ProviderBase;

using java.sql;

namespace System.Data.OleDb
{
	public sealed class OleDbDataReader : AbstractDataReader
	{
		#region Fields

		#endregion // Fields

		#region Constructors

		internal OleDbDataReader(OleDbCommand command) : base(command)
		{
		}

		#endregion // Constructors

		#region Methods

		protected override SystemException CreateException(string message, SQLException e)
		{
			return new OleDbException(message,e, (OleDbConnection)_command.Connection);		
		}

		protected override SystemException CreateException(java.io.IOException e)
		{
			return new OleDbException(e, (OleDbConnection)_command.Connection);		
		}

		public override String GetDataTypeName(int columnIndex)
		{
			try {
				string jdbcTypeName = Results.getMetaData().getColumnTypeName(columnIndex + 1);
				
				return OleDbConvert.JdbcTypeNameToDbTypeName(jdbcTypeName);
			}
			catch (SQLException e) {
				throw CreateException(e);
			}
		}

		protected override int GetProviderType(int jdbcType)
		{
			return (int)OleDbConvert.JdbcTypeToOleDbType(jdbcType);   
		}

		#endregion // Methods
	}
}