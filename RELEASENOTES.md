# **Raptor Release Notes**

## <span style='color:mediumseagreen;font-weight:bold'>Version 0.27.0</span>

### **New** 🎉

1. Exposed internal struct type `SpriteBatchItem`
   * This was exposed just in case a library user wanted to use this
2. Added a new extension method named `MapValue` to the `byte` data type to allow the user to map a `byte` value to another range where the start and stop values of the range are of the type `float`

### **Changes** ✨

1. Updated the **feature to develop** PR template to not include the card type in the PR title
2. Update code documentation throughout the library related to explanations of true and false values
   * This is about making the true and false values show as key words in the documentation
3. Fixed spelling issue in `AtlasSubTextureData` constructor code documentation

### **Improvements** 🌟

1. Added a finalizer to the `Texture` class to make sure disposal of unmanaged resources is performed.
2. Fixed code documentation on following properties
   * `IWindowProps.Initialized`
   * `ISpriteBatch.BatchSize`

### **Nuget/Library Updates** 📦

1. Updated the nuget package **System.IO.Abstractions** from **v13.2.28** to **v13.2.29**

### **Breaking Changes** 💣

1. Updated all solution projects to **NET 5.0**!!
2. Added `IDisposable` interface to the `IContent` interface to enforce disposable of content types
3. Removed the `KeyEventArgs` type
   * This was not being used in the library and was an old type that used to used but was never removed
4. Changed the `ToGLColor` extension method on the `System.Drawing.Color` type from public to internal
   * This was unintentionally exposing a type that was meant to be hidden from the library user
5. Renamed constructor parameter for `AtlasData` class to fix spelling mistake
   * Renamed parameter from `atlasSubTexutureData` to `atlasSubTextureData`
6. Changed the `FreeTypeErrorEventArgs` class from public to internal
   * This was not meant to be exposed to the library user
7. Changed the `ITaskService` interface from public to internal
   * This was not meant to be exposed to the library user
8. Changed the `TaskService` class from public to internal
   * This was not meant to be exposed to the library user
9. Changed the name of the `SystemDisplayException` class to `SystemDisplayException`
   * This was due to keeping the API language synonymous with the rest of the library API.  For example, the `ISystemMonitorService` and `SystemMonitorService` types
10. Changed the `OpenGLObservable` class from public to internal
   * This was not meant to be exposed to the library user
11. Changed the `ImageData` struct to a readonly structure
   * This will enforce the use of the constructor to be used to set the `ImageData.Color`, `ImageData.Width`, and `ImageData.Height` values
12. Changed the `ImageData` `Width`, `Height`, and `Color` properties to public fields

---

## <span style='color:mediumseagreen;font-weight:bold'>Version 0.26.0</span>

### **New** 🎉

1. Exposed internal helper methods below for users to take advantage of
   * `RotateAround()`
     * This method lets you rotate a vector around a given origin at a given angle either clockwise or counterclockwise
   * ToVector4()
     * This method lets you convert a 4 component `Color` type to a `Vector4` type
2. Added new type called `ImageData` to hold pixel data as well as width and height of an image.
3. Added new property named `Unloaded` to the interface `IContent` to represent a value to indicating if content has been **unloaded**
    * This effects the current content types below as they as well have the new `Unloaded` property as well
        * `Sound`
        * `AtlasData`
        * `Texture`
4. Added new enum named `FontStyle` to represent the font styles **Regular**, **Bold**, **Italic**
5. Added new type called `FontSettings` that represent font settings JSON data that must exist on disk along with a font file when loading a font
6. Added new `ResolveDirPath()` method to the `IPathResolver` type to give the ability to get resolved directory paths to a content item
    * Other resolver types that are inheriting the abstract class `ContentPathResolver` will of course get this functionality for free
7. Added new `LoadContentException` type that will be thrown when there is an exception when loading content
8. Added new `LoadFontException` type that will be thrown when there is an exception loading font content
9. Added new `InvalidInputException` type that will be thrown when an input is used that is not part of the available `KeyCode` or `MouseButton` enums
10. Added new `SystemDisplayException` type that will be thrown when there is an exception with loading or setting system monitor settings
11. Added new method `CreateFontLoader()` to the `ContentLoaderFactory` class to allow users to create an instance of an `ILoader<IFont>` implementation for loading fonts
12. Added new method `CreateFontPathResolver()` to the `PathResolverFactory` class to allow users to create an instance of `IPathResolver` for resolving paths to font content
13. Added new types 'IFont' and `Font` to be used a new type of content that can be loaded for rendering text
14. Added new type `GlyphMetrics` that holds the various metrics of a single glyph for a particular font
    * This can be used to do custom text rendering
