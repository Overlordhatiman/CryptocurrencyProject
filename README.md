# CryptocurrencyProject

## Overview
CryptocurrencyProject is a WPF application designed to manage and convert various cryptocurrencies for Digital Cloud Technologies as a test project. It supports multiple languages, allowing users to switch between different locales easily. The application fetches data from an external API and provides functionalities such as viewing currency details, converting between different cryptocurrencies, and updating the UI dynamically based on the selected language.

## Features
- **Currency List:** View a list of top cryptocurrencies.
- **Currency Converter:** Convert between different cryptocurrencies.
- **Language Support:** Switch between different languages (e.g., English, Ukrainian).
- **Dynamic Theme Switching:** Switch between light and dark themes.

## Technologies Used
- WPF (Windows Presentation Foundation)
- MVVM (Model-View-ViewModel) Pattern
- C#
- .NET
- Localization and Globalization

## Installation
1. **Clone the repository:**
   ```sh
   git clone https://github.com/yourusername/CryptocurrencyProject.git
   ```
2. **Navigate to the project directory:**
    ```sh
    cd CryptocurrencyProject
   ```
4. **Open the solution file `CryptocurrencyProject.sln` in Visual Studio.**
5.  **Restore the NuGet packages:**
        In Visual Studio, right-click the solution and select Restore NuGet Packages.
6.  **Build the solution:**
        Press Ctrl+Shift+B or use the Build menu.

Project Structure


![Preview](https://github.com/Overlordhatiman/CryptocurrencyProject/assets/37751934/66b27e53-a64e-462c-ae97-f80d729340fb)


Localization Resource Files

Add resource files for localization under the Resources folder:

    Messages.resx: Default language resource file.
    Messages.uk-UA.resx: Ukrainian language resource file.

Ensure these resource files include the necessary keys and values for your application strings.
