using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BB84.SAU.Application.Tests;

[TestClass]
public abstract class ApplicationTestBase
{
	public sealed class ViewModelTestAttribute : TestMethodAttribute
	{
		public override TestResult[] Execute(ITestMethod testMethod)
		{
			if (Thread.CurrentThread.GetApartmentState() == ApartmentState.STA)
				return Invoke(testMethod);

			TestResult[] result = [];
			Thread thread = new(() => result = Invoke(testMethod));
			thread.SetApartmentState(ApartmentState.STA);
			thread.Start();
			thread.Join();
			return result;
		}

		private static TestResult[] Invoke(ITestMethod testMethod)
			=> [testMethod.Invoke(null)];
	}
}
