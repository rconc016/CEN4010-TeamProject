using System;
using System.Collections.Generic;
using AspNetCoreDemoApp.Models;
using AspNetCoreDemoApp.Utils;

namespace AspNetCoreDemoApp.Services
{
    public interface IFilterService
    {
        /// <summary>
        /// Adds the given filter value if is a valid string.
        /// </summary>
        /// <param name="filterCommands">The list of filter commands to add to.</param>
        /// <param name="name">The name of the property to filter.</param>
        /// <param name="queryOperator">The operation to use as a filter.</param>
        /// <param name="value">The value to use in the filter.</param>
        void AddFilterCommand(IList<FilterCommand> filterCommands, string name, QueryOperator queryOperator, string value);

        /// <summary>
        /// Adds the given filter value if is a valid date.
        /// </summary>
        /// <param name="filterCommands">The list of filter commands to add to.</param>
        /// <param name="name">The name of the property to filter.</param>
        /// <param name="queryOperator">The operation to use as a filter.</param>
        /// <param name="value">The value to use in the filter.</param>
        void AddFilterCommand(IList<FilterCommand> filterCommands, string name, QueryOperator queryOperator, DateTime value);

        /// <summary>
        /// Adds the given filter value if is a valid boolean.
        /// </summary>
        /// <param name="filterCommands">The list of filter commands to add to.</param>
        /// <param name="name">The name of the property to filter.</param>
        /// <param name="queryOperator">The operation to use as a filter.</param>
        /// <param name="value">The value to use in the filter.</param>
        void AddFilterCommand(IList<FilterCommand> filterCommands, string name, QueryOperator queryOperator, bool value);
    }
}