15. Added methods below to allow the user to render text to the screen using an `IFont` implementation
    * `void Render(IFont font, string text, int x, int y)`
    * `void Render(IFont font, string text, int x, int y, Color tintColor)`
16. Added new interface `IFontAtlasService`
    * An implementation of this interface is used internally in the library to create font atlas textures which is used to render texture during runtime.
    * This interface can be used to implement a custom font atlas texture creator
17. Added a `SetState(TInputs input, bool state)` method to the `IGameInput` interface that will allow the user to manually manipulate the state
    * The types below inherit this new functionality of the `IGameInput` interface
      * New interface `IKeyboardInput`
      * New interface `IMouseInput`
      * Class `Keyboard`
      * Class `Mouse`
1.  Added new `Initialized` property to the `IWindowProps` interface
    * This is to allow a users implementation to indicate when the window has finished being initialized
2.  Added new `Initialized` property to the `Window` class
    * This is new functionality that has added to indicate that the window has finished initialization
3.  Added observable pattern types below for the purpose of the library user to create messaging systems
    * `Observable`
    * `Obsever`
    * `ObserverUnsubscriber`

### **Changes** ✨

1. Incoming content name processing related to file extensions changed for abstract class `ContentPathResolver`
    * This class does not check for and remove file extensions anymore. This responsibility is now on the implementation of types of `ContentPathResolver`
    * The following already built resolvers have been updated appropriately to process file extensions
        * `AtlasJSONDataPathResolver`
        * `AtlasTexturePathResolver`
        * `SoundPathResolver`
        * `TexturePathResolver`

### **Nuget/Library Updates** 📦

1. Replaced nuget package **Microsoft.CodeAnalysis.FxCopAnalyzers** to **Microsoft.CodeAnalysis.NetCopAnalyzers** **v5.0.3**
   * The **Microsoft.CodeAnalysis.FxCopAnalyzers** nuget library is being deprecated and it should be replaced with the other nuget package mentioned above
2. Added nuget package **FreeTypeSharp** **v1.1.3**
   * This is a binding library to the native cross platform library **FreeType** for loading and dealing with fonts
3. Updated nuget package **Newtonsoft.Json** from **v12.0.3** to **v13.0.1**
4. Updated nuget package **SimpleInjector** from **v5.1.0** to **v5.3.0**
5. Updated nuget package **SixLabors.ImageSharp** from **v1.0.2** to **v1.0.3**
6. Updated nuget package **System.IO.Abstractions** from **v12.2.24** to **v13.2.28**

### **Breaking Changes** 💣

1. Renamed interface `IImageFileService` to `IImageService`
    * Returns a new type named `ImageData` to represent the pixel data as well as width and height of an image
2. Renamed class `ImageFileService` to `ImageService`
    * Returns a new type named `ImageData` to represent the pixel data as well as width and height of an image
3. Changed `TextureLoader` constructor parameter to reflect name change to the `IImageFileService` interface
4. Changed `Texture` constructor signature from `Texture(string name, string path, byte[] pixelData, int width, int height)` to `Texture(string name, string path, ImageData imageData)`
5. Refactored the `FileDirectoryName` property name on the `IPathResolver` interface to the name `ContentDirectoryName`
    * This is to align more with the meaning and contextual purpose of the property
    * NOTE: All all other existing resolver types have picked this change up as well
6. Swapped parameters for the `AtlasData` class constructor to better align for the intended public facing API of the library
    * The position of the parameters `ITexture texture` and `AtlasSubTextureData[] atlasSubTextureData` were swapped
7. Removed the `None` enum value from the `MouseButton` enumeration

### **Bug Fixes** 🐛

1. Fixed a bug where content file items with extension with upper case letters were not being loaded
2. Fixed a bug where the default X position of a window was being calculated incorrectly
3. Fixed an issue where unit tests were failing with false positives related to unsafe code and testing the `FontAtlasService` class
   * This was causing unreliable builds with tests failing when they should not of

---

## <span style='color:mediumseagreen;font-weight:bold'>Version 0.25.0</span>

### **New** 🎉

