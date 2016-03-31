# Jeu-Passif-Prototype-Unity3D
Création d'un petit jeu passif dans lequel le joueur peut créer et placer des bâtiments qui permettent de générer des ressources de sorte à pouvoir fabriquer des équipements pour les revendre.

Sites web :
- Trello : https://trello.com/b/l6zvRFBy/idle-game-prototype-unity3d
- Site dédié : http://haddadbpro.wix.com/portfolio#!jeu-passif/se9ch
- Linkedin : https://www.linkedin.com/in/haddadbenjamin

Vidéo de l'application (cliquer)  : 
[![ScreenShot](http://i.imgur.com/Oj5SNxX.jpg)](https://www.youtube.com/watch?v=_2Nx2YNqQiE)

Points intéressants :
- Mon architecture permet de respecter l'encapsulation de mes classes grâce à mes gestionnaires d'évènements.
- Tous les points importants gameplay et qui doivent être unique du  sont configurables à travers l'inspecteur d'Unity, ceci me permet aussi d'éviter des doublons de données.
- Toutes les intéractions sont faites à un moment précis et non à chaque frame.
- Mon gestionnaire de "pools" permet d'éviter les allocations et désallocations massive lorsque l'application tourne.
- L'architecture de cette application donne accès à une pelletée de services : gestionnaire d'évènements, gestionnaire de pool étendable ou non, gestionnaire d'objets, de matériaux, de textures, de sprites, de références, etc...
- Les bâtiments de génération de ressources peuvent générer plusieurs types de ressources, et en fonction du niveau de ce bâtiment ils peuvent générer plus ou moins de ressources, leur prix pour aller au niveau suivant augmente, etc.. Tous ces paramètres sont entièrement configurables à travers l'interface. Ceci m'a permis de générer les 12 différents bâtiments de ressources via l'interface sans devoir rajouter une seule ligne de code.
- On peut configurer chacune des recettes à travers l'interface par type d'équipement et leur spécifier des ressources pré requises, des matériaux pré requis, prix de l'objet de base, niveau de l'objet de base, etc... Ceci permet de changer le gameplay sans devoir coder.
- Ce projet a été réalisé en 7 à 10 jours en temps plein si l'on ne compte pas les pauses et il est constitué de ~100 différents fichiers.

[align=center]
![Alt text](http://i.imgur.com/3Own1bK.jpg "Recettes d'armures.")
![Alt text](http://i.imgur.com/5u6BRrf.jpg "Fabrication d'équipements en cours.")
![Alt text](http://i.imgur.com/xfmqMOx.jpg "Amélioration d'un bâtiment.")
![Alt text](http://i.imgur.com/JogB4VU.jpg "Vente d'un bâtiment.")
![Alt text](http://i.imgur.com/WqNXcOJ.jpg "Menu de création de bâtiment qui génère de la ressource puis placement de ce bâtiment.")
![Alt text](http://i.imgur.com/U7lpP7W.jpg "Menu de création d'équipement, on peut sélectionner le filtre que l'on souhaite et configurer entièrement la partie recette ainsi que la configuration des objets qu'il génère.")
![Alt text](http://i.imgur.com/V8JBkZ0.jpg "Boutons d'intéractions d'un bâtiment.")
![Alt text](http://i.imgur.com/2OX9oRM.jpg "Boutons permettant de naviguer sur les menus de construction d'équipements et de création de bâtiment générant des ressources.")
![Alt text](http://i.imgur.com/oQt0BcF.png "Architecture du jeu. (133 fichiers en ~10 jours en temps plein)")
![Alt text](http://i.imgur.com/zx10bbK.jpg "Configuration des bâtiments générant des ressources.")
![Alt text](http://i.imgur.com/Znyq5tb.jpg "Configuration des recettes ainsi que des objets générer, les données qui peuvent changées ne peuvent pas être modifiées d'ici cette interface. (ceci me permet d'éviter des doublons de données).")
![Alt text](http://i.imgur.com/2fiBgcz.jpg "Présentation d'une partie des services de cette application.")
![Alt text](http://i.imgur.com/GDUWymp.jpg "Animations de mes menus.")
![Alt text](http://i.imgur.com/5Tk6mT2.jpg "Recettes d'arcs.")
![Alt text](http://i.imgur.com/3Own1bK.jpg "Recettes d'armures.")
![Alt text](http://i.imgur.com/FTkMxaP.jpg "Recettes de dagues.")
![Alt text](http://i.imgur.com/e22GVIn.jpg "Recettes de vestes.")
![Alt text](http://i.imgur.com/3Own1bK.jpg "Recettes d'armures.")
[/align]
