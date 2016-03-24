# Jeu-Passif-Prototype-Unity3D
Prototypage d'un idle game, le but de ce projet est de me faire manipuler l'UI d'Unity, voir les problèmatiques d'un Idle Game, et probablement tester le réseau d'Unity.

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
