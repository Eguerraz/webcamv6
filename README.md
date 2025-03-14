🚗 Projet de Reconnaissance de Panneaux de Vitesse avec Caméra

📝 Description du Projet

Ce projet a pour but de signaler les panneaux de limitation de vitesse à l'aide d'une caméra. Le programme détecte les panneaux en analysant les images capturées et en identifiant les chiffres indiquant la vitesse maximale autorisée.

🛠️ Fonctionnement du Programme

📸 Capture d'Image : Le programme recherche et utilise la première caméra disponible sur l'appareil exécutant le code.

🖼️ Conversion Hexadécimale : L'image capturée est convertie en valeurs hexadécimales pour faciliter l'analyse des données.

🔴 Isolation de la Couleur Rouge : Comme les panneaux de limitation de vitesse en France sont situés à droite et encadrés de rouge, le programme isole la zone rouge dans le coin droit de l'image.

⚫ Détection des Pixels Noirs : À l'intérieur de cette zone rouge, le programme analyse les pixels noirs formant les chiffres du panneau.

🧠 Reconnaissance des Chiffres : À l'aide de calculs de probabilité, le programme traduit les formes détectées en chiffres représentant la vitesse.

🌐 Technologies Utilisées

Python : Langage de programmation principal.

OpenCV : Bibliothèque pour la capture et le traitement d'images.

Algorithmes de Probabilité : Méthodes statistiques pour la reconnaissance des chiffres.

🚀 Comment Lancer le Projet

📥 Cloner ce dépôt :

git clone https://github.com/ton-repo.git

🖥️ Installer les dépendances :

pip install opencv-python numpy

▶️ Exécuter le programme :

python main.py

👥 Contributeurs

👨‍💻 Esteban Guerraz : Développement de la reconnaissance de panneaux