1. Added the ability to apply render effects to a texture being rendered
   * Added a render effect to flip the texture horizontally
   * Added a render effect to flip the texture vertically
   * Added a render effect to flip the texture horizontally and vertically in one render call
2. Added the ability to set the screen clear color during runtime

### **Improvements** 🌟

1. Added simple improvements to internal value caching
   * This is related to caching <span style='font-weight: bold; color: dodgerblue'>OpenGL</span> values before <span style='font-weight: bold; color: dodgerblue'>OpenGL</span> has been initialized

---

## <span style='color:mediumseagreen;font-weight:bold'>Version 0.24.1</span> - <span style='color:indianred;font-weight:bold'>(Hot Fix)</span>

### **Bug Fixes** 🐛

1. Fixed an issue where loading content would throw an exception that the content did not exist even though it did exist
   * This was occurring for <span style='font-weight: bold; color: dodgerblue'>texture</span> and <span style='font-weight: bold; color: dodgerblue'>texture atlas</span> JSON types of content 

### **Improvements** 🌟

1. Made simple adjustments to hot PR templates

---

## <span style="color:mediumseagreen;font-weight:bold">Version 0.24.0</span>

### **Breaking Changes** 💣

1. Moved the following types from the <span style='font-weight: bold; color: khaki'>Raptor.Desktop</span> namespace to the <span style='font-weight: bold; color: khaki'>Raptor.UI</span> namespace
   * NOTE: The types below were all of the types in the <span style='font-weight: bold; color: khaki'>Raptor.Desktop</span> namespace.  The namespace has been removed entirely
   * <span style='font-weight: bold; color: khaki'>IWindow</span>
   * <span style='font-weight: bold; color: khaki'>IWindowAction</span>
   * <span style='font-weight: bold; color: khaki'>IWindowProps</span>
   * <span style='font-weight: bold; color: khaki'>Window</span>
2. Replaced the <span style='font-weight: bold; color: khaki'>IKeyboard</span> and <span style='font-weight: bold; color: khaki'>IMouse</span> types with a new type named <span style='font-weight: bold; color: khaki'>IGameInput<TInputs, TInputState></span>
   * The types <span style='font-weight: bold; color: khaki'>IKeyboard</span> and <span style='font-weight: bold; color: khaki'>IMouse</span> have been removed
3. Removed the <span style='font-weight: bold; color: khaki'>PointerContainer</span> class from the library

### **Improvements** 🌟

1. Improved audio device management and how audio is played

### **Tech Debt/Cleanup** 🧹

1. Refactored code to meet coding standards
2. Cleaned up <span style='font-weight: bold; color: dodgerblue'>YAML</span> files for build pipeline
   * No functional changes were done.  Just cleanup
3. Refactored unit test method names for consistency and clarity
4. Increased code coverage of various areas of the code base
5. Made correct to PR templates used when merging hot fix branches to develop branches

---

## <span style="color:mediumseagreen;font-weight:bold">Version 0.23.1</span> - <span style="color:indianred;font-weight:bold">(Hot Fix)</span>

### **Bug Fixes** 🐛

1. Fixed a bug where only the max of 2 textures could be rendered in a batch on the same texture
   * The max quad buffer data amount (batch size) of 2 was all that was getting alloted on the GPU.  This was throwing an exception when attempting to render more than 2 quads worth of buffer data

### **New** 🎉

1. Added the ability to the <span style='font-weight: bold; color: khaki'>SpriteBatch </span> class to be able to set a custom batch size amount
   * Doing this will dispose of the GPU buffers and shader programs and re-create them

---

## <span style="color:mediumseagreen;font-weight:bold">Version 0.23.0</span>

### **Breaking Changes** 💣

1. Removed <span style='font-weight: bold; color: khaki'>InvalidReason </span> enum from library
2. Removed <span style='font-weight: bold; color: khaki'>AtlasRegionRectangle </span> from library
3. Removed <span style='font-weight: bold; color: khaki'>IAtlasRegionRectangle </span> from library

### **Changes** ✨

1. Changed <span style='font-weight: bold; color: khaki'>AtlasData.Texture </span> property to no longer be readonly
2. Added caching ability to the <span style='font-weight: bold; color: khaki'>TextureLoader </span> class to internally cache textures to improve performance
   * Loading of a texture that has already been loaded will return the exact same texture
3. Added caching ability to the <span style='font-weight: bold; color: khaki'>AtlasLoader </span> class to internally cache atlas data to improve performance
   * Loading of a texture atlas and atlas data that has already been loaded will return the exact same data
