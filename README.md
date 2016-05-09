# ManagedX.Display
ManagedX.Display is a .NET 4.6 class library which provides access to GDI display devices (adapters, monitors) on Windows Vista/7/8/8.1/10.

Also partially implements the DisplayConfig API (not available on Windows Vista).


## Features
- Supports both x64 and x86 platforms (AnyCPU)
- CLS-compliant
- Fully commented (offline documentation will be provided later)
- Built taking into account code analysis suggestions (all rules enabled)
- Hand-written: special care has been taken to avoid duplicating types (ie: DXGI), thus maximizing compatibility


### Important
For the display adapters/monitors events to work, the application must periodically call DisplayDeviceManager.Refresh() !


### Requirements
- Windows Vista SP2 or newer
- .NET Framework 4.6 : https://www.microsoft.com/en-us/download/details.aspx?id=48130
- ManagedX : https://github.com/YHiniger/ManagedX