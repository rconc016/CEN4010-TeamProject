using System;
using System.Collections.Generic;
using AspNetCoreDemoApp.Models;
using AspNetCoreDemoApp.Services;
using AspNetCoreDemoApp.Utils;
using FluentAssertions;
using Xunit;

namespace AspNetCoreDemoApp.Tests.Unit
{
    public class FilterServiceUnitTests
    {
        private FilterService filterService;

        public FilterServiceUnitTests()
        {
            filterService = new FilterService();
        }

        [Fact]
        public void AddFilterCommandWithStringValueShouldNotReturnNull()
        {
            IList<FilterCommand> filterCommands = new List<FilterCommand>();
            string name = "testName";
            QueryOperator queryOperator = QueryOperator.Contains;
            string value = "testValue";            

            filterService.AddFilterCommand(filterCommands, name, queryOperator, value);

            filterCommands.Count.Should().Be(1);
            filterCommands[0].FilterKey.Should().Be(name);
            filterCommands[0].Operator.Should().Be(queryOperator);
            filterCommands[0].FilterValue.Should().Be(value);
        }


        [Theory]
        [InlineData(null, null)]
        [InlineData("", "")]
        [InlineData("testName", null)]
        [InlineData("testName", "")]
        [InlineData(null, "testValue")]
        [InlineData("", "testValue")]
        public void AddFilterCommandWithStringValueShouldReturnNull(string testName, string testValue)
        {
            IList<FilterCommand> filterCommands = new List<FilterCommand>();
            QueryOperator queryOperator = QueryOperator.Contains;            

            filterService.AddFilterCommand(filterCommands, testName, queryOperator, testValue);

            filterCommands.Count.Should().Be(0);
        }

        [Fact]
        public void AddFilterCommandWithDateValueShouldNotReturnNull()
        {
            IList<FilterCommand> filterCommands = new List<FilterCommand>();
            string name = "testName";
            QueryOperator queryOperator = QueryOperator.Contains;
            DateTime value = new DateTime(2018, 9, 27);            

            filterService.AddFilterCommand(filterCommands, name, queryOperator, value);

            filterCommands.Count.Should().Be(1);
            filterCommands[0].FilterKey.Should().Be(name);
            filterCommands[0].Operator.Should().Be(queryOperator);
            filterCommands[0].FilterValue.Should().Be(value);
        }

        [Theory]
        [InlineData(null, false)]
        [InlineData("", false)]
        [InlineData("testName", false)]
        [InlineData(null, true)]
        [InlineData("", true)]
        public void AddFilterCommandWithDateValueShouldReturnNull(string testName, bool validTestDate)
        {
            IList<FilterCommand> filterCommands = new List<FilterCommand>();
            QueryOperator queryOperator = QueryOperator.Contains;            
            DateTime date = validTestDate ? new DateTime(2018, 9, 27) : DateTime.MinValue;

            filterService.AddFilterCommand(filterCommands, testName, queryOperator, date);

            filterCommands.Count.Should().Be(0);
        }
    }
}