4. Added caching ability to the <span style='font-weight: bold; color: khaki'>SoundLoader </span> class to internally cache sounds to improve performance
   * Loading of a sound that has already been loaded will return the exact same sound

### **New** 🎉

1. Added new enumeration type named <span style='font-weight: bold; color: khaki'>TextureType </span> to represent the kind of texture
   * This can be represent the rendering of a whole texture or sub-texture
2. Added a new interface type named <span style='font-weight: bold; color: khaki'>IContentUnloadable</span>
   * This can be used to unload content
3. Added ability to throw a descriptive exception when attempting to render a texture with a width or height that is less than or equal to zero
4. Add feature to be able to unload texture, texture atlas, and sound data
   * This has been implemented into the <span style='font-weight: bold; color: khaki'>TextureLoader</span>, <span style='font-weight: bold; color: khaki'>AtlasLoader</span>, and <span style='font-weight: bold; color: khaki'>SoundLoader</span>

### **Tech Debt/Cleanup** 🧹

1. Refactor class field for <span style='font-weight: bold; color: khaki'>AtlasData </span> class

---

## <span style="color:mediumseagreen;font-weight:bold">Version 0.22.0</span>

### **Breaking Changes** 💣

1. Changed the <span style='font-weight: bold; color: khaki'>Sound </span> class to take in a full file path to the sound content instead of just a sound name
   * Before this used a content source to resolve where the sound content was located
   * The new <span style='font-weight: bold; color: khaki'>SoundLoader </span> is now responsible for resolving the path to the sound content before loading the sound
2. The following types have changed their names
   * <span style='font-weight: bold; color: khaki'>AtlasDataLoader </span> class name has been changed to <span style='font-weight: bold; color: khaki'>AtlasLoader</span>
   * <span style='font-weight: bold; color: khaki'>IContentSource </span> class name has been changed to <span style='font-weight: bold; color: khaki'>IPathResolver</span>
   * <span style='font-weight: bold; color: khaki'>ContentSource </span> class name has been changed to <span style='font-weight: bold; color: khaki'>ContentPathResolver</span>
   * <span style='font-weight: bold; color: khaki'>AtlasContentSource </span> class has been changed to <span style='font-weight: bold; color: khaki'>AtlasJSONDataPathResolver</span>
   * <span style='font-weight: bold; color: khaki'>SoundContentSource </span> class has been changed to <span style='font-weight: bold; color: khaki'>SoundPathResolver</span>

### **New** 🎉

1. Added the following methods to the <span style='font-weight: bold; color: khaki'>ContentLoaderFactory </span> class to create different kinds of content loaders
   * <span style='font-weight: bold; color: khaki'>CreateTextureLoader()</span>
   * <span style='font-weight: bold; color: khaki'>CreateTextureAtlasLoader()</span>
   * <span style='font-weight: bold; color: khaki'>CreateSoundLoader()</span>
2. <span style='font-weight: bold; color: khaki'>ContentSource </span> types are now called <span style='font-weight: bold; color: khaki'>Resolvers </span> and have been improved.  The following resolvers have been created to help resolve paths to various pieces of content
   * <span style='font-weight: bold; color: khaki'>AtlasJSONDataPathResolver</span>
   * <span style='font-weight: bold; color: khaki'>AtlasTexturePathResolver</span>
   * <span style='font-weight: bold; color: khaki'>SoundPathResolver</span>
   * <span style='font-weight: bold; color: khaki'>TexturePathResolver</span>
5. Created a <span style='font-weight: bold; color: khaki'>PathResolverFactory </span> class to create instances of the different resolvers
6. Created new types for loading texture atlas data such as texture atlas images and atlas sub texture data
   * Created the following types to be used for loading of texture atlas's
      * <span style='font-weight: bold; color: khaki'>IAtlasData</span>
      * <span style='font-weight: bold; color: khaki'>AtlasData</span>
      * <span style='font-weight: bold; color: khaki'>AtlasRepository</span>
      * <span style='font-weight: bold; color: khaki'>AtlasSubTextureData</span>
9. Made <span style='font-weight: bold; color: khaki'>MapValue() </span> overloads public to the library API for library users to use
   * These extension methods can be used map a value from one range to another

### **Tech Debt/Cleanup** 🧹

