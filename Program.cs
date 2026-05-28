using System.Runtime.CompilerServices;
using CheckRegister.Models;
using CheckRegister.Services;




ApplicationState appState = new ApplicationState();
Menu menu = new Menu(appState);

await menu.DisplayMenu();
