# **Projet 8**

![image](https://github.com/user-attachments/assets/e373fb94-3f59-4ff2-9112-a6fa921f59b0)


## **Introduction**

![image](https://github.com/user-attachments/assets/5e0a27b4-1565-4ed1-ba8e-00c495f12588)


- Objectif de l'application
- Explication de son fonctionnement
- Démonstration
- Conclusion

## **Objectifs de l'application**

![P9 3](https://github.com/user-attachments/assets/020a2c59-7634-4b09-ad02-41ae398b5314)
![P8 4](https://github.com/user-attachments/assets/182f6f48-3bab-4249-a8c9-c3ff41d7d80b)
![P8 2](https://github.com/user-attachments/assets/80cda889-03d0-4503-834e-c86c78f60d1c)
![P8 1](https://github.com/user-attachments/assets/c9a03e94-b969-4c9c-82ce-e600a37af074)

Créer une application VR (Réalité Virtuelle) sur Unity, dans laquelle vous trouvez un catalogue de voiture, que vous pouvez observer et changer leur couleur.                  
Le déplacement peut se faire de deux manière en téléportation et avec les joysticks.

## **Explication**

 - **XR Rig**:

 Le **XR Rig** dans Unity est un **Prefab** pré-configuré pour permettre le **déplacement**, les **mouvements** et les **interactions**, il inclut les **Controllers**, la **Caméra**, un **Character Controller** et les **Scripts** permettant le bon fonctionnement    des principales mécanique, C'est grâce à lui que l'on interagit dans les scenes de l'application mais aussi qu'on se déplace sur un plane.

- **Teleportation Anchor**:

Ils sont des points de téléportation avec lesquels on peut **interagir** grâce aux **raycasts des controllers** pour les selectionner et se teleporter au prochain point, Ils permettent au personnage de se téléporter dans la scene en elle-même.

- **ScriptableObjects**

  Les données des préfabs des voitures sont dans des _**scriptableObjects**_, ils permetent de stocker et modifier facilement différentes données de chaque article.

- **UI**:

  Il a été demandé d'afficher les FPS, triangles, batches, ect.. ils sont affichés sur un **Canvas** en **WorldSpace** et des **TextMeshPro** contenus dans le canvas.
  L'application possède Quatre UI, un principal sur lequel apparaissent les voitures en temps réel grâce à un **Render Texture** qui renvoit une image camera, le changement couleur, ensuite l'UI des specs, l'UI option jeu (qualité graphique, gestion volume sfx, musique et DevMod pour l'affichage FPS) et un UI pour la gestion playlist.

- **DataBase**:

  Une **database** a été créée pour les spécificités des voiture en **SQL** puis mise en ligne et appelé via **WebRequest**.

- **Bonus**:

  Pour rendre l'application plus immersive, un personnage en model 3D a été rajouté avec les animations des mains, une playlist de musique et des sons (générique) de moteur et klaxon pour les voitures.

## **Démonstration**

![Projet8](https://github.com/user-attachments/assets/53764a7a-885c-494b-8a53-c0794ab15bd4)


## **Conclusion**

Pour Conclure, l'application est fonctionnelle, permet de choisir trois voitures et changer leurs couleurs, de se déplacer en se téléportant et déplacement normal.

Lien APK : https://drive.google.com/file/d/1zNbWe9m633xpmN8a0d8k0UdXuuXsQT0p/view?usp=drive_link

![true-romance-tony-scott](https://github.com/user-attachments/assets/6d7612a3-c492-4003-ad20-21d691f44156)