1. Removed **VelcroPhysics.dll** library
2. Removed the following classes/types
   1. <span style='font-weight: bold; color: khaki'>PhysicsBody</span>
   2. <span style='font-weight: bold; color: khaki'>PhysicsWorld</span>
   3. <span style='font-weight: bold; color: khaki'>PhysicsBodySettings</span>
   4. <span style='font-weight: bold; color: khaki'>VelcroBody</span>
   5. <span style='font-weight: bold; color: khaki'>VelcroWorld</span>

### **Improvements** 🌟

1. Increase of code coverage with unit tests

### **Nuget/Library Updates** 📦

1. The following packages were updated for the unit testing project
   * Removed nuget package **Microsoft.CodeAnalysis.FxCopAnalyers** *`v3.3.1`*
   * Added **Microsoft.CodeAnalysis.NetAnalyers** from *`v5.0.3`*
   * Updated nuget package **coverlet.msbuild** from *`v2.9.0`* to *`v3.0.2`*
   * Updated nuget package **Microsoft.NET.Test.Sdk** from *`v16.8.0`* to *`v16.8.3`*
   * Updated nuget package **Moq** from *`v4.15.1`* to *`v4.16.0`*
   * Updated nuget package **System.IO.Abstractions** from *`v12.2.24`* to *`v13.2.9`*

---

## <span style="color:mediumseagreen;font-weight:bold">Version 0.21.0</span>

### **Breaking Changes** 💣

1. Changed <span style='font-weight: bold; color: khaki'>Keyboard </span> class from static to non static
   * Implemented new <span style='font-weight: bold; color: khaki'>IKeyboard </span> interface that the <span style='font-weight: bold; color: khaki'>Keyboard </span> class inherits from.  This makes the keyboard functionality much more testable and able to be injected using a DI container
2. Changed <span style='font-weight: bold; color: khaki'>Mouse </span> class from static to non static
   * Implemented new <span style='font-weight: bold; color: khaki'>IMouse </span> interface that the <span style='font-weight: bold; color: khaki'>Mouse </span> class inherits from.  This makes the keyboard functionality much more testable and able to be injected using a DI container
3. Changed the <span style='font-weight: bold; color: khaki'>contentLoader </span> parameter type from a concrete <span style='font-weight: bold; color: khaki'>ContentLoader </span> type, to the <span style='font-weight: bold; color: khaki'>IContentLoader </span> interface type for the <span style='font-weight: bold; color: khaki'>LoadContent() </span> method in the <span style='font-weight: bold; color: khaki'>IContentLoadable </span> interface
   * This is to follow along with the design of making things flexible and testable and aligns with the rest of the library

### **Build/Release Pipelines** 🔁

1. Updated the YAML file for the build pipeline to use the new **Build-Release-Servers** agent pool
2. Added an additional task to the build, test, and publish artifact YAML template files to install **dotnet core 3.x sdk**

---

## <span style="color:mediumseagreen;font-weight:bold">Version 0.20.1</span> - <span style="color:indianred;font-weight:bold">(Hot Fix)</span>

### **New** 🎉

1. Changed the window to be displayed in the center of the screen horizontally and vertically by default

### **Bug Fixes** 🐛

1. Fixed an issue with the window position not working when using the <span style='font-weight: bold; color: khaki'>Window.Position </span> property
2. Fixed an issue with **GLFW** exceptions being thrown when using a width or height value of **'0'** when using the <span style='font-weight: bold; color: khaki'>CreateWindow() </span> method in the <span style='font-weight: bold; color: khaki'>WindowFactory </span> class

---

## <span style="color:mediumseagreen;font-weight:bold">Version 0.20.0</span>

### **New** 🎉

1. Added a method named <span style='font-weight: bold; color: khaki'>ShowAsync() </span> to the <span style='font-weight: bold; color: khaki'>IWindow </span> interface and all associated <span style='font-weight: bold; color: khaki'>IWindow </span> implementations
   * This is to provide the ability for consumers of the library/framework to create <span style='font-weight: bold; color: khaki'>IWindow </span> implementations, that can show the window asynchronously instead of on the applications main thread
   * Example: Showing a **Raptor** window inside of a GUI application like **WPF** or **WinForms**, and not block the UI thread

---

## <span style="color:mediumseagreen;font-weight:bold">Version 0.19.0</span>

### **Bug Fixes** 🐛

1. Fixed an issue with an **OpenGL** bindings exception being thrown when using the <span style='font-weight: bold; color: khaki'>SpriteBatchFactory.CreateSpriteBatch() </span> method
   * This was due to something that was missed when implementing the change in release **v0.18.0**

