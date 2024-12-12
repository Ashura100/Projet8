using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;

namespace CarVr
{
    public class UIManager : MonoBehaviour
    {
        public RawImage vehicleImage;               // Image de rendu de la cam�ra
        public TextMeshProUGUI nameText, modelText, chText, energyText, weightsText, yearsText, priceText;
        public GameObject engineButton, hornButton;
        public List<CarSO> carList = new List<CarSO>();  // Liste des v�hicules disponibles
        public Transform[] spawnPoints;             // Tableau de points de pose
        public BoxCollider boxCollider;
        public int currentVehicleIndex = 0;        // Index du v�hicule actue

        public CarSO currentCar;                   // R�f�rence � la voiture actuellement s�lectionn�e
        private List<GameObject> instantiatedCars = new List<GameObject>(); // Liste des v�hicules instanci�s

        void Start()
        {
            engineButton.SetActive(false);
            hornButton.SetActive(false);

            carList = CatalogManager.instance.carList;
            currentCar = CatalogManager.instance.currentCar;
            currentVehicleIndex = 0;
        }
        void Update()
        {
            NextVehicle();
            PreviousVehicle();
        }

        // M�thode pour changer le v�hicule affich�
        public void SwitchVehicle(CarSO car)
        {
            // D�finir la voiture actuelle
            currentCar = car;

            // Mettre � jour l'image avec la RenderTexture de la nouvelle voiture
            if(vehicleImage is not null)
                vehicleImage.texture = currentCar.image;

            // Activer le GameObject associ� au v�hicule s�lectionn�
            foreach (var carSO in carList)
            {
                if (carSO.gameOject != null)
                    carSO.gameOject.SetActive(carSO == currentCar);
            }
        }

        // M�thode pour passer au v�hicule suivant dans la liste
        public void NextVehicle()
        {
            if (carList.Count == 0) return;
            currentVehicleIndex = (currentVehicleIndex + 1) % carList.Count;
            SwitchVehicle(carList[currentVehicleIndex]);
        }

        // M�thode pour passer au v�hicule pr�c�dent dans la liste
        public void PreviousVehicle()
        {
            if (carList.Count == 0) return;
            currentVehicleIndex = (currentVehicleIndex - 1 + carList.Count) % carList.Count;
            SwitchVehicle(carList[currentVehicleIndex]); 
        }

        // M�thode pour changer la couleur en fonction de la couleur du bouton s�lectionn�
        public void ChangeColor(Button colorButton)
        {
            if (currentCar != null && currentCar.color != null)
            {
                // R�cup�rer la couleur de l'image du bouton
                Color selectedColor = colorButton.GetComponent<Image>().color;

                // Appliquer la couleur au mat�riau de la voiture actuelle
                currentCar.color.color = selectedColor;
            }
        }

        void DisplayCarData()
        {
            if (currentCar == null) return;
            try
            {
                nameText.text = currentCar.name;
                modelText.text = currentCar.description;
                chText.text = currentCar.ch.ToString() + "cv";
                energyText.text = currentCar.energy;
                weightsText.text = currentCar.weights.ToString() + "kg";
                yearsText.text = currentCar.years.ToString();
                priceText.text = currentCar.price.ToString() + "�";
            }
            catch (Exception e)
            {
                return;
            }
        }

        // M�thode pour s�lectionner et instancier la voiture actuelle sur un point de pose
        public void CarSelected()
        {
            // D�truire tous les v�hicules d�j� instanci�s pour �viter les doublons
            foreach (var car in instantiatedCars)
            {
                if (car != null)
                    Destroy(car);
            }
            instantiatedCars.Clear();  // Vider la liste des v�hicules instanci�s

            // Instancier le v�hicule sur chaque point de pose
            if (currentCar != null && currentCar.gameOject != null)
            {
                foreach (var spawnPoint in spawnPoints)
                {
                    GameObject newCar = Instantiate(currentCar.gameOject, spawnPoint.position, spawnPoint.rotation);
                    newCar.transform.SetParent(spawnPoint, true);
                    instantiatedCars.Add(newCar);  // Ajouter � la liste des v�hicules instanci�s

                    engineButton.SetActive(true);
                    hornButton.SetActive(true);
                    DisplayCarData();
                }
            }
        }
    }
}