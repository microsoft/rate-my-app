Rate My App
===========

Rate My App component is used to create prompts that appear at set intervals and allow the user to provide feedback and rate the application in the Windows Phone Store.

When the application with Rate My App component is started for the 5th time, a dialog for reviewing the app is shown to the user. If the user declines to review the app, he/she will be given the option to provide direct feedback to the developer. On the 10th run of the app, if a review was not already collected, the user will be prompted one more time to rate the app.

This solution consists of Rate My App component and several demo applications demonstrating how Rate My App component can be integrated to various kinds of Windows Phone applications like XAML, Panorama, XAML/XNA, XAML/Direct3D apps.

This solution is hosted in GitHub:
https://github.com/nokia-developer/rate-my-app

Rate My App component is also available through NuGet Package Manager. Search NuGet repositories for "RateMyApp", install it on your application, and follow the instructions in Rate My App Guide to easily integrate review and feedback functionality to your app. 

Developed with Microsoft Visual Studio Express for Windows Phone 2012.

Compatible with:

 * Windows Phone 7
 * Windows Phone 8

Tested to work on:

 * Nokia Lumia 925
 

Instructions
------------

Make sure you have the following installed:

 * Windows 8
 * Windows Phone SDK 8.0

To build and run the sample:

 * Open the SLN file
   * File > Open Project, select the file RateMyApp.sln
 * Right-click RateMyXamlAppWP8 in the Solution Explorer and select "Set as StartUp Project"   
 * Select the target 'Emulator WXGA'.
 * Press F5 to build the project and run it on the Windows Phone Emulator.

To deploy the sample on Windows Phone 8 device:
 * See the official documentation for deploying and testing applications on Windows Phone devices at http://msdn.microsoft.com/en-us/library/windowsphone/develop/ff402565(v=vs.105).aspx


About the implementation
------------------------

Important folders:

| Folder | Description |
| ------ | ----------- |
| / | Contains the project file, the license information and this file (README.md) |
| RateMyApp | Root folder for the implementation files of RateMyApp component. |
| RateMyApp/Controls | FeedbackOverlay user control of RateMyApp component. |
| RateMyApp/Helpers | Helper classes for RateMyApp component. |
| RateMyApp/Properties | RateMyApp component properties. |
| RateMyApp/Resources | RateMyApp component localization resources. |
| RateMyAppDemos | Root folder for the demo applications using RateMyApp component. |
| RateMyAppDemos/WP7 | Root folder for WP7 demo applications. |
| RateMyAppDemos/WP7/RateMyXamlAppWP7 | Root folder for RateMyXamlAppWP7 application. |
| RateMyAppDemos/WP7/RateMyXamlXnaApp | Root folder for RateMyXamlXnaApp application. |
| RateMyAppDemos/WP8 | Root folder for WP8 demo applications. |
| RateMyAppDemos/WP8/RateMyDirect3DApp | Root folder for RateMyDirect3DApp application. |
| RateMyAppDemos/WP8/RateMyPanoramaApp | Root folder for RateMyPanoramaApp application. |
| RateMyAppDemos/WP8/RateMyXamlAppWP8 | Root folder for RateMyXamlAppWP8 application. |

Important files:

| File | Description |
| ---- | ----------- |
| FeedbackOverlay.xaml | Rating/Feedback dialog user control. |
| FeedbackOverlay.xaml.cs | Code behind file for Rating/Feedback dialog user control. |
| FeedbackHelper.cs | Helper class to control FeedbackOverlay behaviour. |
| StorageHelper.cs | Helper class to handle RateMyApp component settings. |
| MainPage.xaml | Page that contains FeedbackOverlay control. |
| MainPage.xaml.cs | Handles ApplicationBar visibility. |

Important classes:

| Class | Description |
| ----- | ----------- |
| FeedbackOverlay | The user control to access RateMyApp functionality. |


Known issues
------------

No known issues.


License
-------

    Copyright © 2013 Nokia Corporation. All rights reserved.
    
    Nokia, Nokia Developer, and HERE are trademarks and/or registered trademarks of
    Nokia Corporation. Other product and company names mentioned herein may be
    trademarks or trade names of their respective owners.
    
    License
    Subject to the conditions below, you may use, copy, modify and/or merge copies
    of this software and associated content and documentation files (the “Software”)
    to test, develop, publish, distribute, sub-license and/or sell new software
    derived from or incorporating the Software, solely in connection with Nokia
    devices. Some of the documentation, content and/or software maybe licensed under
    open source software or other licenses. To the extent such documentation,
    content and/or software are included, licenses and/or other terms and conditions
    shall apply in addition and/or instead of this notice. The exact terms of the
    licenses, disclaimers, acknowledgements and notices are reproduced in the
    materials provided, or in other obvious locations. No other license to any other
    intellectual property rights is granted herein.
    
    This file, unmodified, shall be included with all copies or substantial portions
    of the Software that are distributed in source code form.
    
    The Software cannot constitute the primary value of any new software derived
    from or incorporating the Software.
    
    Any person dealing with the Software shall not misrepresent the source of the
    Software.
    
    Disclaimer
    THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
    IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS
    FOR A PARTICULAR PURPOSE, QUALITY AND NONINFRINGEMENT. IN NO EVENT SHALL THE
    AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES (INCLUDING,
    WITHOUT LIMITATION, DIRECT, SPECIAL, INDIRECT, PUNITIVE, CONSEQUENTIAL,
    EXEMPLARY AND/ OR INCIDENTAL DAMAGES) OR OTHER LIABILITY, WHETHER IN AN ACTION
    OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE
    SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
    
    Nokia Corporation retains the right to make changes to this document at any
    time, without notice.
  

Version history
---------------

 * 1.0 First public release 