### **Nuget/Library Updates** 📦

1. Updated nuget package **OpenTK** from **v4.0.6** to **v4.2.0**
2. Updated nuget package **Microsoft.CodeAnalysis.FxCopAnalyzers** from **v3.3.0** to **v3.3.1**
3. Updated nuget package **SixLabors.ImageSharp** from **v1.1.1** to **v1.0.2**
4. Updated nuget package **System.IO.Abstractions** from **v12.2.7** to **v12.2.24**
5. Updated nuget package **Simplelnjector** from **v5.0.4** to **v5.1.0**
6. Updated nuget package **Moq** from **v4.14.7** to **v4.15.1**
7. Updated nuget package **Microsoft.NET.Test.Sdk** from **v16.7.1** to **v16.8.0**

---

## <span style="color:mediumseagreen;font-weight:bold">Version 0.18.0</span>

### **New** 🎉

1. Added a new method to the <span style='font-weight: bold; color: khaki'>Window </span> class named <span style='font-weight: bold; color: khaki'>ShowAsync() </span> to add the ability to run a **Raptor Window** on another thread
   * Currently <span style='font-weight: bold; color: khaki'>Show() </span> blocks the thread that it was invoked on due to the <span style='font-weight: bold; color: khaki'>Show() </span> method not returning until the window has been closed  This is not ideal due to how long the window needs to live.  Using <span style='font-weight: bold; color: khaki'>ShowAsync() </span> will allow the use of the window while still allowing execution after the invocation of the method
     * Use Case: Running a **Raptor Window** from a GUI application
   * Any implementation of the <span style='font-weight: bold; color: khaki'>Window </span> class will get this functionality

### **Bug Fixes** 🐛

1. Fixed an issue with the abstract <span style='font-weight: bold; color: khaki'>Window </span> class not being disposed of properly when closing the window
2. Fixed an issue with the <span style='font-weight: bold; color: khaki'>Sound </span> class not being disposed of properly when calling dispose explicitly or implicitly

---

## <span style="color:mediumseagreen;font-weight:bold">Version 0.17.0</span>

### **Changes** ✨

1. Improved how the <span style='font-weight: bold; color: khaki'>Window </span> class shows the window
   * Previously, the window was displayed on window creation before the <span style='font-weight: bold; color: khaki'>Show() </span> method was invoked.  Now the window will only be displayed when invoking the <span style='font-weight: bold; color: khaki'>Show() </span> method.  This gives more control over how the window is displayed during window creation and also enables better management of when a window is shown on another thread other than the **main thread**
   * NOTE: Simply invoking the <span style='font-weight: bold; color: khaki'>Show() </span> method on a worker thread will run the window on that thread

---

## <span style="color:mediumseagreen;font-weight:bold">Version 0.16.1</span> - <span style="color:indianred;font-weight:bold">(Hot Fix)</span>

### **Bug Fixes** 🐛

1. Fixed an issue with failing unit tests related to unsafe pointer setup with the unit tests and implementation details related to **GLFW**

---

## <span style="color:mediumseagreen;font-weight:bold">Version 0.16.0</span>

### **New** 🎉

1. Added the ability for the abstract <span style='font-weight: bold; color: khaki'>Window </span> class to allow for automatic buffer clearing
   * Setting the <span style='font-weight: bold; color: khaki'>AutoClearBuffer </span> property to true will automatically clear the buffer before rendering
   * If the <span style='font-weight: bold; color: khaki'>AutoClearBuffer </span> property is set to false, the buffer has to be cleared manually.  This can be done by using the <span style='font-weight: bold; color: khaki'>SpriteBatch.Clear() </span> method

### **Breaking Changes** 💣

