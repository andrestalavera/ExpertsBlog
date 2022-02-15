# Applications mobiles avec **Xamarin.Forms**

## Les approches du développement mobile :
- Applications développées avec les langages/technologies Java ou Kotlin pour Android ou Objective-C ou Swift pour iOS/iPadOS.
- Applications développées avec des frameworks Xamarin, Flutter, etc. qui traduisent le code source (C#, Dart, ...) dans un code natif à travers des brides (ponts).
- Applications développées avec une technologies front (HTML, React, ...), parfois accompagnées de wrappers (PhoneGap, ...).

## Les approches technologiques :

|Technologie|Code métier|Code UI|Android|iOS / iPadOS|
|--|--|--|--|--|
|Android|Java/Kotlin|AXML|✅|❌|
|iOS/iPadOS|Objective-C/Swift|XIB (Xcode Interface Builder, page par page) / **Storyboard (toutes les pages)**.|❌|✅|
|React Native|React (Typescript)|HTML, React, ...|✅|✅|
|PWA|JavaScript, Typescript, ReactJS, Angular, Blazor (C#), ...|HTML, technologie front (ReactJS, Angular, Blaozor, Razor, ...), ...|✅|✅|
|Flutter|Dart|Dart|✅|✅|
|Xamarin.Android|C#|AXML (natif Android)|✅|✅|
|Xamarin.iOS|C#|XIB, Storyboards (natif iOS)|✅|✅|
|Xamarin.Forms|C#|XAML (partagé)|✅|✅|

> Xamarin.Forms deviendra MAUI en novembre 2022.

Xamarin._X_ se base sur [Mono](https://www.mono-project.com) (et pas .NET Framework/.NET Core/.NET). C'est une implémentation de .NET (grâce au standard ECMA-335) sur les distributations Linux (Ubuntu, Xubuntu, DebianOS, OpenSuse, ...).

## Prérequis pour développer avec Xamarin
|Technologie|Installations Windows|Installations macOS|
|--|--|--|
|Xamarin.Android|SDK Android (pour les versions), JDK, Visual Studio avec les outils Xamarin, _Emulateurs_. |SDK Android (pour les versions), JDK, Visual Studio avec les outils Xamarin, _Emulateurs_.|
|Xamarin.iOS|Visual Studio avec les outils Xamarin, macOS avec XCode et les SDK's iOS (pour apparayer l'émulateur avec Windows et tester depuis cet OS) ou un compte Apple Developer.|XCode avec les SDK's et simulateurs iOS, Visual Studio, Xamarin.
|Xamarin.Forms|Les deux installations ci-dessus.|Les deux installations ci-dessus.|

> _En italique, les outils qui ne sont pas obligatoires._

# Les essentiels

## Pattern MVVM


# Experts Blog
Une application de blogs spécialisée dans les commerces de proximité.

## Structure de la solution
|Projet|Description|
|--|--|
|`ExpertsBlog.Entities`|Les modèles (`BlogPost`, `Category`, `Tag`, ) qui serviront à notre API.|