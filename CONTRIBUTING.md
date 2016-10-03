# How to setup local environment

## Prerequisites

1. Visual Studio 2015+ 

## Installation steps 

1. Create `sandbox` subfolder in repository root.
1. Install Sitecore 8.0+ version in folder you just created. Use any name for site, for example `algoliaui`.
1. Copy Sitecore license file into `AlgoliaUI.Tests` to enable Sitecore.FakeBD tests. 
1. Open solution file in Visual Studio. Publish `AlgoliaUI` project using `sandbox` profile.
1. Open http://algoliaui/unicorn.aspx link and Sync `Default Configuration`


## Troubleshooting

### Wrong DLL versions copied to sandbox

Solution is tightly coupled to Sitecore 8.2 Initial release through installed nuget packages. That should be fine for generated package and sandbox development because Sitecore DLLs should not be included.
To achieve that `Copy Local` property is set to `False` for all Sitecore DLLs. That may be overridden if Sitecore nuget packages reinstalled.

Symptoms:  

Could not load file or assembly 'System.Web.Mvc' or one of its dependencies. The located assembly's manifest definition does not match the assembly reference. (Exception from HRESULT: 0x80131040)

Solution:

1. Set back `Copy Local` property to `False` for all Sitecore DLLs
2. Restore original DLLs in `sandbox\website\bin` using ZIP archive of the Sitecore site root folder