﻿using System.Collections.Generic;
using System.Threading.Tasks;
using FluentAssertions;
using Mayhap.Maybe;
using Xunit;

namespace Mayhap.Tests.Maybe
{
    public class TrackTests
    {
        public static IEnumerable<object[]> ContinuationTheory => new[]
        {
            new object[] { "fault".Fail<int>(), "onSuccess".Success(), "fault".Fail<string>() }, 
            new object[] { 100.Success(), "onSuccess".Success(), "onSuccess".Success() },
            new object[] { 100.Success(), "nextFault".Fail<string>(), "nextFault".Fail<string>() },
        };

        [Theory]
        [MemberData(nameof(ContinuationTheory))]
        public void GivenSyncFuncMaybeAndExistingMaybe_WhenContinuationCalledOnExistingMaybe_ThenShouldReturnMaybeWithProperties(
            Maybe<int> previousMaybe,
            Maybe<string> continuationResult,
            Maybe<string> expectedMaybe)
        {
            // given
            Maybe<string> Continuation(int _) => continuationResult;

            // when
            var actual = previousMaybe.Map(Continuation);

            // then
            actual.Should().Be(expectedMaybe);
        }

        [Theory]
        [MemberData(nameof(ContinuationTheory))]
        public async Task GivenAsyncFuncMaybeAndExistingMaybe_WhenContinuationCalledOnExistingMaybe_ThenShouldReturnMaybeWithProperties(
            Maybe<int> previousMaybe,
            Maybe<string> continuationResult,
            Maybe<string> expectedMaybe)
        {
            // given
            Task<Maybe<string>> Continuation(int _) => Task.FromResult(continuationResult);

            // when
            var actual = await previousMaybe.Map(Continuation);

            // then
            actual.Should().Be(expectedMaybe);
        }

        [Theory]
        [MemberData(nameof(ContinuationTheory))]
        public void GivenSyncContinue_WhenCalled_ThenReturnSameMaybeAsReturnAndAsOutParameter(
            Maybe<int> previousMaybe,
            Maybe<string> continuationResult,
#pragma warning disable xUnit1026 // Theory methods should use all of their parameters
            Maybe<string> __)
#pragma warning restore xUnit1026 // Theory methods should use all of their parameters
        {
            // given
            Maybe<string> Continuation(int _) => continuationResult;

            // when
            var returnedMaybe = previousMaybe.Map(Continuation, out var outParamMaybe);

            // then
            returnedMaybe.Should().Be(outParamMaybe);
        }

        [Theory]
        [MemberData(nameof(ContinuationTheory))]
        public async Task GivenAsyncContinue_WhenCalled_ThenReturnSameMaybeAsReturnAndAsOutParameter(
            Maybe<int> previousMaybe,
            Maybe<string> continuationResult,
#pragma warning disable xUnit1026 // Theory methods should use all of their parameters
            Maybe<string> __)
#pragma warning restore xUnit1026 // Theory methods should use all of their parameters
        {
            // given
            Task<Maybe<string>> Continuation(int _) => Task.FromResult(continuationResult);

            // when
            var returnedMaybe = await previousMaybe.Map(Continuation, out var outParamMaybeTask);
            var outParamMaybe = await outParamMaybeTask;

            // then
            returnedMaybe.Should().Be(outParamMaybe);
        }
    }
}