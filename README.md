# ManagedX.Display
ManagedX.Display is a .NET 4.5 class library providing access to GDI display adapters, monitors and display modes on Windows Vista/7/8/8.1/10.
Also implements DisplayConfig (not available on Windows Vista).

## Features
- Supports both x64 and x86 platforms (AnyCPU)
- CLS-compliant
- Fully commented (offline documentation will be provided later)
- Built taking into account code analysis suggestions (all rules enabled)
- Hand-written: special care has been taken to avoid duplicating types (ie: DXGI), thus increasing compatibility (and readability?)


### Important
For the display adapters/monitors events to work, the application must periodically call DisplayDeviceManager.Refresh() !