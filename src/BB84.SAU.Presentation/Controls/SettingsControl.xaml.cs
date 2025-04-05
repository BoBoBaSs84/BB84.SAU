// -----------------------------------------------------------------------------
// Copyright:	Robert Peter Meyer
// License:		MIT
//
// This source code is licensed under the MIT license found in the
// LICENSE file in the root directory of this source tree.
// -----------------------------------------------------------------------------
using System.Text.RegularExpressions;
using System.Windows.Controls;
using System.Windows.Input;

namespace BB84.SAU.Presentation.Controls;

/// <summary>
/// Interaction logic for SettingsControl.xaml
/// </summary>
public partial class SettingsControl : UserControl
{
	[GeneratedRegex("[^0-9]+")]
	private static partial Regex NumberRegex();

	[GeneratedRegex("^[0-9A-F]+$")]
	private static partial Regex HexRegex();

	/// <summary>
	/// Initializes an instance of the <see cref="SettingsControl"/> class.
	/// </summary>
	public SettingsControl() => InitializeComponent();

	private void OnTbSteamIdInput(object sender, TextCompositionEventArgs e)
	{
		Regex regex = NumberRegex();
		e.Handled = regex.IsMatch(e.Text);
	}

	private void OnTbApiKeyInput(object sender, TextCompositionEventArgs e)
	{
		Regex regex = HexRegex();
		e.Handled = regex.IsMatch(e.Text);
	}
}
