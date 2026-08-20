// -----------------------------------------------------------------------------
// Copyright:	Robert Peter Meyer
// License:		MIT
//
// This source code is licensed under the MIT license found in the
// LICENSE file in the root directory of this source tree.
// -----------------------------------------------------------------------------
using System.Runtime.CompilerServices;
using System.Windows.Threading;

namespace BB84.SAU.Application.Tests;

[TestClass]
public abstract class ApplicationTestBase
{
	public sealed class ViewModelTestAttribute([CallerFilePath] string callerFilePath = "", [CallerLineNumber] int callerLineNumber = -1)
		: TestMethodAttribute(callerFilePath, callerLineNumber)
	{
		private static readonly Lazy<Dispatcher> Apartment = new(CreateApartment, LazyThreadSafetyMode.ExecutionAndPublication);

		public override async Task<TestResult[]> ExecuteAsync(ITestMethod testMethod)
			=> await Apartment.Value.InvokeAsync(() => Invoke(testMethod)).Task.Unwrap();

		private static async Task<TestResult[]> Invoke(ITestMethod testMethod)
			=> [await testMethod.InvokeAsync(null)];

		/// <remarks>
		/// The view models create WPF objects that are backed by apartment bound COM instances, some of
		/// which WPF caches statically for the lifetime of the process. A per test apartment would tear
		/// down those instances as soon as its thread ends, so every later test would fail on the stale
		/// cache. All view model tests therefore share one long living single threaded apartment.
		/// </remarks>
		private static Dispatcher CreateApartment()
		{
			TaskCompletionSource<Dispatcher> source = new(TaskCreationOptions.RunContinuationsAsynchronously);

			Thread thread = new(() =>
			{
				source.SetResult(Dispatcher.CurrentDispatcher);
				Dispatcher.Run();
			})
			{
				IsBackground = true,
				Name = "ViewModelTestApartment"
			};

			thread.SetApartmentState(ApartmentState.STA);
			thread.Start();

			return source.Task.GetAwaiter().GetResult();
		}
	}
}
