using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Networking;
using static System.Net.WebRequestMethods;

namespace CarVr
{
    public class CatalogManager : MonoBehaviour
    {
        public static CatalogManager instance;
        public List<CarSO> carList = new List<CarSO>();  // Liste des véhicules disponibles
        public CarSO currentCar;
        public const string carPhp = "https://virtualhome.hopto.org/carvr/";

        private void Awake()
        {
            instance = this;
        }

        private void Start()
        {
            StartCoroutine(GetCarData());
        }

        public IEnumerator GetCarData()
        {
            string url = carPhp;
            using (UnityWebRequest webRequest = UnityWebRequest.Get(url))
            {
                // Request and wait for the desired page.
                yield return webRequest.SendWebRequest();

                if (webRequest.result == UnityWebRequest.Result.ConnectionError || webRequest.result == UnityWebRequest.Result.ProtocolError)
                {
                    Debug.LogError(webRequest.error);
                }
                else
                {
                    // Show results as text
                    Debug.Log(webRequest.downloadHandler.text);

                    // Process the data as needed
                    ProcessCarData(webRequest.downloadHandler.text);
                }
            }
        }

        void ProcessCarData(string jsonData)
        {
            JArray jArray = JArray.Parse(jsonData);
            JObject jo = jArray.Children<JObject>().FirstOrDefault();

            Debug.Log(jo.GetValue("Maker"));

            carList.Clear();

            foreach (JObject carData in jArray.Children<JObject>())
            {
                // Créer une nouvelle instance de carSo pour chaque voiture du JSON
                CarSO newCar = ScriptableObject.CreateInstance<CarSO>();

                newCar.name = carData.GetValue("Maker").ToString();
                newCar.description = carData.GetValue("Model").ToString();
                newCar.energy = carData.GetValue("Energy").ToString();
                newCar.ch = float.Parse(carData.GetValue("Ch").ToString());
                newCar.weights = float.Parse(carData.GetValue("Weights").ToString());
                newCar.years = float.Parse(carData.GetValue("Years").ToString());
                newCar.price = float.Parse(carData.GetValue("Price").ToString());
                newCar.image = Resources.Load<RenderTexture>(carData.GetValue("Image").ToString());
                newCar.color = Resources.Load<Material>(carData.GetValue("Paint").ToString());
                GameObject go = Resources.Load<GameObject>(carData.GetValue("Model3D").ToString());
                newCar.gameOject = go;

                // Ajoutez l'item à la liste des items
                carList.Add(newCar);

                // Debug log pour vérifier les éléments ajoutés
                Debug.Log("Added car: " + newCar.name);
            }

            // Après avoir ajouté tous les items, vous pouvez éventuellement déclencher une mise à jour de l'interface utilisateur
            UpdateUIWithItems();
        }

        void UpdateUIWithItems()
        {
            // Implémentez la logique pour afficher les items sur l'interface utilisateur
            // Par exemple, vous pouvez itérer sur la liste des items et les afficher dans un conteneur
            foreach (CarSO car in carList)
            {
                // Ajoutez le code ici pour instancier des éléments d'interface utilisateur et les remplir avec les données de l'item
                Debug.Log("Displaying Car: " + car.name);
            }
        }

    }
}
