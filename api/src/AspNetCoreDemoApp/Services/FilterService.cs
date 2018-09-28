using System;
using System.Collections.Generic;
using AspNetCoreDemoApp.Models;
using AspNetCoreDemoApp.Utils;

namespace AspNetCoreDemoApp.Services
{
    public class FilterService : IFilterService
    {
        public void AddFilterCommand(IList<FilterCommand> filterCommands, string name, QueryOperator queryOperator, string value)
        {
            AddFilterCommand(filterCommands, ConvertToFilterCommand(name, queryOperator, value));
        }

        public void AddFilterCommand(IList<FilterCommand> filterCommands, string name, QueryOperator queryOperator, DateTime? value)
        {
            AddFilterCommand(filterCommands, ConvertToFilterCommand(name, queryOperator, value));
        }

        public void AddFilterCommand(IList<FilterCommand> filterCommands, string name, QueryOperator queryOperator, bool? value)
        {
            AddFilterCommand(filterCommands, ConvertToFilterCommand(name, queryOperator, value));
        }

        /// <summary>
        /// Adds the given filter command to the list of
        /// filters if it isn't null.
        /// </summary>
        /// <param name="filterCommands">The list of filters to add to.</param>
        /// <param name="command">The filter command to add.</param>
        private void AddFilterCommand(IList<FilterCommand> filterCommands, FilterCommand command)
        {
            if (command != null)
            {
                filterCommands?.Add(command);
            }
        }

        /// <summary>
        /// Validates the string value and
        /// converts it to a generic filter command.
        /// </summary>
        /// <param name="name">The name of the property to filter.</param>
        /// <param name="queryOperator">The type of filter to apply.</param>
        /// <param name="value">The value of the filter to be used</param>
        /// <returns>The generic filter command if the value is valid, null otherwise.</returns>
        private FilterCommand ConvertToFilterCommand(string name, QueryOperator queryOperator, string value)
        {
            FilterCommand command = null;

            if (!string.IsNullOrWhiteSpace(name) && !string.IsNullOrWhiteSpace(value))
            {
                command = CreateGenericFilterCommand(name, queryOperator, value);
            }

            return command;
        }

        /// <summary>
        /// Validates the date value and
        /// converts it to a generic filter command.
        /// </summary>
        /// <param name="name">The name of the property to filter.</param>
        /// <param name="queryOperator">The type of filter to apply.</param>
        /// <param name="value">The value of the filter to be used</param>
        /// <returns>The generic filter command if the value is valid, null otherwise.</returns>
        private FilterCommand ConvertToFilterCommand(string name, QueryOperator queryOperator, DateTime? value)
        {
            FilterCommand command = null;

            if (!string.IsNullOrWhiteSpace(name) && value != null)
            {
                command = CreateGenericFilterCommand(name, queryOperator, value);
            }

            return command;
        }

        /// <summary>
        /// Validates the boolean value and
        /// converts it to a generic filter command.
        /// </summary>
        /// <param name="name">The name of the property to filter.</param>
        /// <param name="queryOperator">The type of filter to apply.</param>
        /// <param name="value">The value of the filter to be used</param>
        /// <returns>The generic filter command if the value is valid, null otherwise.</returns>
        private FilterCommand ConvertToFilterCommand(string name, QueryOperator queryOperator, bool? value)
        {
            FilterCommand command = null;

            if (!string.IsNullOrWhiteSpace(name) && value != null)
            {
                command = CreateGenericFilterCommand(name, queryOperator, value);
            }

            return command;
        }

        /// <summary>
        /// Creates a generic filter command.
        /// </summary>
        /// <param name="name">The name of the property to filter.</param>
        /// <param name="queryOperator">The type of filter to apply.</param>
        /// <param name="value">The value of the filter to be used</param>
        /// <returns>The newly created generic filter command.</returns>
        private FilterCommand CreateGenericFilterCommand(string name, QueryOperator queryOperator, object value)
        {
            return new FilterCommand
			{
                FilterKey = name,
                Operator = queryOperator,
                FilterValue = value
            };
        }
    }
}