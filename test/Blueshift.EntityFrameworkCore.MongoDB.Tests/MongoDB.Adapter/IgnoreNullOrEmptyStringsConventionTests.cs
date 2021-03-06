﻿using Blueshift.EntityFrameworkCore.MongoDB.Adapter;
using Blueshift.EntityFrameworkCore.MongoDB.SampleDomain;
using MongoDB.Bson.Serialization;
using Xunit;

namespace Blueshift.EntityFrameworkCore.MongoDB.Tests.MongoDB.Adapter
{
    public class IgnoreNullOrEmptyStringsConventionTests
    {
        [Theory]
        [InlineData(data: null)]
        [InlineData("")]
        [InlineData(" \t\v\r\n")]
        [InlineData("TestData")]
        public void Should_not_serialize_null_or_empty_strings(string value)
        {
            var bsonClassMap = new BsonClassMap<Employee>();
            BsonMemberMap bsonMemberMap = bsonClassMap.MapMember(e => e.FirstName);
            var ignoreNullOrEmptyStringsConvention = new IgnoreNullOrEmptyStringsConvention();
            ignoreNullOrEmptyStringsConvention.Apply(bsonMemberMap);
            var employee = new Employee
            {
                FirstName = value
            };
            Assert.Equal(!string.IsNullOrEmpty(value), bsonMemberMap.ShouldSerialize(employee, employee.FirstName));
        }
    }
}