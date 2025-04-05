// -----------------------------------------------------------------------------
// Copyright:	Robert Peter Meyer
// License:		MIT
//
// This source code is licensed under the MIT license found in the
// LICENSE file in the root directory of this source tree.
// -----------------------------------------------------------------------------
using BB84.SAU.Domain.Exceptions;

namespace BB84.SAU.Domain.Tests.Exceptions;

[TestClass]
public sealed class SteamSdkExceptionTests
{
	[TestMethod]
	[TestCategory("Constructor")]
	public void SteamSdkExceptionTest()
	{
		SteamSdkException? exception;
		string message = "Unit Test Exception!";

		exception = new(message);

		Assert.IsNotNull(exception);
		Assert.AreEqual(message, exception.Message);
		Assert.IsNull(exception.InnerException);
	}
}
