// -----------------------------------------------------------------------------
// Copyright:	Robert Peter Meyer
// License:		MIT
//
// This source code is licensed under the MIT license found in the
// LICENSE file in the root directory of this source tree.
// -----------------------------------------------------------------------------
using System.Windows.Controls;
using System.Windows.Media.Imaging;

using BB84.Notifications;

namespace BB84.SAU.Application.ViewModels.Base;

/// <summary>
/// The view model base class.
/// </summary>
public abstract class ViewModelBase : NotifiableObject
{
	/// <summary>
	/// Creates an <see cref="Image"/> from the provided <paramref name="uri"/>.
	/// </summary>
	/// <param name="uri">The uri parameter to use.</param>
	/// <returns>A new <see cref="Image"/>.</returns>
	protected static Image CreateImageFromUri(Uri uri)
	{
		BitmapImage bitmap = new();
		bitmap.BeginInit();
		bitmap.UriSource = uri;
		bitmap.EndInit();
		return new() { Source = bitmap };
	}
}
