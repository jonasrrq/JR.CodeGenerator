using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JR.CodeGenerator.Models
{
    /// <summary>
    /// Setting
    /// </summary>
    public class Setting
    {
        /// <summary>
        /// Gets or sets the data connection.
        /// </summary>
        /// <value>
        /// The data connection.
        /// </value>
        public DataConnection DataConnection { get; set; }
        /// <summary>
        /// Gets or sets the data general.
        /// </summary>
        /// <value>
        /// The data general.
        /// </value>
        public DataGeneral DataGeneral { get; set; }
    }
}