1. Moved the following types to to a new namespace with the name <span style='font-weight: bold; color: khaki'>Raptor.Desktop`
   * <span style='font-weight: bold; color: khaki'>IWindow</span>
   * <span style='font-weight: bold; color: khaki'>Window</span>
2. Added 2 more new interfaces related to window properties for window state and actions
   * Added new interfaces called <span style='font-weight: bold; color: khaki'>IWindowProps </span> and <span style='font-weight: bold; color: khaki'>IWindowActions</span>
   * These interfaces were added to the <span style='font-weight: bold; color: khaki'>Raptor.Desktop </span> namespace
3. Changed the name of the <span style='font-weight: bold; color: khaki'>Window.UpdateFreq </span> property to <span style='font-weight: bold; color: khaki'>Window.UpdateFrequency</span>

### **Bug Fixes** 🐛

1. Fixed a bug where the buffer was not being cleared before textures were being rendered
   * This was causing textures to be smeared on the render target during texture movement or animation
2. Fixed a bug where textures being rendered would be stretched horizontally and vertically when resizing the window

---

## <span style="color:mediumseagreen;font-weight:bold">Version 0.15.0</span>

### **Breaking Changes** 💣

1. Improved how content is used and managed
   * Removed <span style='font-weight: bold; color: khaki'>Window </span> class constructor dependency of the type <span style='font-weight: bold; color: khaki'>IContentLoader</span>
   * Created a <span style='font-weight: bold; color: khaki'>ILoader<T> </span> for **Raptor** content types below
     1. Graphics
     2. Sound
     3. Atlas Data
   * Created new <span style='font-weight: bold; color: khaki'>IContent </span> type.  Every content type created must inherit from this interface to be able to be loaded and managed as **content**.  The list below are the current types that have been changed to use the new <span style='font-weight: bold; color: khaki'>IContent </span> type
     1. <span style='font-weight: bold; color: khaki'>ITexture</span>
     2. <span style='font-weight: bold; color: khaki'>ISound</span>
     3. <span style='font-weight: bold; color: khaki'>IAtlasRegionRectangle</span>

### **New** 🎉

1. Created new <span style='font-weight: bold; color: khaki'>IPlatform </span> type with associated implementation type <span style='font-weight: bold; color: khaki'>Platform </span> to detect which platform the code is running on
   * Implemented use of <span style='font-weight: bold; color: khaki'>IPlatform </span> type into the 
2. Added new exception type <span style='font-weight: bold; color: khaki'>UnknownContentException</span>
   * This exception is thrown when dealing with unsupported content

---

## <span style="color:mediumseagreen;font-weight:bold">Version 0.14.0</span>

### **New** 🎉

1. Added the ability to retrieve information about monitors that might be hooked up to the system
   * This includes width, height, DPI, scaling and bit depth

### **Other** 👏

1. Simple cleanup and refactoring

---

## <span style="color:mediumseagreen;font-weight:bold">Version 0.13.0</span>

### **New** 🎉

1. Added the ability to change the position of the window programmatically
2. Added the ability to enable and disable the maximize button and the resizing of the window border
   * Both the maximize button and the disabling the resizing of the window border are both set using the <span style='font-weight: bold; color: khaki'>WindowBorder.Fixed </span> border state value
3. Added the ability to set the mouse cursor to be shown or hidden when hovering over the window
4. Added the ability to set the state of the window
   * Available states:
     1. Minimized
     2. Normal
     3. Maximized
     4. Full Screen

### **Other** 👏

1. Simple cleanup and refactoring

---

## <span style="color:mediumseagreen;font-weight:bold">Version 0.12.0</span>

### **Changes** ✨

1. Changed the nuget package setup to properly include native **OpenAL** runtime libraries

### **Nuget/Library Updates** 📦

1. Updated **OpenTK** from **v4.0.0-pre9.1** to **v4.0.6**
2. Updated **SimpleInjector** from **v5.0.3** to **v5.0.4**
3. Updated **Microsoft.NET.Test.Sdk** from **v16.7.0** to **v16.7.1**
4. Updated **Moq** from **v4.14.5** to **v4.14.7**

### **Breaking Changes** 💣

1. Refactored **KeyCode** enumeration to match closer to **OpenTK** version

### **Other** 👏

1. Changed **OpenGL** shader source code files to embedded resources
   * This means that the source code is embedded into the assembly itself and loaded during runtime to be sent to the GPU
2. Refactored code to meet coding standards
3. Added ability to move the texture in the **Sandbox** project to test out keyboard input
4. Replaced the **KinsonDigital.FileIO** with **System.IO.Abstractions**
   * This was done to not have to maintain the **KinsonDigital.FileIO** library anymore and to use the better **System.IO.Abstractions** library

---

## <span style="color:mediumseagreen;font-weight:bold">Version 0.11.0</span>

### **Other** 👏

1. Added rules to **editorconfig** files in solution to improve coding standards
2. Adjusted **editorconfig** solution setup

### **Nuget/Library Updates** 📦

1. Updated **KinsonDigital.FileIO** nuget package from **v1.6.1** to **1.7.0**

---

## <span style="color:mediumseagreen;font-weight:bold">Version 0.10.0</span>

### **Additions**

1. Added code analyzers to the solution to enforce coding standards and keep code clean
   * This required adding nuget packages to allow the analyzers to run
		1. Microsoft.CodeAnalysis.FxCopAnalyzers - v3.3.0
		2. StyleCop.Analyzers - v1.1.118
   * Added/setup required **editorconfig** files with appropriate coding analyzer rules
   * Added **stylecop.json** files for the stylecop analyzer
2. Refactored code to meet code analyzer requirements
   * This was a very large code refactor
3. Added unit tests to increase code coverage

### **Other** 👏

1. Fixed various failing unit tests

---

## <span style="color:mediumseagreen;font-weight:bold">Version 0.9.0</span>

### **New** 🎉

1. Got the **Keyboard** functionality working.  Now the keyboard can be used!!
2. Got the **Mouse** functionality working.  Now the mouse can be used!!

### **Changes** ✨

1. Removed old **SDL** related code from library/project/solution
2. Refactored code to prevent **OpenGL** related code from being exposed to the public API of the Raptor library
3. Refactored code to use unsigned integers instead of signed integers where it makes sense
   * Example: You cannot have a texture with a negative width.  This has been converted to an unsigned integer
4. Created unit tests where applicable
5.  Implemented various analyzers for the purpose of better code.  This resulted in large amounts of refactoring of the code base to satisfy the analyzers

### **Nuget/Library Updates** 📦

1. Updated nuget package **coverlet.msbuild** from **v2.6.3** to **v2.9.0**
2. Updated nuget package **Microsoft.NET.Test.Sdk** from **v16.2.0** to **v16.6.1**
3. Updated nuget package **Moq** from **v4.12.0** to **v4.14.5**
4. Updated nuget package **xunit.runner.visualstudio** from **v2.4.1** to **v2.4.2**
5. Updated nuget package **SimpleInjector** from **v5.0.1** to **v5.0.2**

---

## <span style="color:mediumseagreen;font-weight:bold">Version 0.8.0</span>

### **New** 🎉

1. Setup library to use native **x86** **SDL** libraries

### **Changes** ✨

1. Updated **SDLCore** library from **v0.1.1** to **v0.3.0**

### **Other** 👏

1. Updated **FxCopAnalyzers** library from version **v2.9.8** to **v3.0.0**

---

## <span style="color:mediumseagreen;font-weight:bold">Version 0.7.0</span>

### **Breaking Changes** 💣

1. Changed the visible scope of all **SDL** implementation classes from **public** to **internal**

### **Nuget/Library Updates** 📦

1. Updated nuget package **SDLCore** from **v0.1.0** to **v0.1.1**

### **Other** 👏

1. Changed how **SDL** files are dealt with in the build and nuget packaging process
   * Removes dependency on the native **SDL** libraries in **Raptor** code base and relies on **SDLCore** nuget package native **SDL** libraries
2. Cleaned up **ExtensionMethodTests** file
3. Added a file to the code base for other developers to know how to contribute to the project
   * Refer to the **CONTRIBUTING.md** file
4. Improved **YAML** files for the **develop** and **production** build pipelines
   * This involved splitting various build tasks intro proper states and jobs

---

## <span style="color:mediumseagreen;font-weight:bold">Version 0.6.0</span>

### **Nuget/Library Updates** 📦

1. Updated **SDLCore** nuget package from **v0.0.1** to **v0.1.0**

### **Changes** ✨

2. Refactored code according to **Microsoft's** FxCop analyzers as well as setting code base to use nullable references
   * This greatly improves the code base to account for null reference exceptions as well as following better coding standards

### **Other** 👏

1. Changed name of **Raptor.Tests** unit testing project to **RaptorTests**
2. Added **runsettings** file to help facilitate better code coverage during development and during **CI/CD** operations

---

## <span style="color:mediumseagreen;font-weight:bold">Version 0.5.0</span>

### **Changes** ✨

1. Set the **solution/project** to use nullable reference types
2. Created **README.md** file
3. Updated **solution/project** to use C# v8.0
4. Updated YAML files to improve build pipelines

### **Breaking Changes** 💣
1. Replaced the custom **Vector** type with the dotnet core **System.Numerics.Vector2** type
   * This reduces maintenance and upkeep of code
