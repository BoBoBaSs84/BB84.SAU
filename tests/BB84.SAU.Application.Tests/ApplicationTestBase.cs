// -----------------------------------------------------------------------------
// Copyright:	Robert Peter Meyer
// License:		MIT
//
// This source code is licensed under the MIT license found in the
// LICENSE file in the root directory of this source tree.
// -----------------------------------------------------------------------------
using System.Runtime.CompilerServices;

namespace BB84.SAU.Application.Tests;

[TestClass]
public abstract class ApplicationTestBase
{
	public sealed class ViewModelTestAttribute([CallerFilePath] string callerFilePath = "", [CallerLineNumber] int callerLineNumber = -1)
		: TestMethodAttribute(callerFilePath, callerLineNumber)
	{
		public override async Task<TestResult[]> ExecuteAsync(ITestMethod testMethod)
		{
			if (Thread.CurrentThread.GetApartmentState() == ApartmentState.STA)
				return await Invoke(testMethod);

			TestResult[] result = [];
			Thread thread = new(async () => result = await Invoke(testMethod));
			thread.SetApartmentState(ApartmentState.STA);
			thread.Start();
			thread.Join();
			return result;
		}

		private static async Task<TestResult[]> Invoke(ITestMethod testMethod)
			=> [await testMethod.InvokeAsync(null)];
	}
}
