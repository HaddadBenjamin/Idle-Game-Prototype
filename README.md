# Jeu-Passif-Prototype-Unity3D
Prototypage d'un idle game, le but de ce projet est de me faire manipuler l'UI d'Unity, voir les problèmatiques d'un Idle Game, tenter d'avoir une architecture bien pensé, utiliser les animations d'Unity et probablement tester le réseau d'Unity.

- Trello : https://trello.com/b/l6zvRFBy/idle-game-prototype-unity3d
- Architecture des batiments (sans la partie UI) : http://i.imgur.com/BR4Ccmp.png
- Code source actuel : http://i.imgur.com/eOzRsFw.png


Points intéressants :
- Mon architecture s'améliore et permet de respecter l'encapsulation de mes classes grâce à mon gestionnaire d'évênements.
- Tous les points important du gameplay sont configurable à travers l'inspecteur d'Unity.
- Aucune des données de cette architecture sont en double.
- Toutes les intéractions sont faites à un moment précis et non à chaque frame.
- L'architecture de cette application donne accès à une pelleté de services : gestionnaire d'évênements, gestionnaire de pool étendable ou non, gestionnaire d'objets, de matériaux, de textures, de sprites, de references, etc...
- Les bâtiments peuvent générer plusieurs types de resources et ont un prix configurable par niveau, les resouces qu'ils gnèrent sont aussi entièrement configurable à travers l'inteface, tous le code est générique, ce qui est plaisant c'est que tous est configurable et par la suite si je souhaite générer d'autres ressources ou créer d'autres type de bâtiment qui en génère je n'aurais pas besoin de coder de nouvelles lignes de codes.

![Alt text](http://i.imgur.com/CUyM374.jpg "Création et placement d'un bâtiment qui permet de générer des resources.")
![Alt text](http://i.imgur.com/K4ZQyY7.jpg "Interface par défaut où l'on voit les ressources du joueur.")
![Alt text](http://i.imgur.com/n5aS2aj.png "Une bonne pelleté de services qui sont tous configurable.")
![Alt text](http://i.imgur.com/xTl3NCu.png "Code actuel du projet après environ 35-40h de développement il me semble (5 jours, vérifier cela sur le Github)")

Il y a actuellement 10 bâtiments configuré qui peuvent générés plusieurs types de ressources, chacun de ces bâtiments ont un prix de base configurable et peuvent être monter de niveau si vous payer un certain montant de ressources (configurable) ce qui leur permet de générer plus de ressources et là encore tous est configurable à travers l'interface d'Unity.

Actuellement on peut placer 10 différents bâtiments dont 3 bâtiments par type de bâtiment.

Grâce à l'interface générique réalisé sur ce projet, je n'est pas eu besoin de rajouter une seule ligne de code pour rajouter ces 10 nouveaux bâtiments.

Je pense que je vais maintenant m'attaquer à l'interface d'informations des bâtiments qui permettra de monter de niveau un bâtiment si l'on paye un montant de ressources, déplacer un bâtiment, supprimer un bâtiment, vendre un bâtiment.

Enfin je terminerai le gameplay de base par la création d'objets avec ces ressources et les permettre de les revendre en or du jeu.

Ceci permettra de poser la base du gameplay de ce jeu, quand cela sera fini j'imaginerai un gameplay plus poussé avec des objets, des sorts, des dungeons de sorte à rendre ce jeu plus riche, plus dynamique et plus fun.

