﻿using System;
using System.Linq;
using CodeSmith.Core.Extensions;
using Exceptionless.Core.Validation;
using Exceptionless.Models;
using Xunit;
using Xunit.Extensions;

namespace Exceptionless.Api.Tests.Validation {
    public class EventValidatorTests {
        private readonly EventValidator _validator;

        public EventValidatorTests() {
            _validator = new EventValidator();
        }
        
        [Theory]
        [InlineData(null, true)]
        [InlineData("1234567", false)]
        [InlineData("12345678", true)]
        [InlineData("1234567890123456", true)]
        [InlineData("123456789012345678901234567890123", false)]
        public void ValidateReferenceId(string referenceId, bool isValid) {
            var result = _validator.Validate(new Event { Type = Event.KnownTypes.Error, ReferenceId = referenceId });
            Assert.Equal(isValid, result.IsValid);
        }

        [Theory]
        [InlineData(Event.KnownTypes.Error, true)]
        [InlineData(Event.KnownTypes.FeatureUsage, true)]
        [InlineData(Event.KnownTypes.Log, true)]
        [InlineData(Event.KnownTypes.NotFound, true)]
        [InlineData(Event.KnownTypes.SessionEnd, true)]
        [InlineData(Event.KnownTypes.SessionStart, true)]
        [InlineData("invalid-type", false)]
        public void ValidateType(string type, bool isValid) {
            var result = _validator.Validate(new Event { Type = type });
            Assert.Equal(isValid, result.IsValid);
        }
    }
}