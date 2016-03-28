# Jeu-Passif-Prototype-Unity3D
Création d'un petit jeu passif dans lequel le joueur peut créer et placer des bâtiments qui génèrent de la ressources et par la suite les utiliser de sorte à créer des équipements de sorte à les revendre.

- Trello : https://trello.com/b/l6zvRFBy/idle-game-prototype-unity3d

Points intéressants :
- Mon architecture s'améliore et permet de respecter l'encapsulation de mes classes grâce à mes gestionnaires d'évènements.
- Tous les points important unique du gameplay sont configurables à travers l'inspecteur d'Unity, ceci me permet aussi d'éviter des doublons de données.
- Toutes les intéractions sont faites à un moment précis et non à chaque frame.
- L'architecture de cette application donne accès à une pelletée de services : gestionnaire d'évènements, gestionnaire de pool étendable ou non, gestionnaire d'objets, de matériaux, de textures, de sprites, de references, etc...
- Les bâtiments de génération de resources peuvent générer plusieurs types de resources, et en fonction du niveau de ce bâtiment ils peuvent générer plus ou moins de ressources, leur prix pour aller au niveau suivant augmente, etc.. Tous ces paramètres sont entièrement configurables à travers l'interface. Ceci m'a permis de générer les 12 différents bâtiments de ressources via l'interface sans devoir rajouter une seule ligne de code.
- On peut configurer chacune des recettes à travers l'interface par type d'équipement et leur spécifier des ressources pré requises, des matériaux prérequis, prix de l'objet de base, niveau de l'objet de base, etc... Ceci permet de changer le gameplay sans devoir coder.
- Ce projet a été réalisé en 7 à 10 jours en temps plein et il est constitué de ~75 différents fichiers.

![Alt text](http://i.imgur.com/WqNXcOJ.jpg "Menu de création de bâtiment qui génère de la ressource puis placement de ce bâtiment.")
![Alt text](http://i.imgur.com/U7lpP7W.jpg "Menu de création d'équipement, on peut sélectionner le filtre que l'on souhaite et configurer entièrement la partie recette ainsi que la configuration des objets qu'il génère.")
![Alt text](http://i.imgur.com/V8JBkZ0.jpg "Boutons d'intéractions d'un bâtiment.")
![Alt text](http://i.imgur.com/2OX9oRM.jpg "Boutons permettant de naviguer sur les menus de construction d'équipements et de création de bâtiment générant des resources.")
![Alt text](http://i.imgur.com/ffKkgYI.jpg "Architecture du jeu. (~75 fichiers en 7-10 jours en temps plein)")
![Alt text](http://i.imgur.com/zx10bbK.jpg "Configuration des bâtiments générant des ressources.")
![Alt text](http://i.imgur.com/Znyq5tb.jpg "Configuration des recettes ainsi que des objets générer, les données qui peuvent changés ne peuvent pas être modifié d'ici cette interface. (ceci me permet d'éviter des doublons de données).")
![Alt text](http://i.imgur.com/2fiBgcz.jpg "Présentation d'une partie des services de cette application.")
![Alt text](http://i.imgur.com/GDUWymp.jpg "Animations de mes menus.")
