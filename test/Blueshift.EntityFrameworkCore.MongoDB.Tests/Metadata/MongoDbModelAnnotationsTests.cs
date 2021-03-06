﻿using Blueshift.EntityFrameworkCore.MongoDB.Metadata;
using Blueshift.EntityFrameworkCore.MongoDB.SampleDomain;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Xunit;

namespace Blueshift.EntityFrameworkCore.MongoDB.Tests.Metadata
{
    public class MongoDbModelAnnotationsTests
    {
        [Fact]
        public void Database_name_null_by_default()
        {
            var mongoDbModelAnnotations = new MongoDbModelAnnotations(new Model());
            Assert.Null(mongoDbModelAnnotations.Database);
        }

        [Fact]
        public void Can_write_database_name()
        {
            var mongoDbModelAnnotations = new MongoDbModelAnnotations(new Model()) { Database = "test" };
            Assert.Equal(expected: "test", actual: mongoDbModelAnnotations.Database);
        }

        [Fact]
        public void Complex_types_is_not_null_but_empty_by_default()
        {
            var mongoDbModelAnnotations = new MongoDbModelAnnotations(new Model());
            Assert.NotNull(mongoDbModelAnnotations.ComplexTypes);
            Assert.Empty(mongoDbModelAnnotations.ComplexTypes);
        }

        [Fact]
        public void Can_add_complex_type()
        {
            var model = new Model();
            var entityType = new EntityType(typeof(Specialty), model, ConfigurationSource.Explicit);
            var mongoDbModelAnnotations = new MongoDbModelAnnotations(model)
            {
                ComplexTypes =
                {
                    entityType
                }
            };
            Assert.True(mongoDbModelAnnotations.ComplexTypes.Contains(entityType));
        }
    }
}